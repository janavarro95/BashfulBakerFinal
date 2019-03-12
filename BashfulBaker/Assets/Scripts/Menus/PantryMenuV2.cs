using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Items;
using Assets.Scripts.QuestSystem.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class PantryMenuV2:Menu
    {

        public bool setForTutorial = false;
        private getIng tutorialProgress;
        private bool eatFirstInput;

        public override void Start()
        {
            this.menuCursor = this.gameObject.transform.Find("Canvas").Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void setUpForSnapping()
        {
           
        }

        public override void exitMenu()
        {
            GameInformation.Game.Menu = null;
            base.exitMenu();
        }
        public override void Update()
        {
            if (eatFirstInput == false)
            {
                eatFirstInput = true;
                return;
            }
            checkForInput();
        }

        public void initializeTutorialMode(getIng Progress)
        {
            this.tutorialProgress = Progress;
            this.setForTutorial = true;
        }

        private void checkForInput()
        {
            if (Game.DialogueManager.IsDialogueUp)
            {
                return;
            }

            if (GameInput.InputControls.APressed)
            {
                Dish d = new Dish("Cookie Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;

                if (setForTutorial)
                {
                    updateTutorial();
                }

                exitMenu();
            }
            else if (GameInput.InputControls.BPressed)
            {
                Dish d = new Dish("Cake Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if (GameInput.InputControls.XPressed)
            {
                Dish d = new Dish("Pie Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if (GameInput.InputControls.YPressed)
            {
                Dish d = new Dish("Bread Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if(GameInput.InputControls.LeftBumperPressed || GameInput.InputControls.StartPressed)
            {
                exitMenu();
            }
        }

        private void updateTutorial()
        {

            Debug.Log("Picked up ingredients");

            Game.HUD.showInventory = true;
            Game.Player.dishesInventory.Add(new Dish("Chocolate Chip Cookie"));
            Game.HUD.updateInventoryHUD();
            Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookie", "Sylvia", new List<string>()));
            Game.HUD.showQuests = true;
            Game.StartNewTimerPhase(5, 0);

            ///Get code from getting script
            FindObjectOfType<DialogueManager>().StartDialogue(tutorialProgress.pickUpText);

            Game.Player.gameObject.GetComponent<PlayerMovement>().NextStep();
            tutorialProgress.arrow.GetComponent<progress>().SetStep(1);
            tutorialProgress.arrow.GetComponent<UnityEngine.SpriteRenderer>().enabled = true;
            tutorialProgress.arrow.GetComponent<progress>().A.SetActive(false);
        }
    }
}
