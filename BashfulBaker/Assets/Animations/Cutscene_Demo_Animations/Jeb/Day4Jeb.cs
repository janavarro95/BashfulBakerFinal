using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using Assets.Scripts.QuestSystem.Quests;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utilities.Delegates;
using Assets.Scripts.Utilities;


public class Day4Jeb : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (Game.Day4JebTalkedTo)
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
        else if (step == backandforth.Length && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false)
        {
            // jeb_animator.SetInteger("Movement_Phase", 3);
            // GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
            Game.Player.setSpriteVisibility(Assets.Scripts.Enums.Visibility.Invisible);
            ScreenTransitions.StartSceneTransition(2, "Credits", Color.black, ScreenTransitions.TransitionState.FadeOut);

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
            Game.HUD.showHUD = false;
            Game.HUD.showQuests = false;   
            step++;
        }
    }
}
