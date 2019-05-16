using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DiaBoxReference;
    public Sprite[] face;
    public Dialogue[] dias;
    private int step;
    private bool flag;

    void Start()
    {
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().currentStep != -1)
        {
            this.gameObject.SetActive(false);
        }
        step = 0;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false && step > 0)
        {
            if (flag && DiaBoxReference.GetComponent<DialogueManager>().IsDialogueUp == false)
            {
                saysomething(2);
                flag = false;
            }
        }
        if (step == 3)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().NextStep();
            GameObject.Find("Arrow").SetActive(false);
            GameObject.Find("pantry").SetActive(false);
            GameObject.Find("FakeShit").GetComponent<SpriteRenderer>().enabled = true;
            step++;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (step == 0)
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = face[step];
            FindObjectOfType<DialogueManager>().StartDialogue(dias[step]);
            step++;
            flag = true;
        }
    }

    public void saysomething(float seconds)
    {
        Invoke("starttalking", seconds);

    }

    public void starttalking()
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = face[step];
        FindObjectOfType<DialogueManager>().StartDialogue(dias[step]);
        step++;
        flag = true;
        Debug.Log(step);

    }


}
