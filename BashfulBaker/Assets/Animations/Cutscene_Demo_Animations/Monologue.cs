using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite Dane_Face;
    public Dialogue First;
    public Dialogue Second;
    public Dialogue Third;
    private bool canCount;
    private int ApressCount;
    private bool check_one;
    private bool check_two;
    private bool check_three;
    private bool beginable;

    void Start()
    {
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().currentStep != -1)
        {
            this.gameObject.SetActive(false);
        }
        canCount = false;
        ApressCount = 0;
        check_one = false;
        check_two = false;
        check_three = false;
        beginable = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (InputControls.APressed && canCount == true)
        {
            ApressCount++;
            Debug.Log(ApressCount);
        }
        if(ApressCount >= 1 & check_one == false && beginable == false)
        {
            canCount = false;
            ApressCount = 0;
            check_one = true;
            StartCoroutine(WaitforTime(2, Second));

        }
        if (check_one == true && check_two == false && ApressCount >= 2)
        {
            canCount = false;
            ApressCount = 0;
            check_two = true;
            StartCoroutine(WaitforTime(2, Third));

        }
      /*  if (check_one == true && check_two == true && ApressCount >= 2)
        {
            canCount = false;
            ApressCount = 0;
            check_two = true;
            WaitforTime(3, Third);
        }*/

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (beginable == true)
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = Dane_Face;
            FindObjectOfType<DialogueManager>().StartDialogue(First);
            canCount = true;
            beginable = false;
        }
    }
    IEnumerator WaitforTime(int seconds, Dialogue dialogue)
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        canCount = true;
        if(dialogue == Third)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().NextStep();
        }
    }
}
