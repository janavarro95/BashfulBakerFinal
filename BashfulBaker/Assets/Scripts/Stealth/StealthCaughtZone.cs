using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Utilities;
using Assets.Scripts.GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthCaughtZone : MonoBehaviour
{
    public StealthAwarenessZone awareness;
    public Dialogue dialogue;
    public Sprite guardFace;
    public Item itemToTake;

    public bool inDialogue = false;
    private int dialoguePressesExit = 0;
    public int exitPresses = 0;

    public enum GuardType
    {
        Guard,
        Villager
    }

    /// <summary>
    /// Determines the type of npc this is attached to.
    /// </summary>
    public GuardType guardType;

    public bool hasEaten = false;
    public string dishConsumedName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // update
    private void Update()
    {
        if (inDialogue)
        {
            ProgressDialogue();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = guardFace;

        if (!collision.gameObject.GetComponent<PlayerMovement>().hidden)
        {
            Debug.Log("YOU GOT CAUGHT!");

            collision.gameObject.GetComponent<PlayerMovement>().currentStep = 0;

            if (this.guardType == GuardType.Guard)
            {
                if (hasEaten == false)
                {
                    Debug.Log("---this guard has not been fed");

                    if (Game.Player.activeItem != null && (Game.Player.activeItem is Dish))
                    {
                        Debug.Log("---player is holding something");

                        Item item = Game.Player.dishesInventory.getRandomDish();
                        if (!inDialogue)
                        {
                            dialogue = new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                            {
                                "*Sniff sniff* Ohh that {0} looks delicious!",
                                "Is that for me? Thanks! Now be carefull this late at night!"
                            }, Game.Player.activeItem.Name).ToArray());

                            BeginDialogue(dialogue, item);
                        }
                        return;
                    }
                    else if (Game.Player.dishesInventory.getAllDishes().Count > 0)
                    {
                        Debug.Log("---player is not holding anything, but has items in inventory");

                        Item item = Game.Player.dishesInventory.getRandomDish();
                        if (!inDialogue)
                        {
                            dialogue = new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                            {
                                "*Sniff sniff* Ohh that {0} looks delicious!",
                                "Is that for me? Thanks! Now be carefull this late at night!"
                            }, Game.Player.activeItem.Name).ToArray());

                            BeginDialogue(dialogue, item);
                        }
                        return;
                    }
                    /*else if (hasEaten == true)
                    {
                        awareness.talkingToPlayer = true;
                        Game.DialogueManager.StartDialogue(new Dialogue("Guard",StringUtilities.FormatStringList(new List<string>()
                    {
                        "Hey there, your {0} you gave me was delicious! When can you make me some more!?",
                        "(You ended up getting caught up in converstation for quite some time.)"
                    },dishConsumedName).ToArray()));

                        Game.PhaseTimer.subtractTime(15);
                        //Game.Player.position = GameObject.Find("BakeryOutsideRespawn").transform.position;
                        awareness.talkingToPlayer = false;
                    }*/
                    else
                    {
                        Debug.Log("---escort the player, they have nothing");
                        if (!inDialogue)
                        {
                            dialogue = new Dialogue("Guard", new List<string>()
                            {
                                "You can't be out this late! Let me escort you back home!"
                            }.ToArray());

                            BeginDialogue(dialogue, null);
                        }
                        return;
                    }
                }
            }
            /*else if (guardType == GuardType.Villager)
            {
                awareness.talkingToPlayer = true;
                Game.DialogueManager.StartDialogue(new Dialogue("Villager", new List<string>()
            {
                    "Hey, what are you doing out here late at night???",
                    "Well I suppose you are fine. Mind if we chat for a bit?",
                    "(The villager talks your ear off for quite some time causing you to loose a bit of time.)"
            }.ToArray()));
                Game.PhaseTimer.subtractTime(15);
                awareness.talkingToPlayer = true;
            }*/
            else
            {
                //Not sure why you would end up here.
            }
        }
    }

    // dialogue exit options
    private void BeginDialogue(Dialogue d, Item i)
    {
        awareness.talkingToPlayer = true;
        inDialogue = true;
        exitPresses = 0;
        dialoguePressesExit = d.sentences.Length+1;
        itemToTake = i;
        Game.DialogueManager.StartDialogue(d);
    }

    private void ProgressDialogue()
    {
        if (InputControls.APressed)
        {
            exitPresses++;
            if (exitPresses >= dialoguePressesExit)
            {
                EndDialogue();
            }
        }
    }

    private void EndDialogue()
    {
        inDialogue = false;

        if (itemToTake != null)
        {
            // consume the dish
            dishConsumedName = itemToTake.Name;
            Game.Player.dishesInventory.Remove(itemToTake);
            Game.Player.activeItem = null;
            hasEaten = true;
        }
        else
        {
            // transport to outside the bakery
            Game.Player.position = GameObject.Find("BakeryOutsideRespawn").transform.position;
        }

        awareness.talkingToPlayer = false;
    }
}
