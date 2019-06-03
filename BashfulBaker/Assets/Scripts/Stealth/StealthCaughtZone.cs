using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Utilities;
using Assets.Scripts.GameInput;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthCaughtZone : MonoBehaviour
{
    public StealthAwarenessZone awareness;
    public Dialogue dialogue;
    private Dialogue specialDialogue;
    public Sprite guardFace;
    public Item itemToTake;
    public Material pacMat;
    public DialogueManager dm;
    private GameObject breathing;
    private GuardRamber ramble;
    private bool rambleReady = true;
    private bool endDia = true;

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


    private string _guardUniqueID;
    public string GuardID
    {
        get
        {
            return _guardUniqueID;
        }
    }

    private void Awake()
    {
        this.awareness = this.gameObject.GetComponent<StealthAwarenessZone>();
        specialDialogue = dialogue;

        breathing = GameObject.Find("bar");
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.gameObject.transform.position;
        _guardUniqueID = pos.x + "_" + pos.y + "_" + pos.z;


        if (dm == null)
            dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();


        BreathingMinigame(false);

        ramble = GameObject.FindGameObjectWithTag("Ramblings").GetComponent<GuardRamber>();
    }

    // update
    private void Update()
    {
        if (Game.HasGuardBeenFed(this.GuardID) && this.hasEaten==false)
        {
            Pacify();
        }

        if (inDialogue)
        {
            ProgressDialogue();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (pm == null)
            return;

        if (!pm.hidden)
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = guardFace;
            Debug.Log("YOU GOT CAUGHT!");

            // enable the minigame
            BreathingMinigame(true);

            // take the things
            pm.currentStep = 0;
            if (this.guardType == GuardType.Guard)
            {
                if (hasEaten == false)
                {
                    Debug.Log("---this guard has not been fed");

                    if (Game.Player.dishesInventory.getAllDishes().Count > 0)
                    {

                        //Item item = (Game.Player.activeItem != null && (Game.Player.activeItem as Dish).IsDishComplete ? Game.Player.activeItem : Game.Player.dishesInventory.getRandomBoxedDish());
                        Item item = (Game.Player.dishesInventory.getRandomBoxedDish());
                        
                        if ((item as Dish).IsDishComplete)
                        {
                            dialogue = new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                            {
                                "*Sniff sniff* Ohh that {0} looks delicious!"
                            }, item.Name).ToArray());

                            BeginDialogue(dialogue, item);

                        }
                        else
                        {
                            dialogue = new Dialogue("Guard", new List<string>()
                            {
                                "You can't be out this late! You should really get back home!"
                            }.ToArray());

                            BeginDialogue(dialogue, null);
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
                                "You can't be out this late! You should really get back home!"
                            }.ToArray());

                            BeginDialogue(dialogue, null);
                        }
                        return;
                    }
                }
            }
            else
            {
                if (!inDialogue)
                {
                    BeginDialogue(specialDialogue, null);
                }
                return;
            }
        }
    }

    // dialogue exit options
    private void BeginDialogue(Dialogue d, Item i)
    {
        // adjust awareness
        awareness.talkingToPlayer = true;
        inDialogue = true;

        // start dialog
        Game.DialogueManager.StartDialogue(d);

        // take item
        itemToTake = i;

        // stop the player
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanPlayerMove = false;

        // remove hud
        Game.HUD.showInventory = false;
        Game.HUD.showHUD = false;
    }

    private void ProgressDialogue()
    {
        if (this.guardType == GuardType.Guard)
        {
            if (breathing.GetComponentInChildren<Breathe>().isFinished())
            {
                if (!dm.IsDialogueUp)
                    EndDialogue();
                else if (endDia)
                {
                    if (itemToTake != null && !String.IsNullOrEmpty(itemToTake.Name))
                    {
                        // make new dialog
                        dialogue = new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                        {"Thank you for the {0}, have a lovely night!"},
                        itemToTake.Name).ToArray());

                        // start new dialog
                        Game.DialogueManager.StartDialogue(dialogue);
                        endDia = false;
                    }
                    else
                    {
                        // make new dialog
                        dialogue = new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                        {"I'll personally escort you!"},
                        "NOTHING").ToArray());

                        // start new dialog
                        Game.DialogueManager.StartDialogue(dialogue);
                        endDia = false;
                    }
                }
            }
            else if (!dm.IsDialogueUp)
            {
                Ramble();
            }
            else if (dm.sentenceFinished)
            {
                if (rambleReady)
                    StartCoroutine(WaitToEndDialogue(2f));
            }
        }
        else EndDialogue();
    }

    private void Ramble()
    {
        // make new dialog
        dialogue = new Dialogue("Guard", StringUtilities.FormatStringList(new List<string>()
                {ramble.NextRamble()},
        (itemToTake != null ? itemToTake.Name : "NOTHING")).ToArray());

        // start new dialog
        Game.DialogueManager.StartDialogue(dialogue);
    }

    private IEnumerator WaitToEndDialogue(float time)
    {
        rambleReady = false;
        yield return new WaitForSeconds(time);
        dm.IsDialogueUp = false;
        rambleReady = true;
    }

    private void EndDialogue()
    {
        inDialogue = false;
        endDia = true;
        awareness.talkingToPlayer = false;
        if (guardType == GuardType.Guard)
        { 
            if (itemToTake != null && !String.IsNullOrEmpty(itemToTake.Name))
            {
                TakeItemAway();
                Game.Player.PlayerMovement.Escaped();
                awareness.investigate = null;
            }
            else
            {
                // transport to outside the bakery
                Game.Player.position = GameObject.Find("BakeryOutsideRespawn").transform.position;
                Game.StealthManager.caughtByGuard();
                awareness.investigate = null;
                Game.Player.PlayerMovement.EscapedReset();
            }
        }

        // start player movement
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanPlayerMove = true;

        // stop breathing
        BreathingMinigame(false);        
        
        // bring back hud
        Game.HUD.showInventory = true;
        Game.HUD.showHUD = true;
    }

    // Pacify makes the guard useless
    private void Pacify()
    {
        Debug.Log("Pacifying");
        if (!hasEaten)
        {
            // adjust light
            awareness.flashlight.viewMeshFilter.GetComponent<MeshRenderer>().material = pacMat;
            awareness.flashlight.DrawFieldOfView();

            // slow me down
            awareness.movementSpeed /= 1.5f;
            awareness.capturePatrolPoint = this.transform.position;
            awareness.movementLerp = 0.0f;

            // remove spots
            awareness.spotsToGoTo.Clear();
            awareness.investigate = null;

            // send me home
            awareness.returnHome = true;

            // disable this collision
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }

        hasEaten = true;
    }

    // take awy the previously saved item
    private void TakeItemAway()
    {
        // consume the dish
        if (guardType == GuardType.Guard)
        {
            Pacify();
            dishConsumedName = itemToTake.Name;
            Game.Player.dishesInventory.Remove(itemToTake);
            Game.Player.activeItem = null;
            Game.CaughtByGuard(this.GuardID, dishConsumedName);
        }
    }

    // breathing minigame
    void BreathingMinigame(bool activate)
    {
        if (breathing == null)
            return;

        if (!activate)
        {
            Breathe bb = breathing.GetComponentInChildren<Breathe>();
            bb.progress = 0;
            bb.pbar.transform.localScale = new Vector3(0, bb.pbar.transform.localScale.y, 1);
        }

        if (this.tag == "Guard")
            breathing.SetActive(activate);
    }
}
