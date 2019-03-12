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
        [SerializeField]
        private bool eatFirstInput;


        GameObject categorySelect;
        GameObject cookieSelect;
        GameObject cakeSelect;
        GameObject pieSelect;
        GameObject breadSelect;
        GameObject cookieSelectTutorial;

        [SerializeField]
        private enum PantryMode
        {
            Select,
            Cookies,
            Cakes,
            Pies,
            Breads
        }

        [SerializeField]
        private PantryMode currentMode;

        public void Awake()
        {
            this.menuCursor = this.gameObject.transform.Find("Canvas").Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();


            this.categorySelect = this.gameObject.transform.Find("Canvas").Find("CategorySelect").gameObject;
            this.cookieSelect = this.gameObject.transform.Find("Canvas").Find("CookieSelect").gameObject;
            this.cakeSelect = this.gameObject.transform.Find("Canvas").Find("CakeSelect").gameObject;
            this.pieSelect = this.gameObject.transform.Find("Canvas").Find("PieSelect").gameObject;
            this.breadSelect = this.gameObject.transform.Find("Canvas").Find("BreadSelect").gameObject;
            this.cookieSelectTutorial = this.gameObject.transform.Find("Canvas").Find("CookieSelectTutorial").gameObject;

            this.cookieSelect.SetActive(false);
            this.cakeSelect.SetActive(false);
            this.pieSelect.SetActive(false);
            this.breadSelect.SetActive(false);
            this.cookieSelectTutorial.SetActive(false);

            currentMode = PantryMode.Select;
        }

        public override void Start()
        {
           
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

            categorySelect.transform.Find("BButton").gameObject.SetActive(false);
            categorySelect.transform.Find("XButton").gameObject.SetActive(false);
            categorySelect.transform.Find("YButton").gameObject.SetActive(false);
        }

        private void checkForInput()
        {
            if (currentMode == PantryMode.Select)
            {
                bool input = checkForInput_Select();
                if (input == true) return;
            }

            if (currentMode == PantryMode.Cookies) checkForInput_Cookies();
            if (currentMode == PantryMode.Cakes) checkForInput_Cakes();
            if (currentMode == PantryMode.Pies) checkForInput_Pies();
            if (currentMode == PantryMode.Breads) checkForInput_Breads();
        }

        private bool checkForInput_Select()
        {
            if (Game.DialogueManager.IsDialogueUp)
            {
                return true;
            }

            if (setForTutorial && InputControls.APressed)
            {
                /*
                Dish d = new Dish("Cookie Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;

                updateTutorial();
                exitMenu();
                */
                cookieSelectTutorial.SetActive(true);
                this.currentMode = PantryMode.Cookies;
                return true;
            }

            if (setForTutorial == false)
            {

                if (GameInput.InputControls.APressed)
                {
                    cookieSelect.SetActive(true);
                    this.currentMode = PantryMode.Cookies;
                    return true;
                }
                else if (GameInput.InputControls.BPressed)
                {
                    cakeSelect.SetActive(true);
                    this.currentMode = PantryMode.Cakes;
                    return true;
                }
                else if (GameInput.InputControls.XPressed)
                {
                    pieSelect.SetActive(true);
                    this.currentMode = PantryMode.Pies;
                    return true;
                }
                else if (GameInput.InputControls.YPressed)
                {
                    breadSelect.SetActive(true);
                    this.currentMode = PantryMode.Breads;
                    return true;
                }
            }
            else if ( GameInput.InputControls.StartPressed)
            {
                exitMenu();
                return true;
            }
            return false;
        }

        private void checkForInput_Cookies()
        {
            if (Game.DialogueManager.IsDialogueUp)
            {
                return;
            }

            if (setForTutorial && InputControls.APressed)
            {
                
                Dish d = new Dish("Chocolate Chip Cookie");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;

                updateTutorial();
                exitMenu();
                
            }

            if (setForTutorial == false)
            {

                if (GameInput.InputControls.APressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.BPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.XPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.YPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
            }
            else if (GameInput.InputControls.StartPressed)
            {
                this.currentMode = PantryMode.Select;
                cookieSelect.SetActive(false);
                return;
            }
            return;
        }

        private void checkForInput_Cakes()
        {
            if (setForTutorial == true) return;


            if (Game.DialogueManager.IsDialogueUp)
            {
                return;
            }

            if (setForTutorial == false)
            {

                if (GameInput.InputControls.APressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.BPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.XPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.YPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.StartPressed)
                {
                    this.currentMode = PantryMode.Select;
                    cookieSelect.SetActive(false);
                    return;
                }
            }

            return;
        }

        private void checkForInput_Pies()
        {
            if (setForTutorial == true) return;


            if (Game.DialogueManager.IsDialogueUp)
            {
                return;
            }

            if (setForTutorial == false)
            {

                if (GameInput.InputControls.APressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.BPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.XPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.YPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.StartPressed)
                {
                    this.currentMode = PantryMode.Select;
                    cookieSelect.SetActive(false);
                    return;
                }
            }
        }

        private void checkForInput_Breads()
        {
            if (setForTutorial == true) return;


            if (Game.DialogueManager.IsDialogueUp)
            {
                return;
            }

            if (setForTutorial == false)
            {

                if (GameInput.InputControls.APressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.BPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.XPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.YPressed)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.StartPressed)
                {
                    this.currentMode = PantryMode.Select;
                    cookieSelect.SetActive(false);
                    return;
                }
            }
        }

        private void updateTutorial()
        {

            Debug.Log("Picked up ingredients");

            Game.HUD.showInventory = true;
            //Game.Player.dishesInventory.Add(new Dish("Chocolate Chip Cookie"));
            Game.HUD.updateInventoryHUD();
            Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookie", "Sylvia", new List<string>()));
            Game.HUD.showQuests = true;
            Game.StartNewTimerPhase(5, 0);

            try
            {
                ///Get code from getting script
                FindObjectOfType<DialogueManager>().StartDialogue(tutorialProgress.pickUpText);

                Game.Player.gameObject.GetComponent<PlayerMovement>().NextStep();
                tutorialProgress.arrow.GetComponent<progress>().SetStep(1);
                tutorialProgress.arrow.GetComponent<UnityEngine.SpriteRenderer>().enabled = true;
                tutorialProgress.arrow.GetComponent<progress>().A.SetActive(false);
            }
            catch(Exception err)
            {

            }
        }
    }
}
