using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Menus;
using Assets.Scripts.QuestSystem.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameInput {
    public class getIng : MonoBehaviour
    {
        public GameObject arrow;
        public Dialogue pickUpText;
        /// <summary>
        /// Checks for interacting.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Game.TutorialCompleted == false) checkForTutorialInteraction(collision);
            else checkForNonTutorialInteraction(collision);

        }

        /// <summary>
        /// Checks for interacting with this during the tutorial.
        /// </summary>
        /// <param name="collision"></param>
        private void checkForTutorialInteraction(Collider2D collision)
        {
            if (arrow.GetComponent<progress>().step == 0)
            {
                arrow.GetComponent<SpriteRenderer>().enabled = false;
                arrow.GetComponent<progress>().A.SetActive(true);
            }
            if (InputControls.APressed && collision.gameObject.tag == "Player" && arrow.GetComponent<progress>().step == 0)
            {


                Menu.Instantiate<PantryMenuV2>();
                (Game.Menu as PantryMenuV2).initializeTutorialMode(this);

                /*
                FindObjectOfType<DialogueManager>().StartDialogue(pickUpText);
                collision.GetComponent<PlayerMovement>().NextStep();
                arrow.GetComponent<progress>().SetStep(1);
                arrow.GetComponent<SpriteRenderer>().enabled = true;
                arrow.GetComponent<progress>().A.SetActive(false);
                */
            }
        }
        /// <summary>
        /// Checks for interacting with this not during a tutorial.
        /// </summary>
        /// <param name="collision"></param>
        private void checkForNonTutorialInteraction(Collider2D collision)
        {
            if (InputControls.APressed && collision.gameObject.tag == "Player")
            {
                Menu.Instantiate<PantryMenuV2>();
            }
        }


        /// <summary>
        /// Checks when the player leaves the trigger hitbox.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Game.TutorialCompleted == false)
            {
                arrow.GetComponent<SpriteRenderer>().enabled = true;
                arrow.GetComponent<progress>().A.SetActive(false);
            }
        }

        
    }
}