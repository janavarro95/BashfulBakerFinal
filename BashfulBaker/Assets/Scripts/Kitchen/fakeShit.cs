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
        public GameObject pantry, arrow, bag;
        public Dialogue whereAreThey, hereTheyAre;

        private void Start()
        {
            c = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (InputControls.APressed && collision.gameObject.tag == "Player" && GetComponent<SpriteRenderer>().enabled)
            {
                if (c)
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(whereAreThey);
                    transform.position = new Vector3(bag.transform.position.x, bag.transform.position.y + 1, -2);
                    c = false;
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
