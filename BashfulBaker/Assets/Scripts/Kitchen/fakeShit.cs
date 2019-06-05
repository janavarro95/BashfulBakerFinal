using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Menus;
using Assets.Scripts.QuestSystem.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameInput
{
    public class fakeShit : MonoBehaviour
    {
        private bool c;
        public GameObject pantry, arrow, bag, A;
        public Dialogue whereAreThey, hereTheyAre;

        private void Start()
        {
            c = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!(GetComponent<SpriteRenderer>().enabled || GetComponent<SpriteRenderer>().enabled)) return;
            GetComponent<SpriteRenderer>().enabled = false;
            A.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!(GetComponent<SpriteRenderer>().enabled || GetComponent<SpriteRenderer>().enabled)) return;
            GetComponent<SpriteRenderer>().enabled = true;
            A.GetComponent<SpriteRenderer>().enabled = false;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (InputControls.APressed && collision.gameObject.tag == "Player" && (GetComponent<SpriteRenderer>().enabled || A.GetComponent<SpriteRenderer>().enabled))
            {
                if (c)
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(whereAreThey);
                    transform.position = new Vector3(bag.transform.position.x, bag.transform.position.y + 1, -2);
                    c = false;
                    GetComponent<SpriteRenderer>().enabled = true;
                    A.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(hereTheyAre);
                    pantry.SetActive(true);
                    arrow.SetActive(true);
                    gameObject.SetActive(false);
                }
                
            }
        }
    }
}
