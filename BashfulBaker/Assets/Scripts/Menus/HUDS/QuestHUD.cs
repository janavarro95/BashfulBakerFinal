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
        Image quest5Image;

        GameObject menuBackground;

        public override void Start()
        {
            updateForTheDay();
            menuBackground.SetActive(false);
        }

        public void updateForTheDay()
        {
            GameObject canvas = this.transform.Find("Canvas").gameObject;

            menuBackground = canvas.transform.Find("MenuBackground").gameObject;

            quest1Image = menuBackground.transform.Find("Quest1").gameObject.GetComponent<Image>();
            quest2Image = menuBackground.transform.Find("Quest2").gameObject.GetComponent<Image>();
            quest3Image = menuBackground.transform.Find("Quest3").gameObject.GetComponent<Image>();
            quest4Image = menuBackground.transform.Find("Quest4").gameObject.GetComponent<Image>();
            quest5Image = menuBackground.transform.Find("Quest5").gameObject.GetComponent<Image>();

            getQuestImages();
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
                quest1Image.gameObject.SetActive(true);
            }
            if (cookingQuests.Count >= 2)
            {
                CookingQuest quest = cookingQuests[1];
                quest2Image.sprite = loadQuestImage(quest);
                quest2Image.gameObject.SetActive(true);
            }
            if (cookingQuests.Count >= 3)
            {
                CookingQuest quest = cookingQuests[2];
                quest3Image.sprite = loadQuestImage(quest);
                quest3Image.gameObject.SetActive(true);
            }
            if (cookingQuests.Count >= 4)
            {
                CookingQuest quest = cookingQuests[3];
                quest4Image.sprite = loadQuestImage(quest);
                quest4Image.gameObject.SetActive(true);
            }
            if (cookingQuests.Count >= 5)
            {
                CookingQuest quest = cookingQuests[4];
                quest5Image.sprite = loadQuestImage(quest);
                quest5Image.gameObject.SetActive(true);
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
                    "Quest_SylviaCookies"
                }));

                Sprite sprite = Game.ContentManager.loadSprite(texture, new Rect(new Rect(0, 0, 122, 52)), new Vector2(0.5f, 0.5f), 16);
                return sprite;
            }

            return null;
        }

        private void setUpMenuForDisplay()
        {

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

    }
}
