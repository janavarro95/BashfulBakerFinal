using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Success : MonoBehaviour
{
    public Dialogue VictorySpeech;
    public Sprite daneFace;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Headshot").GetComponent<Image>().sprite = daneFace;
        FindObjectOfType<DialogueManager>().StartDialogue(VictorySpeech);
    }
}
