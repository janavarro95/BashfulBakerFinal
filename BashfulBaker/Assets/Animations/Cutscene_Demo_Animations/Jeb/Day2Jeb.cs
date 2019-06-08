using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using Assets.Scripts.QuestSystem.Quests;

public class Day2Jeb : MonoBehaviour
{
    public GameObject DiaBoxReference;
    public Dialogue Jeb1;
    public Dialogue Dane1;
    public Dialogue Jeb2;
    public Sprite Jeb_Face;
    public Sprite Dane_Face;
    public Animator jeb_animator;
    private int step;
    public GameObject Bubble;




    // Start is called before the first frame update
    void Start()
    {
        if (Game.Day2JebTalkedTo)
        {
            gameObject.SetActive(false);
        }
        step = 0;
        Game.HUD.showAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step > 0)
        {
            if (step == 3)
            {
                jeb_animator.SetInteger("Movement_Phase", 3);
            }
            if (step == 2)
            {
                GameObject.Find("Headshot").GetComponent<Image>().sprite = Jeb_Face;
                FindObjectOfType<DialogueManager>().StartDialogue(Jeb2);
                step++;
            }
            if (step == 1)
            {
                GameObject.Find("Headshot").GetComponent<Image>().sprite = Dane_Face;
                FindObjectOfType<DialogueManager>().StartDialogue(Dane1);
                step++;
            }


        }


    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputControls.APressed && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && Game.Day2JebTalkedTo == false)
        {
            Game.Day2JebTalkedTo = true;
            Bubble.SetActive(false);
            GameObject.Find("Headshot").GetComponent<Image>().sprite = Jeb_Face;
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
            FindObjectOfType<DialogueManager>().StartDialogue(Jeb1);
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
        if (Game.CurrentDayNumber == 2)
        {
            Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookies", "Sylvia", new List<string>()));
            Game.QuestManager.addQuest(new CookingQuest("Mint Chip Cookies", "Sylvia", new List<string>()));
           // Game.QuestManager.addQuest(new CookingQuest("Oatmeal Raisin Cookies", "Lylia", new List<string>()));
            Game.HUD.showQuests = true;
            Game.StartNewTimerPhase(5, 0, true);
        }

    }
}
