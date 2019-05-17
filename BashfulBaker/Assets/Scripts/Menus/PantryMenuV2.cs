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

        public bool setForTutorial
        {
            get
            {
                return Game.CurrentDayNumber == 0 || Game.CurrentDayNumber == 1;
            }
        }
        private getIng tutorialProgress;
        [SerializeField]
        private bool eatFirstInput;


        GameObject categorySelect;
        GameObject cookieSelect;
        GameObject cakeSelect;
        GameObject pieSelect;
        GameObject breadSelect;
        GameObject cookieSelectTutorial;

        GameObject cookieHidden1;
        GameObject cookieHidden2;
        GameObject cookieHidden3;
        GameObject cookieHidden4;

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

            this.cookieHidden1 = this.cookieSelect.gameObject.transform.Find("Image").Find("Blackout1").gameObject;
            this.cookieHidden2 = this.cookieSelect.gameObject.transform.Find("Image").Find("Blackout2").gameObject;
            this.cookieHidden3 = this.cookieSelect.gameObject.transform.Find("Image").Find("Blackout3").gameObject;
            this.cookieHidden4 = this.cookieSelect.gameObject.transform.Find("Image").Find("Blackout4").gameObject;

            currentMode = PantryMode.Select;

            Game.Menu = this;
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

            if (GameInput.InputControls.StartPressed)
            {
                exitMenu();
            }
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
                /*
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
                */
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
                Debug.Log("Is Dialogue up?");
                return;
            }

            if (setForTutorial && InputControls.APressed)
            {
                
                Dish d = new Dish(Enums.Dishes.ChocolateChipCookies);
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                Game.Player.updateHeldItemSprite();

                updateTutorial();
                exitMenu();
                
            }

            if (setForTutorial == false)
            {

                if (Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.ChocolateChips).stack > 0)
                {
                    cookieHidden1.SetActive(false);
                }
                else
                {
                    cookieHidden1.SetActive(true);
                }
                if (Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.MintChips).stack > 0)
                {
                    cookieHidden2.SetActive(false);
                }
                else
                {
                    cookieHidden2.SetActive(true);
                }
                if (Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Pecans).stack > 0)
                {
                    cookieHidden3.SetActive(false);
                }
                else
                {
                    cookieHidden3.SetActive(true);
                }
                if (Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Raisins).stack > 0)
                {
                    cookieHidden4.SetActive(false);
                }
                else
                {
                    cookieHidden4.SetActive(true);
                }

                if (GameInput.InputControls.APressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing=>(ing as SpecialIngredient).ingredientType== Enums.SpecialIngredients.ChocolateChips).stack>0)
                {
                    
                    Dish d = new Dish(Enums.Dishes.ChocolateChipCookies);
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.ChocolateChips).stack -= 1;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.BPressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.MintChips).stack > 0)
                {
                    Dish d = new Dish(Enums.Dishes.MintChipCookies);
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.MintChips).stack -= 1;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.XPressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Pecans).stack > 0)
                {
                    Dish d = new Dish(Enums.Dishes.PecanCookies);
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Pecans).stack -= 1;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.YPressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Raisins).stack > 0)
                {
                    Dish d = new Dish(Enums.Dishes.OatmealRaisinCookies);
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Raisins).stack -= 1;
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

                if (GameInput.InputControls.APressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.ChocolateChips).stack > 0)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.ChocolateChips).stack -= 1;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.BPressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Pecans).stack > 0)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Pecans).stack -= 1;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.XPressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Carrots).stack > 0)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Carrots).stack -= 1;

                    updateTutorial();
                    exitMenu();
                }
                else if (GameInput.InputControls.YPressed && Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Strawberries).stack > 0)
                {
                    Dish d = new Dish("Chocolate Chip Cookie");
                    Game.Player.dishesInventory.Add(d);
                    Game.Player.activeItem = d;

                    Game.Player.specialIngredientsInventory.actualItems.Find(ing => (ing as SpecialIngredient).ingredientType == Enums.SpecialIngredients.Strawberries).stack -= 1;

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

            //Debug.Log("Picked up ingredients");

            Game.HUD.showHUD = true;

            Game.HUD.showInventory = true;
            //Game.Player.dishesInventory.Add(new Dish("Chocolate Chip Cookie"));
            Game.HUD.updateInventoryHUD();

            
            Game.HUD.showQuests = true;
            Game.StartNewTimerPhase(5, 0,true);

            try
            {
                ///Get code from getting script
                if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().currentStep == -1)
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(tutorialProgress.pickUpText);
                }
                Game.Player.gameObject.GetComponent<PlayerMovement>().NextStep();
                tutorialProgress.arrow.GetComponent<progress>().SetStep(1);
                tutorialProgress.arrow.GetComponent<UnityEngine.SpriteRenderer>().enabled = true;
                tutorialProgress.arrow.GetComponent<progress>().A.SetActive(false);
            }
            catch(Exception err)
            {

            }
        }

        private void getDailyQuests()
        {
            //if (Game.CurrentDayNumber == 1 || Game.CurrentDayNumber == 0) Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookies", "Sylvia", new List<string>()));

            if (Game.CurrentDayNumber == 2)
            {
                Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookies", "Sylvia", new List<string>()));
                Game.QuestManager.addQuest(new CookingQuest("Mint Chip Cookies", "Norville", new List<string>()));
                Game.QuestManager.addQuest(new CookingQuest("Oatmeal Raisin Cookies", "Lylia", new List<string>()));

            }

        }
    }
}
