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
    public class EndofDayMenu : Menu
    {
        /// <summary>
        /// The finished button for closing the menu.
        /// </summary>
        private Menus.Components.MenuComponent finishedButton;

        private MenuComponent mainMenuButton;

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


        private Text StirringPerformance;
        private Text RollingPerformance;
        private Text PackingPerformance;

        private TMPro.TextMeshProUGUI text;


        private List<Vector3> cameraPositions;
        int positionIndex;
        float lerpSpeed = 0.001f;
        float currentLerp = 0f;

        public List<Camera> cameras;

        private Vector3 currentPos
        {
            get
            {
                if (positionIndex < cameraPositions.Count)
                {
                    return cameraPositions[positionIndex];
                }
                else
                {
                    positionIndex = positionIndex % cameraPositions.Count;
                    return cameraPositions[positionIndex];
                }
            }
        }
        private Vector3 nextPos
        {
            get
            {
                if (positionIndex + 1 < cameraPositions.Count)
                {
                    return cameraPositions[positionIndex+1];
                }
                if (positionIndex + 1 == cameraPositions.Count)
                {
                    return cameraPositions[0];
                }
                else if(positionIndex+1>cameraPositions.Count)
                {
                    positionIndex = positionIndex % cameraPositions.Count;
                    return cameraPositions[positionIndex+1];
                }
                else
                {
                    return cameraPositions[positionIndex+1];
                }
            }
        }


        /// <summary>
        /// Runs when this script is started up by Unity.
        /// </summary>
        public override void Start()
        {
            Game.Player.gameObject.SetActive(false);
            ScreenTransitions.StartSceneTransition(2f, "", Color.black, ScreenTransitions.TransitionState.FadeIn);
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


            StirringPerformance = background.transform.Find("StirringTime").gameObject.transform.GetComponent<Text>();
            RollingPerformance = background.transform.Find("RollingTime").gameObject.transform.GetComponent<Text>();
            PackingPerformance = background.transform.Find("PackingTime").gameObject.transform.GetComponent<Text>();

            StirringPerformance.text = Game.MinigameStats[Enums.CookingStationMinigame.MixingBowl].averageTimeMinSec;
            RollingPerformance.text = Game.MinigameStats[Enums.CookingStationMinigame.RollingStation].averageTimeMinSec;
            PackingPerformance.text = Game.MinigameStats[Enums.CookingStationMinigame.PackingStation].averageTimeMinSec;

            quest1Image.gameObject.SetActive(false);
            quest2Image.gameObject.SetActive(false);
            quest3Image.gameObject.SetActive(false);
            quest4Image.gameObject.SetActive(false);
            quest5Image.gameObject.SetActive(false);
            quest6Image.gameObject.SetActive(false);

            finishedButton = new Components.MenuComponent(canvas.transform.Find("Close Button").gameObject.GetComponent<Button>());
            mainMenuButton = new Components.MenuComponent(canvas.transform.Find("MainMenu").gameObject.GetComponent<Button>());

            if (Game.CurrentDayNumber == 0) Game.CurrentDayNumber = 1;

            getQuestImages();
            setUpForSnapping();

            text = background.transform.Find("GuardsFed").gameObject.transform.GetComponent<TMPro.TextMeshProUGUI>();
            if (Game.NumberOfTimesCaught != null)
            {
                text.text = "x" + Game.NumberOfTimesCaught[Game.CurrentDayNumber].ToString();
            }
            else
            {
                text.text = "x0";
            }

            if (SceneManager.sceneCount <= 2)
            {
                SceneManager.LoadScene("Neighborhood", LoadSceneMode.Additive);
            }

        }


        /// <summary>
        /// Sets the actual quest images based off of positions and data.
        /// </summary>
        private void getQuestImages()
        {
            List<CookingQuest> cookingQuests = Game.QuestManager.getCookingQuests();
            List<CookingQuest> finishedQuests = new List<CookingQuest>();

            foreach (CookingQuest cq in cookingQuests)
            {
                if (cq.HasBeenDelivered == true)
                {
                    finishedQuests.Add(cq);
                    continue;
                }
            }
            int count = 0;
            foreach (CookingQuest cq in finishedQuests)
            {
                if (finishedQuests.Count >= 1 && count == 0)
                {
                    quest1Image.gameObject.SetActive(true);
                    quest1Image.sprite = loadQuestImage(cq);
                    count++;
                    continue;
                }
                if (finishedQuests.Count >= 2 && count == 1)
                {
                    quest2Image.gameObject.SetActive(true);
                    quest2Image.sprite = loadQuestImage(cq);
                    count++;
                    continue;
                }
                if (finishedQuests.Count >= 3 && count == 2)
                {
                    quest3Image.gameObject.SetActive(true);
                    quest3Image.sprite = loadQuestImage(cq);
                    count++;
                    continue;
                }
                if (finishedQuests.Count >= 4 && count == 3)
                {
                    quest4Image.gameObject.SetActive(true);
                    quest4Image.sprite = loadQuestImage(cq);
                    count++;
                    continue;
                }
                if (finishedQuests.Count >= 5 && count == 4)
                {
                    quest5Image.gameObject.SetActive(true);
                    quest5Image.sprite = loadQuestImage(cq);
                    count++;
                    continue;
                }
                if (finishedQuests.Count >= 6 && count == 5)
                {
                    quest6Image.gameObject.SetActive(true);
                    quest6Image.sprite = loadQuestImage(cq);
                    count++;
                    continue;
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

            finishedButton.setNeighbors(null, null, mainMenuButton, null);
            mainMenuButton.setNeighbors(null, null, null, finishedButton);

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
            if (cameraPositions == null)
            {

                GameObject obj = GameObject.Find("CameraPoints");
                if (obj == null) return;
                List<GameObject> objs= GameObject.FindGameObjectsWithTag("MainCamera").ToList();
                foreach(GameObject ok in objs)
                {
                    cameras.Add(ok.GetComponent<Camera>());
                }
                this.cameraPositions = new List<Vector3>();
                foreach (Transform t in obj.transform)
                {
                    cameraPositions.Add(t.position);
                }
                
                foreach(Camera c in cameras)
                {
                    c.gameObject.transform.position = cameraPositions[0] + new Vector3(0, 0, -10);
                }
                
            }
            checkForInput();
            cameraPan();
        }

        /// <summary>
        /// Pan the camera around the neighborhood.
        /// </summary>
        public void cameraPan()
        {
            if (cameraPositions.Count > 0)
            {
                if (currentLerp == 0)
                {

                }
                if (currentLerp >= 1f)
                {
                    currentLerp = 0;
                    positionIndex++;
                }
                currentLerp += lerpSpeed;

                foreach (Camera c in cameras)
                {
                    c.gameObject.transform.position = Vector3.Lerp(currentPos, nextPos, currentLerp)+new Vector3(0,0,-10);
                }
            }
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

            if (GameCursorMenu.SimulateMousePress(mainMenuButton))
            {
                Menu.Instantiate<ReturnToTitleConfirmationMenu>(true);
                (Game.Menu as ReturnToTitleConfirmationMenu).setNoFunctionality(restart);
            }
        }

        private static void restart()
        {
            Menu.Instantiate<EndofDayMenu>(true);
            // make sure that exclamation is disabled
            Game.Player.PlayerMovement.EscapedReset();
        }

    }
}
