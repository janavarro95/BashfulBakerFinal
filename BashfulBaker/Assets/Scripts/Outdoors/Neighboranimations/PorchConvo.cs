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
    public Sprite daneFace;
    public Sprite poutingboy;
    public SpriteRenderer Neighbor_Sprite;
    //public Animator Neighbor_animator;
    public GameObject DiaBoxReference;
    private int step;

    void Start()
    {
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
        else if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step >= convo.Length)
        {
            Neighbor_Sprite.enabled = false;
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Game.Player.activeItem.Name == deliveryOBJ)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
            Neighbor_Sprite.enabled = true;
            GameObject.Find("Headshot").GetComponent<Image>().sprite = faces[step];
            FindObjectOfType<DialogueManager>().StartDialogue(convo[step]);
            step++;
        }
        else
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = poutingboy;
            FindObjectOfType<DialogueManager>().StartDialogue(wrongCookies);
        }
    }
}