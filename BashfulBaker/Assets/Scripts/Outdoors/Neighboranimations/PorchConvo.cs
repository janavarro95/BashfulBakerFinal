using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.QuestSystem.Quests;
using Assets.Scripts.Items;

public class PorchConvo : MonoBehaviour
{

    public Dialogue[] convo;
    public Sprite[] faces;
    public Dialogue wrongCookies;
    public string deliveryOBJ;
    public Sprite poutingboy;
    public SpriteRenderer Neighbor_Sprite;
    public GameObject DiaBoxReference;
    private int step;
    private bool realquestrecieved;

    void Start()
    {
        realquestrecieved = false;
        Neighbor_Sprite.enabled = false;
        step = 0;
    }

    void Update()
    {
        if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step > 0 && step < convo.Length)
        {   
            GameObject.Find("Headshot").GetComponent<Image>().sprite = faces[step];
            FindObjectOfType<DialogueManager>().StartDialogue(convo[step]);
            step++;
        }
        else if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step == convo.Length)
        {
            Neighbor_Sprite.enabled = false;
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
            finishEndOfDay();
            step++;
        }

    }
    void finishEndOfDay()
    {    
        if (Game.CurrentDayNumber == 1 && Game.QuestManager.completedAllQuests())
        {
            Game.HUD.showHUD = true;
            Game.PhaseTimer.resume();
            //Game.PhaseTimer.currentTime = Game.PhaseTimer.maxTime;
            promptToEndDay();

        }else if (Game.CurrentDayNumber == 2 && Game.QuestManager.containsQuest("Amari", "Mint Chip Cookies")==false)
        {
            Game.QuestManager.removeQuest("Sylvia", "Mint Chip Cookies");
            Game.QuestManager.addQuest(new CookingQuest("Mint Chip Cookies", "Amari", new List<string>()));

            //Dish d = new Dish(Enums.Dishes.MintChipCookies, Enums.DishState.Packaged);
            //Game.Player.dishesInventory.Add(d);

            Debug.Log(Game.QuestManager.quests.Count);
            Game.HUD.QuestHUD.updateForTheDay();
            Game.PhaseTimer.resume();
            Game.HUD.showHUD = true;
            realquestrecieved = true;
        }
        else if (Game.CurrentDayNumber == 2 && Game.QuestManager.completedAllQuests() && realquestrecieved == true)
        {
            Game.HUD.showHUD = true;
            Game.PhaseTimer.resume();
            //Game.PhaseTimer.currentTime = Game.PhaseTimer.maxTime;
            promptToEndDay();
        }

    }

    private void promptToEndDay()
    {

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        if (Game.Player.activeItem != null)
        {
            if (Game.Player.activeItem.Name == deliveryOBJ && (Game.Player.activeItem as Dish).currentDishState == Enums.DishState.Packaged && step == 0 && Game.DialogueManager.IsDialogueUp == false && (Game.Player.activeItem as Dish).IsDishComplete)
            {
                GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
                Neighbor_Sprite.enabled = true;
                GameObject.Find("Headshot").GetComponent<Image>().sprite = faces[step];
                FindObjectOfType<DialogueManager>().StartDialogue(convo[step]);
                Game.HUD.showHUD = false;
                Game.PhaseTimer.pause();
                step++;
            }

        }
    }

   /* private void OnTriggerEnter2D(Collision2D collision)
    {
        if (step == 0 && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && )
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = poutingboy;
            FindObjectOfType<DialogueManager>().StartDialogue(wrongCookies);
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        Game.HUD.showHUD = true;
        Game.PhaseTimer.resume();
    }
}