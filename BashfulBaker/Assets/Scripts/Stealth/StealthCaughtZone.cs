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

                if (Game.Player.activeItem != null && (Game.Player.activeItem is Dish))
                {
                    Game.DialogueManager.StartDialogue(new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                {
                    "Hey, what are you doing out here late at night???",
                    "*Sniff sniff* Ohh that {0} looks delicious!",
                    "Is that for me? Thanks! Now be carefull this late at night!"
                }, Game.Player.activeItem.Name).ToArray()));

                    Game.Player.inventory.Remove(Game.Player.activeItem);
                    Game.Player.activeItem = null;
                    awareness.shouldMove = true;
                    return;
                }
                else if (Game.Player.inventory.getAllDishes().Count > 0)
                {
                    awareness.shouldMove = false;
                    Item I = Game.Player.inventory.getRandomDish();
                    Game.DialogueManager.StartDialogue(new Dialogue("Guard", Assets.Scripts.Utilities.StringUtilities.FormatStringList(new List<string>()
                {
                    "Hey, what are you doing out here late at night???",
                    "*Sniff sniff* Ohh that {0} looks delicious!",
                    "Is that for me? Thanks! Now be carefull this late at night!"
                }, I.Name).ToArray()));

                    awareness.shouldMove = true;
                    Game.Player.inventory.Remove(I);
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
                awareness.shouldMove = true;
            }
        }
    }

    
}
