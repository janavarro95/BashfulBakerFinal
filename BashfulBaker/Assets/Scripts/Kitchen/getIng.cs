﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameInput {
    public class getIng : MonoBehaviour
    {
        public GameObject arrow;
        public Dialogue pickUpText;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (arrow.GetComponent<progress>().step == 0)
            {
                arrow.GetComponent<SpriteRenderer>().enabled = false;
                arrow.GetComponent<progress>().A.SetActive(true);
            }
            if (InputControls.APressed && collision.gameObject.tag == "Player" && arrow.GetComponent<progress>().step == 0)
            {
                Debug.Log("Picked up ingredients");
                FindObjectOfType<DialogueManager>().StartDialogue(pickUpText);
                collision.GetComponent<PlayerMovement>().NextStep();
                arrow.GetComponent<progress>().SetStep(1);
                arrow.GetComponent<SpriteRenderer>().enabled = true;
                arrow.GetComponent<progress>().A.SetActive(false);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = true;
            arrow.GetComponent<progress>().A.SetActive(false);
        }
    }
}