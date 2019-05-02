using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using Assets.Scripts.QuestSystem.Quests;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// Deals with displaying the player's end results to them when finishing a day.
    /// </summary>
    public class EndofDayMenu:Menu
    {
        /// <summary>
        /// The finished button for closing the menu.
        /// </summary>
        private Menus.Components.MenuComponent finishedButton;

        /// <summary>
        /// The image for the first completed quest fro that day.
        /// </summary>
        private Image quest1Image;
        /// <summary>
        /// The image for the second completed quest for that day.
        /// </summary>
        private Image quest2Image;
        /// <summary>
        /// The image for the third completed quest for that day.
        /// </summary>
        private Image quest3Image;
        /// <summary>
        /// The image that is shown for when the player has completed all the quests.
        /// </summary>
        private Image finishedImage;

        /// <summary>
        /// Runs when this script is started up by Unity.
        /// </summary>
        public override void Start()
        {
            Game.Menu = this;
            Game.HUD.showHUD = false;

            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();

            GameObject background = canvas.transform.Find("Background").gameObject;
            quest1Image = background.transform.Find("Delivery1").gameObject.transform.GetComponent<Image>();
            quest2Image = background.transform.Find("Delivery2").gameObject.transform.GetComponent<Image>();
            quest3Image = background.transform.Find("Delivery3").gameObject.transform.GetComponent<Image>();
            finishedImage = background.transform.Find("FinishedImage").gameObject.transform.GetComponent<Image>();

            quest1Image.gameObject.SetActive(false);
            quest2Image.gameObject.SetActive(false);
            quest3Image.gameObject.SetActive(false);
            finishedImage.gameObject.SetActive(false);

            finishedButton = new Components.MenuComponent(canvas.transform.Find("Close Button").gameObject.GetComponent<Button>());

            if (Game.CurrentDayNumber == 0) Game.CurrentDayNumber = 1;

            getQuestImages();
            setUpForSnapping();
        }

        /// <summary>
        /// Sets the actual quest images based off of positions and data.
        /// </summary>
        private void getQuestImages()
        {
            List<CookingQuest> cookingQuests = Game.QuestManager.getCookingQuests();
            if (cookingQuests.Count >= 1)
            {
                CookingQuest quest = cookingQuests[0];
                quest1Image.sprite = loadQuestImage(quest);
                if (quest.IsCompleted == false)
                {
                    //quest1Image.rectTransform.sizeDelta = new Vector2(-140, quest1Image.rectTransform.sizeDelta.y);
                    quest1Image.rectTransform.localPosition = new Vector3(140, quest1Image.rectTransform.localPosition.y);
                }
                else
                {
                    quest1Image.rectTransform.localPosition = new Vector2(-140, quest1Image.rectTransform.localPosition.y);
                }
                quest1Image.gameObject.SetActive(true);

            }
            if (cookingQuests.Count >= 2)
            {
                CookingQuest quest = cookingQuests[1];
                quest2Image.sprite = loadQuestImage(quest);
                if (quest.IsCompleted == false)
                {
                    quest2Image.rectTransform.localPosition = new Vector2(140, quest2Image.rectTransform.localPosition.y);
                }
                else
                {
                    quest2Image.rectTransform.localPosition = new Vector2(-140, quest2Image.rectTransform.localPosition.y);
                }
                quest2Image.gameObject.SetActive(true);
            }
            if (cookingQuests.Count >= 3)
            {
                CookingQuest quest = cookingQuests[2];
                quest3Image.sprite = loadQuestImage(quest);
                if (quest.IsCompleted == false)
                {
                    quest3Image.rectTransform.localPosition = new Vector2(140, quest3Image.rectTransform.localPosition.y);
                }
                else
                {
                    quest3Image.rectTransform.localPosition = new Vector2(-140, quest3Image.rectTransform.localPosition.y);
                }
                quest3Image.gameObject.SetActive(true);
            }

            foreach(CookingQuest cq in cookingQuests)
            {
                if (cq.IsCompleted == true) continue;
                else
                {
                    finishedImage.gameObject.SetActive(false);
                    return;
                }
            }
            finishedImage.gameObject.SetActive(true);
            Game.UnlockDay();

        }

        /// <summary>
        /// Loads the appropriate image for what quest we have recieved.
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        private Sprite loadQuestImage(CookingQuest quest)
        {
            if (quest.personToDeliverTo == "Sylvia" && quest.RequiredDish == "Chocolate Chip Cookies")
            {
                Debug.Log("Quest is for Sylvia and her Choco Cookies!");
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "Quest_SylviaCookies"
                }));
            
                Sprite sprite=Game.ContentManager.loadSprite(texture, new Rect(new Rect(0,0,122,52)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            return null;
        }

        /// <summary>
        /// Logic that occurs when exiting the menu.
        /// </summary>
        public override void exitMenu()
        {
            Game.Menu = null;
            base.exitMenu();
        }

        /// <summary>
        /// Sets up the menu for snappy controls.
        /// </summary>
        public override void setUpForSnapping()
        {

            finishedButton.setNeighbors(finishedButton, finishedButton, finishedButton, finishedButton);

            this.selectedComponent = finishedButton;
            this.menuCursor.snapToCurrentComponent();

        }
        
        /// <summary>
        /// Checks to see if the menu is set up for snappy controls.
        /// </summary>
        /// <returns></returns>
        public override bool snapCompatible()
        {
            return true;
        }
        
        /// <summary>
        /// Checks for updates ~60x a second.
        /// </summary>
        public override void Update()
        {
            checkForInput();
        }

        /// <summary>
        /// Checks to see if any of the quest info for npcs has been hovered over yet.
        /// </summary>
        public void checkForHover()
        {
        }

        /// <summary>
        /// Checks to see if the player is interacting with the finished button for the game.
        /// </summary>
        public void checkForInput()
        {
            if (GameCursorMenu.SimulateMousePress(finishedButton))
            {
                Menu.Instantiate<ReturnToDailySelectMenu>(true);
            }
        }

    }
}
