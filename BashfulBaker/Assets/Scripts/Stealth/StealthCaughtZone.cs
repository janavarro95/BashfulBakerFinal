using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCaughtZone : MonoBehaviour
{
    public StealthAwarenessZone awareness;
    public Dialogue dialogue;


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

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerMovement>().hidden)
        {
            Debug.Log("YOU GOT CAUGHT!");

            if (this.guardType == GuardType.Guard)
            {

                awareness.shouldMove = false;

                if (Game.Player.activeItem != null && (Game.Player.activeItem is Dish) && hasEaten == false)
                {
                    Game.DialogueManager.StartDialogue(new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                {
                    "Hey, what are you doing out here late at night???",
                    "*Sniff sniff* Ohh that {0} looks delicious!",
                    "Is that for me? Thanks! Now be carefull this late at night!"
                }, Game.Player.activeItem.Name).ToArray()));

                    dishConsumedName = Game.Player.activeItem.Name;
                    Game.Player.dishesInventory.Remove(Game.Player.activeItem);
                    Game.Player.activeItem = null;
                    awareness.shouldMove = true;
                    hasEaten = true;
                    return;
                }
                else if (Game.Player.dishesInventory.getAllDishes().Count > 0 && hasEaten == false)
                {
                    awareness.shouldMove = false;
                    Item I = Game.Player.dishesInventory.getRandomDish();
                    Game.DialogueManager.StartDialogue(new Dialogue("Guard", Assets.Scripts.Utilities.StringUtilities.FormatStringList(new List<string>()
                {
                    "Hey, what are you doing out here late at night???",
                    "*Sniff sniff* Ohh that {0} looks delicious!",
                    "Is that for me? Thanks! Now be carefull this late at night!"
                }, I.Name).ToArray()));
                    dishConsumedName = I.Name;
                    awareness.shouldMove = true;
                    Game.Player.dishesInventory.Remove(I);
                    hasEaten = true;
                    return;
                }
                else if (hasEaten == true)
                {
                    awareness.shouldMove = false;
                    Game.DialogueManager.StartDialogue(new Dialogue("Guard",StringUtilities.FormatStringList(new List<string>()
                {
                    "Hey there, your {0} you gave me was delicious! When can you make me some more!?",
                    "(You ended up getting caught up in converstation for quite some time.)"
                },dishConsumedName).ToArray()));
                    
                    Game.PhaseTimer.subtractTime(15);
                    //Game.Player.position = GameObject.Find("BakeryOutsideRespawn").transform.position;
                    awareness.shouldMove = true;
                }
                else
                {
                    awareness.shouldMove = false;
                    Game.DialogueManager.StartDialogue(new Dialogue("Guard", new List<string>()
                {
                    "Hey, what are you doing out here late at night???",
                    "You can't be out this late! Let me escort you back home!"
                }.ToArray()));
                    Game.Player.position = GameObject.Find("BakeryOutsideRespawn").transform.position;
                    awareness.shouldMove = true;
                    return;
                }
            }
            else if (guardType == GuardType.Villager)
            {
                awareness.shouldMove = false;
                Game.DialogueManager.StartDialogue(new Dialogue("Villager", new List<string>()
            {
                    "Hey, what are you doing out here late at night???",
                    "Well I suppose you are fine. Mind if we chat for a bit?",
                    "(The villager talks your ear off for quite some time causing you to loose a bit of time.)"
            }.ToArray()));
                Game.PhaseTimer.subtractTime(15);
                awareness.shouldMove = true;
            }
            else
            {
                //Not sure why you would end up here.
            }
        }
    }
}
