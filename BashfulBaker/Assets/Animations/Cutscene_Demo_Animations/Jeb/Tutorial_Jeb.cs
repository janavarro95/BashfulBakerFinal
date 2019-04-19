using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;



public class Tutorial_Jeb : MonoBehaviour
{
    public Sprite Jeb_Face;
    public Sprite Jeb_Face_surprised;
    public Animator jeb_animator;
    private int ApressCount;
    public Dialogue tutorial_lines_pt1;
    public Dialogue tutorial_lines_pt2;
    public Dialogue tutorial_lines_pt3;
    private bool check_one;
    private bool check_two;
    private bool check_three;
    public bool canCount;
    public GameObject Bubble;




    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().currentStep >= 0)
        {
            this.gameObject.SetActive(false);
        }
        canCount = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (InputControls.APressed && canCount == true)
        {
            ApressCount++;
            //Debug.Log(ApressCount);
        }
        if (ApressCount >= 3 && check_three == true && jeb_animator.GetInteger("Movement_Phase") == 2)
        {
            End_Count();
            Jeb_disappear();
        }
        if (ApressCount >= 5 && check_two == true && jeb_animator.GetInteger("Movement_Phase") == 1)
        {
            jeb_animator.SetInteger("Movement_Phase", 2);
            End_Count();
            check_three = true;
        }

        if (ApressCount >= 5 && check_one == true && jeb_animator.GetInteger("Movement_Phase") == 0)
        {
            jeb_animator.SetInteger("Movement_Phase", 1);
            End_Count();
            check_two = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (InputControls.APressed && check_one == false)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
            Introduction();
            check_one = true;
            Begin_Count();
        }




    }


    void Bye()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(tutorial_lines_pt3);
    }
    void Introduction()
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = Jeb_Face_surprised;
        //GameObject.Find("speechBubble")
            Bubble.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(tutorial_lines_pt1);
    }
    void Mission()
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = Jeb_Face;
        FindObjectOfType<DialogueManager>().StartDialogue(tutorial_lines_pt2);
    }
    void Jeb_disappear()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1;
        this.gameObject.SetActive(false);
    }
    void Begin_Count()
    {
        ApressCount = 0;
        canCount = true;
    }
    void End_Count()
    {
        ApressCount = 0;
        canCount = false;
    }
    void turn_Dane()
    {
        Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
        //GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite = ;
    }
    void turn_Dane_Down()
    {
        Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Down;
    }
}
