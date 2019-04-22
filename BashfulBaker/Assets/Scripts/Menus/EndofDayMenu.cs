using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using Assets.Scripts.QuestSystem.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class EndofDayMenu:Menu
    {
        private Text endOfDayText;

        private GameObject hoverInfo;
        private Text hoverText;
        private Image hoverImage;

        //public List<MenuComponent> snappableComponents;


        private Menus.Components.MenuComponent finishedButton;

        private Image quest1Image;
        private Image quest2Image;
        private Image quest3Image;


        public override void Start()
        {
            Game.Menu = this;
            Game.HUD.showHUD = false;

            //GameInformation.Game.QuestManager.addQuest(new CookingQuest("Nuggies", "Ronald Mc.Donald", new List<string>() { "Fries" }));

            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();

            GameObject background = canvas.transform.Find("Background").gameObject;
            quest1Image = background.transform.Find("Delivery1").gameObject.transform.GetComponent<Image>();
            quest2Image = background.transform.Find("Delivery2").gameObject.transform.GetComponent<Image>();
            quest3Image = background.transform.Find("Delivery3").gameObject.transform.GetComponent<Image>();

            quest1Image.gameObject.SetActive(false);
            quest2Image.gameObject.SetActive(false);
            quest3Image.gameObject.SetActive(false);

            finishedButton = new Components.MenuComponent(canvas.transform.Find("Close Button").GetComponent<Button>());


            if (Game.CurrentDayNumber == 0) Game.CurrentDayNumber = 1;


            getQuestImages();
        }

        private void getQuestImages()
        {
            List<CookingQuest> cookingQuests = Game.QuestManager.getCookingQuests();

            Debug.Log("Cooking quest count: " + cookingQuests.Count);

            if (cookingQuests.Count >= 1)
            {
                CookingQuest quest = cookingQuests[0];
                quest1Image.sprite = loadQuestImage(quest);
                if (quest.IsCompleted == false)
                {
                    quest1Image.rectTransform.sizeDelta = new Vector2(-140, quest1Image.rectTransform.sizeDelta.y);
                }
                else
                {
                    quest1Image.rectTransform.sizeDelta = new Vector2(140, quest1Image.rectTransform.sizeDelta.y);
                }
                quest1Image.gameObject.SetActive(true);

            }
            if (cookingQuests.Count >= 2)
            {
                CookingQuest quest = cookingQuests[1];
                quest2Image.sprite = loadQuestImage(quest);
                if (quest.IsCompleted == false)
                {
                    quest2Image.rectTransform.sizeDelta = new Vector2(-140, quest2Image.rectTransform.sizeDelta.y);
                }
                else
                {
                    quest2Image.rectTransform.sizeDelta = new Vector2(140, quest2Image.rectTransform.sizeDelta.y);
                }
                quest2Image.gameObject.SetActive(true);
            }
            if (cookingQuests.Count >= 3)
            {
                CookingQuest quest = cookingQuests[2];
                quest3Image.sprite = loadQuestImage(quest);
                if (quest.IsCompleted == false)
                {
                    quest3Image.rectTransform.sizeDelta = new Vector2(-140, quest3Image.rectTransform.sizeDelta.y);
                }
                else
                {
                    quest3Image.rectTransform.sizeDelta = new Vector2(140, quest3Image.rectTransform.sizeDelta.y);
                }
                quest3Image.gameObject.SetActive(true);
            }
        }

        private Sprite loadQuestImage(CookingQuest quest)
        {
            if(quest.personToDeliverTo=="Sylvia" && quest.RequiredDish=="Chocolate Chip Cookies")
            {
                Debug.Log("Quest is for Sylvia and her Choco Cookies!");
                Texture2D texture=Game.ContentManager.loadTexture2DFromResources("Graphics/UI/Menus/DailyRecap/Quest_SylviaCookies");
                Sprite sprite=Game.ContentManager.loadSprite(texture, new Rect(0, 0, 244, 104), new Vector2(0, 0), 16);
                return sprite;
            }
            else
            {
                Debug.Log("Quest if for: " + quest.personToDeliverTo);
                Debug.Log("The dish to deliver is: " + quest.RequiredDish);
            }

            return null;
        }

        public override void exitMenu()
        {
            Game.Menu = null;
            base.exitMenu();
        }

        public override void setUpForSnapping()
        {

            finishedButton.setNeighbors(null, null, null, null);

            this.selectedComponent = finishedButton;
            this.selectedComponent.snapToThisComponent();

        }

        public override bool snapCompatible()
        {
            return true;
        }

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

        public void checkForInput()
        {
            if (GameCursorMenu.SimulateMousePress(finishedButton))
            {
                Game.returnToMainMenu();
            }
        }

    }
}
