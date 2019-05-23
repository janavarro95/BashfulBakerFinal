using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using Assets.Scripts.QuestSystem.Quests;

public class Tutorial_Jeb : MonoBehaviour
{
    public GameObject DiaBoxReference;
    public Dialogue[] backandforth;
    public Sprite[] headshots;
    public Dialogue Jebs_Warning; 
    public Animator jeb_animator;
    private int step;
    public GameObject Bubble;
    private bool waitingtoend;



    // Start is called before the first frame update
    void Start()
    {
        waitingtoend = false;
        if (Game.Day1JebTalkedTo || Game.Player.PlayerMovement.currentStep > 0)
        {
            gameObject.SetActive(false);
        }
        step = 0;
       // Game.HUD.showAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step <16 && step>0)
        {
                GameObject.Find("Headshot").GetComponent<Image>().sprite = headshots[step];
                FindObjectOfType<DialogueManager>().StartDialogue(backandforth[step]);
                step++;
            Debug.Log(step);

        }else if (step == 16)
        {
            jeb_animator.SetInteger("Movement_Phase", 4);
        }


        if (waitingtoend && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step == 17)
        {
            freeDane();
            Jeb_disappear();
        }


    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputControls.APressed && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false)
        {
            Game.Day1JebTalkedTo = true;
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
            Game.HUD.showQuests = true;
           // Game.StartNewTimerPhase(10, 0, true);
        }
        

    }
    public void warning()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Jebs_Warning);
        waitingtoend = true;
        step++;
    }
}