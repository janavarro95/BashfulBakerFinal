using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;




public class SullyDay1 : MonoBehaviour
{

    public GameObject trigger;
    public GameObject DiaBoxReference;
    public Animator sully_animator;
    public Sprite sully_Face;
    public Sprite dane_Face;
    public bool isOpen;
    public int step;
    public Dialogue StopRightThere;
    public Dialogue Sully_Bye;
    public Dialogue[] Conversation;
    private int convosteps;
    public Sprite[] convofaces;


    private void Start()
    {
        if (Game.TalkedtoSully || Game.CurrentDayNumber != 1)
        {
            gameObject.SetActive(false);
            Destroy(trigger);
        }
        GameObject.Find("Headshot").GetComponent<Image>().sprite = sully_Face;
        step = 0;
    }
    private void Update()
    {
        isOpen = DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp;

        if (isOpen == false && step > 0)
        {
            if (step == 3)
            {
                sully_animator.SetInteger("Phase", 3);
            }
            if (step == 2)
            {
                GameObject.Find("Headshot").GetComponent<Image>().sprite = sully_Face;
                Sully_Byebye();
                step++;
            }
            if (step == 1)
            {
                //GameObject.Find("Headshot").GetComponent<Image>().sprite = dane_Face;

                if (convosteps == 7)
                {
                    step++;
                }
                else
                {
                    Dane_Speaks();
                }
            }

        }
        if (trigger.GetComponent<triggercheck>().beenHit == true && step == 0 && isOpen == false)
        {
            sully_animator.SetInteger("Phase", 1);
            Game.TalkedtoSully = true;
            Game.PhaseTimer.pause();
        }
    }


    private void endApproach()
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = sully_Face;
        sully_animator.SetInteger("Phase", 2);
        Debug.Log("set");
    }
    private void increasestep()
    {
        step++;
    }
    void Getattention()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(StopRightThere);
    }
    void Sully_Speaks()
    {
        Dane_Speaks();
    }
    void Dane_Speaks()
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = convofaces[convosteps];
        FindObjectOfType<DialogueManager>().StartDialogue(Conversation[convosteps]);
        convosteps++;
        Debug.Log(convosteps);
    }
    void Sully_Byebye()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Sully_Bye);
    }
    void kill_Sully()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
        Game.PhaseTimer.resume();
        this.gameObject.SetActive(false);
        Destroy(trigger);
    }
}
