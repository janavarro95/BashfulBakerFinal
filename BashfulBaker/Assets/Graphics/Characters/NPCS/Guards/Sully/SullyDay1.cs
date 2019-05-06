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
    public Dialogue Sully_Hey;
    public Dialogue Dane_Thanks;
    public Dialogue Sully_Bye;


    private void Start()
    {

        step = 0;

    }
    private void Update()
    {
        isOpen = DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp;

        if (isOpen ==false && step > 0){
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
                GameObject.Find("Headshot").GetComponent<Image>().sprite = dane_Face;
                Dane_Speaks();
                step++;
            }
 

        }


        if (trigger.GetComponent<triggercheck>().beenHit == true && step == 0 && isOpen == false)
        {
           // step++;
            sully_animator.SetInteger("Phase", 1);
   

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
        FindObjectOfType<DialogueManager>().StartDialogue(Sully_Hey);
    }
    void Dane_Speaks()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dane_Thanks);
    }
    void Sully_Byebye()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Sully_Bye);
    }
    void kill_Sully()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
        this.gameObject.SetActive(false);
        Destroy(trigger);
    }





    // Start is called before the first frame update
    /*  void Start()
      {
          canCount = false;
          step = 0;
          triggered = trigger.GetComponent<triggercheck>().beenHit;
      }

      // Update is called once per frame
      void Update()
      {

          if (InputControls.APressed && canCount == true)
          {
              ApressCount++;
              //Debug.Log(ApressCount);
          }

          if (triggered && step == 0 && arrived == true)
          {

              GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
              killtrigger();
              Begin_Count();
              step++;
          }
          if (step ==1 && ApressCount == 4)
          {
              End_Count();


          }
      }

      void endAmination()
      {
          triggered = false;
      }
      void killtrigger()
      {
          Destroy(GameObject.Find("triggercheck"));
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
      void release_Dane()
      {
          GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1.25f;
      }

      void madeIt()
      {
          arrived = true;
      }
  }
  */
}