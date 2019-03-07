using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;

public class flavorText : MonoBehaviour
{
    private int aCount;
    private bool openable;
    public Dialogue flavor;
    public Sprite Dane_Face;
    //public Sprite Jeb_Face; 


    // Start is called before the first frame update
    void Start()
    {
        openable = true;
      //  aCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (InputControls.APressed && openable == false)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 1;
            //GameObject.Find("Headshot").GetComponent<Image>().sprite = Jeb_Face;
        }*/

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (InputControls.APressed && openable)
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = Dane_Face;
           // GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0;
            FindObjectOfType<DialogueManager>().StartDialogue(flavor);
           openable = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        openable = true;
    }
}

