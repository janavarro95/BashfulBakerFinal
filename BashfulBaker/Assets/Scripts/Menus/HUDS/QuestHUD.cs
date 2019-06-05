using Assets.Scripts.GameInformation;
using Assets.Scripts.QuestSystem.Quests;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.HUDS
{
    public class QuestHUD:HUD
    {
        GameObject canvas;
        private Button exitButton;

        Image quest1Image;
        Image quest2Image;
        Image quest3Image;
        Image quest4Image;

        Image quest1Complete;
        Image quest2Complete;
        Image quest3Complete;
        Image quest4Complete;

        GameObject menuBackground;

        public override void Start()
        {
            canvas = this.gameObject.transform.Find("Canvas").gameObject;
            updateForTheDay();
            menuBackground.SetActive(true);
        }

        public void updateForTheDay()
        {
            

            menuBackground = canvas.transform.Find("MenuBackground").gameObject;

            quest1Image = menuBackground.transform.Find("Quest1").gameObject.GetComponent<Image>();
            quest2Image = menuBackground.transform.Find("Quest2").gameObject.GetComponent<Image>();
            quest3Image = menuBackground.transform.Find("Quest3").gameObject.GetComponent<Image>();
            quest4Image = menuBackground.transform.Find("Quest4").gameObject.GetComponent<Image>();

            quest1Complete = quest1Image.gameObject.transform.Find("Image").GetComponent<Image>();
            quest2Complete = quest2Image.gameObject.transform.Find("Image").GetComponent<Image>();
            quest3Complete = quest3Image.gameObject.transform.Find("Image").GetComponent<Image>();
            quest4Complete = quest4Image.gameObject.transform.Find("Image").GetComponent<Image>();

            getQuestImages();
        }

        /// <summary>
        /// Sets the actual quest images based off of positions and data.
        /// </summary>
        private void getQuestImages()
        {
            List<CookingQuest> cookingQuests = Game.QuestManager.getCookingQuests();
            quest1Image.enabled = false;
            quest2Image.enabled = false;
            quest3Image.enabled = false;
            quest4Image.enabled = false;
            quest1Complete.enabled = false;
            quest2Complete.enabled = false;
            quest3Complete.enabled = false;
            quest4Complete.enabled = false;

            if (cookingQuests.Count >= 1)
            {
                CookingQuest quest = cookingQuests[0];
                quest1Image.sprite = loadQuestImage(quest);
                quest1Image.enabled = true;
                quest1Image.gameObject.SetActive(true);
                if (quest.IsCompleted) quest1Complete.enabled = true;
                else quest1Complete.enabled = false;
            }
            if (cookingQuests.Count >= 2)
            {
                CookingQuest quest = cookingQuests[1];
                quest2Image.sprite = loadQuestImage(quest);
                quest2Image.enabled = true;
                quest2Image.gameObject.SetActive(true);
                if (quest.IsCompleted) quest2Complete.enabled = true;
                else quest2Complete.enabled = false;
            }
            if (cookingQuests.Count >= 3)
            {
                CookingQuest quest = cookingQuests[2];
                quest3Image.sprite = loadQuestImage(quest);
                quest3Image.enabled = true;
                quest3Image.gameObject.SetActive(true);
                if (quest.IsCompleted) quest3Complete.enabled = true;
                else quest3Complete.enabled = false;
            }
            if (cookingQuests.Count >= 4)
            {
                CookingQuest quest = cookingQuests[3];
                quest4Image.sprite = loadQuestImage(quest);
                quest4Image.enabled = true;
                quest4Image.gameObject.SetActive(true);
                if (quest.IsCompleted) quest4Complete.enabled = true;
                else quest4Complete.enabled = false;
            }

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

                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }
            if (quest.personToDeliverTo == "Sylvia" && quest.RequiredDish == "Mint Chip Cookies")
            {
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_SylviaMC"
                }));
                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            if (quest.personToDeliverTo == "Lylia" && quest.RequiredDish == "Oatmeal Raisin Cookies")
            {
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_LyliaOR"
                }));
                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }


            if (quest.personToDeliverTo == "Amari" && quest.RequiredDish == "Mint Chip Cookies")
            {
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_AmariMC"
                }));
                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            if (quest.personToDeliverTo == "Amari" && quest.RequiredDish == "Pecan Crescent Cookies")
            {
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_AmariPC"
                }));
                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            if (quest.personToDeliverTo == "Brian" && quest.RequiredDish == "Oatmeal Raisin Cookies")
            {
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_BrianOR"
                }));
                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            if (quest.personToDeliverTo == "Ian" && quest.RequiredDish == "Mint Chip Cookies")
            {
                Texture2D texture = Game.ContentManager.loadTexture2DFromResources(CSExtensions.PathCombine(new List<string>() {
                    "Graphics",
                    "UI",
                    "Menus",
                    "DailyRecap",
                    "QuestButton_IanMC"
                }));
                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 110, 38)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            return null;
        }

        public void setUpMenuForDisplay()
        {
            getQuestImages();
        }


        public override void Update()
        {
            if (Game.HUD.showQuests == true)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    if (menuBackground.activeInHierarchy)
                    {
                        menuBackground.SetActive(false);
                        return;
                    }
                    else
                    {
                        setUpMenuForDisplay();
                        menuBackground.SetActive(true);
                        return;
                    }
                }
            }

        }


        public override void setVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible)
            {
                canvas.SetActive(false);
                updateForTheDay();
            }
            if (visibility == Enums.Visibility.Visible)
            {
                canvas.SetActive(true);
            }
        }
    }
}
