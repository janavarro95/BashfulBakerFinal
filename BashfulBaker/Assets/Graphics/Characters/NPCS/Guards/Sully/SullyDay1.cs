using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;


public class SullyDay1 : MonoBehaviour
{

    public GameObject trigger;
    private bool triggered;
    private bool arrived;
    private int ApressCount;
    private bool canCount;
    private int step;
    public Dialogue Sully_Hey;
    public Dialogue Dane_Thanks;
    public Dialogue Sully_Bye;

    // Start is called before the first frame update
    void Start()
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
    void madeIt()
    {
        arrived = true;
    }
}
