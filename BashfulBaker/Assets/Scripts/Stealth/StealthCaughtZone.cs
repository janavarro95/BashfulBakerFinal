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
    public Material pacMat;
    public DialogueManager dm;

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
        if (dm == null)
            dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
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
        // adjust awareness
        awareness.talkingToPlayer = true;
        inDialogue = true;
        // determine number of presses to exit
        exitPresses = 0;
        dialoguePressesExit = d.sentences.Length+1;
        // start dialog and take item
        Game.DialogueManager.StartDialogue(d);
        itemToTake = i;
        if (itemToTake != null)
        {
            TakeItemAway();
        }
        // stop the player
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanPlayerMove = false;
    }

    private void ProgressDialogue()
    {
        if (!dm.IsDialogueUp)
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        inDialogue = false;
        awareness.talkingToPlayer = false;

        if (itemToTake != null)
        {
            Pacify();
        }
        else
        {
            // transport to outside the bakery
            Game.Player.position = GameObject.Find("BakeryOutsideRespawn").transform.position;
            Pacify();
        }

        // start player movement
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanPlayerMove = true;
    }

    // Pacify makes the guard useless
    private void Pacify()
    {
        // adjust light
        awareness.flashlight.viewRadius /= 2;
        awareness.flashlight.viewMeshFilter.GetComponent<MeshRenderer>().material = pacMat;
        awareness.flashlight.DrawFieldOfView();

        // slow me down
        awareness.lookSpeed /= 2;
        awareness.movementSpeed /= 2;
        hasEaten = true;

        // disable this collision
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    // take awy the previously saved item
    private void TakeItemAway()
    {
        // consume the dish
        dishConsumedName = itemToTake.Name;
        Game.Player.dishesInventory.Remove(itemToTake);
        Game.Player.activeItem = null;
    }
}
