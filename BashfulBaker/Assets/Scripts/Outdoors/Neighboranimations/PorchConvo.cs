using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.QuestSystem.Quests;


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
            Game.PhaseTimer.currentTime = Game.PhaseTimer.maxTime;

        }else if (Game.CurrentDayNumber == 2 && Game.QuestManager.completedAllQuests() == false)
        {
            Game.QuestManager.addQuest(new CookingQuest("Mint Chip Cookies", "Amari", new List<string>()));
            Game.QuestManager.quests.RemoveAt(1);
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
            Game.PhaseTimer.currentTime = Game.PhaseTimer.maxTime;
        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (Game.Player.activeItem.Name == deliveryOBJ && step==0)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
            Neighbor_Sprite.enabled = true;
            GameObject.Find("Headshot").GetComponent<Image>().sprite = faces[step];
            FindObjectOfType<DialogueManager>().StartDialogue(convo[step]);
            Game.HUD.showHUD = false;
            Game.PhaseTimer.pause();
            step++;
        }
        else if (step ==0)
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = poutingboy;
            FindObjectOfType<DialogueManager>().StartDialogue(wrongCookies);
        }
    }
}