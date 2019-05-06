﻿using Assets.Scripts.GameInformation;
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

        private Image quest4Image;
        private Image quest5Image;
        private Image quest6Image;

        private Image minigame1Performance;
        private Image minigame2Performance;
        private Image minigame3Performance;
        private Image minigame4Performance;
        private Image minigame5Performance;
        private Image minigame6Performance;

        private TMPro.TextMeshProUGUI text;

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
            quest4Image = background.transform.Find("Delivery4").gameObject.transform.GetComponent<Image>();
            quest5Image = background.transform.Find("Delivery5").gameObject.transform.GetComponent<Image>();
            quest6Image = background.transform.Find("Delivery6").gameObject.transform.GetComponent<Image>();

            minigame1Performance = background.transform.Find("MG1Performance").gameObject.transform.GetComponent<Image>();
            minigame2Performance = background.transform.Find("MG2Performance").gameObject.transform.GetComponent<Image>();
            minigame3Performance = background.transform.Find("MG3Performance").gameObject.transform.GetComponent<Image>();
            minigame4Performance = background.transform.Find("MG4Performance").gameObject.transform.GetComponent<Image>();
            minigame5Performance = background.transform.Find("MG5Performance").gameObject.transform.GetComponent<Image>();
            minigame6Performance = background.transform.Find("MG6Performance").gameObject.transform.GetComponent<Image>();

            quest1Image.gameObject.SetActive(false);
            quest2Image.gameObject.SetActive(false);
            quest3Image.gameObject.SetActive(false);
            quest4Image.gameObject.SetActive(false);
            quest5Image.gameObject.SetActive(false);
            quest6Image.gameObject.SetActive(false);

            finishedButton = new Components.MenuComponent(canvas.transform.Find("Close Button").gameObject.GetComponent<Button>());

            if (Game.CurrentDayNumber == 0) Game.CurrentDayNumber = 1;

            getQuestImages();
            setUpForSnapping();

            text = background.transform.Find("GuardsFed").gameObject.transform.GetComponent<TMPro.TextMeshProUGUI>();
            text.text ="x"+Game.NumberOfTimesCaught[Game.CurrentDayNumber].ToString();
        }

        /// <summary>
        /// Sets the actual quest images based off of positions and data.
        /// </summary>
        private void getQuestImages()
        {
            List<CookingQuest> cookingQuests = Game.QuestManager.getCookingQuests();
            List<CookingQuest> finishedQuests = new List<CookingQuest>();

            foreach(CookingQuest cq in cookingQuests)
            {
                if (cq.IsCompleted == true)
                {
                    finishedQuests.Add(cq);
                    continue;
                }
            }

            foreach (CookingQuest cq in finishedQuests) {
                if (finishedQuests.Count >= 1)
                {
                    quest1Image.gameObject.SetActive(true);
                    quest1Image.sprite = loadQuestImage(cq);
                }
                if (finishedQuests.Count >= 2)
                {
                    quest2Image.gameObject.SetActive(true);
                    quest2Image.sprite = loadQuestImage(cq);
                }
                if (finishedQuests.Count >= 3)
                {
                    quest3Image.gameObject.SetActive(true);
                    quest3Image.sprite = loadQuestImage(cq);
                }
                if (finishedQuests.Count >= 4)
                {
                    quest4Image.gameObject.SetActive(true);
                    quest4Image.sprite = loadQuestImage(cq);
                }
                if (finishedQuests.Count >= 5)
                {
                    quest5Image.gameObject.SetActive(true);
                    quest5Image.sprite = loadQuestImage(cq);
                }
                if (finishedQuests.Count >= 6)
                {
                    quest6Image.gameObject.SetActive(true);
                    quest6Image.sprite = loadQuestImage(cq);
                }
            }

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
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_SylviaCC"
                }));
            
                Sprite sprite=Game.ContentManager.loadSprite(texture, new Rect(new Rect(0,0,110,38)), new Vector2(0.5f, 0.5f), 16);
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
