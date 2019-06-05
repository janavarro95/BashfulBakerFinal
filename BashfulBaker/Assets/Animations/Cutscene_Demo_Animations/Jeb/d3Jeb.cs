using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using Assets.Scripts.QuestSystem.Quests;


public class d3Jeb : MonoBehaviour
{
    public GameObject DiaBoxReference;
    public Dialogue[] backandforth;
    public Sprite[] headshots;
    //public Dialogue Jebs_Warning;
    public Animator jeb_animator;
    private int step;
    public GameObject Bubble;
    private bool waitingtoend;

    void Start()
    {
        waitingtoend = false;
        if (Game.Day3JebTalkedTo || Game.Player.PlayerMovement.currentStep > 0)
        {
            gameObject.SetActive(false);
        }
        step = 0;
        // Game.HUD.showAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step < backandforth.Length && step > 0)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(backandforth[step]);
            GameObject.Find("Headshot").GetComponent<Image>().sprite = headshots[step];

            step++;
            Debug.Log(step);

        }
        else if (step == backandforth.Length)
        {
            jeb_animator.SetInteger("Movement_Phase", 3);
        }
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputControls.APressed && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false)
        {
            Game.Day3JebTalkedTo = true;
            Bubble.SetActive(false);
            GameObject.Find("Headshot").GetComponent<Image>().sprite = headshots[0];
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
            FindObjectOfType<DialogueManager>().StartDialogue(backandforth[step]);
            step++;
        }
    }
    private void increasestep()
    {
        step++;
    }
    private void freeDane()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
    }
    void Jeb_disappear()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
        this.gameObject.SetActive(false);
        Game.HUD.showHUD = true;
        Game.HUD.showQuests = true;
        if (Game.CurrentDayNumber == 1)
        {
            Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookies", "Sylvia", new List<string>()));
            Game.QuestManager.addQuest(new CookingQuest("Mint Chip Cookies", "Ian", new List<string>()));
            Game.QuestManager.addQuest(new CookingQuest("Oatmeal Raisin Cookies", "Brian", new List<string>()));
            Game.QuestManager.addQuest(new CookingQuest("Pecan Cookies", "Amari", new List<string>()));
            Game.HUD.QuestHUD.setUpMenuForDisplay();
            //Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookies", "Sylvia", new List<string>()));
            Game.HUD.showQuests = true;
            // Game.StartNewTimerPhase(10, 0, true);
        }


    }
}
