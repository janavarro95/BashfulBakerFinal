using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;

public class Success : MonoBehaviour
{
    public Dialogue VictorySpeech;
    public Dialogue wrongCookies;
    public Sprite daneFace;
    public Sprite poutingboy;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Game.Player.activeItem.Name == "Chocolate Chip Cookies")
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = daneFace;
            FindObjectOfType<DialogueManager>().StartDialogue(VictorySpeech);
        }
        else
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = poutingboy;
            FindObjectOfType<DialogueManager>().StartDialogue(wrongCookies);
        }
    }
}
