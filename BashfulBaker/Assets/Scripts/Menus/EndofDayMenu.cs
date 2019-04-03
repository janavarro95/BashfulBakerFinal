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
        private List<GameObject> deliveriesMade;
        private List<GameObject> deliveriesRemaining;

        private List<KeyValuePair<string, string>> deliveriesMadeInfo;
        private List<KeyValuePair<string, string>> deliveriesRemainingInfo;

        private GameObject hoverInfo;
        private Text hoverText;
        private Image hoverImage;

        //public List<MenuComponent> snappableComponents;


        private Menus.Components.MenuComponent finishedButton;

        public override void Start()
        {
            Game.Menu = this;
            Game.HUD.showHUD = false;

            //GameInformation.Game.QuestManager.addQuest(new CookingQuest("Nuggies", "Ronald Mc.Donald", new List<string>() { "Fries" }));

            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();

            endOfDayText = canvas.transform.Find("EndOfDayX").Find("Text").gameObject.GetComponent<Text>();

            finishedButton = new Components.MenuComponent(canvas.transform.Find("Close Button").GetComponent<Button>());


            GameObject made = canvas.transform.Find("DeliveriesMade").gameObject;
            deliveriesMade = new List<GameObject>();
            foreach(Transform t in made.transform)
            {
                deliveriesMade.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }

            GameObject remaining = canvas.transform.Find("DeliveriesRemaining").gameObject;
            deliveriesRemaining = new List<GameObject>();
            foreach (Transform t in remaining.transform)
            {
                deliveriesRemaining.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }


            if (Game.CurrentDayNumber == 0) Game.CurrentDayNumber = 1;

            hoverInfo = canvas.transform.Find("HoverInfo").gameObject;
            hoverInfo.SetActive(false);
            hoverImage = hoverInfo.transform.Find("HoverPortrait").GetComponent<Image>();
            hoverText = hoverInfo.transform.Find("HoverText").GetComponent<Text>();

            deliveriesMadeInfo = new List<KeyValuePair<string, string>>();
            deliveriesRemainingInfo = new List<KeyValuePair<string, string>>();
            initializeQuestResults();
            setUpForSnapping();
        }

        public override void exitMenu()
        {
            Game.Menu = null;
            base.exitMenu();
        }

        public override void setUpForSnapping()
        {
            //UGGG
            MenuComponent firstDelivered=new MenuComponent(deliveriesMade[0].GetComponent<Image>());
            MenuComponent secondDelivered = new MenuComponent(deliveriesMade[1].GetComponent<Image>());
            MenuComponent thirdDelivered = new MenuComponent(deliveriesMade[2].GetComponent<Image>());
            MenuComponent fourthDelivered = new MenuComponent(deliveriesMade[3].GetComponent<Image>());

            MenuComponent firstFailed = new MenuComponent(deliveriesRemaining[0].GetComponent<Image>());
            MenuComponent secondFailed = new MenuComponent(deliveriesRemaining[1].GetComponent<Image>());
            MenuComponent thirdFailed = new MenuComponent(deliveriesRemaining[2].GetComponent<Image>());
            MenuComponent fourthFailed = new MenuComponent(deliveriesRemaining[3].GetComponent<Image>());

            firstDelivered.setNeighbors(null, secondDelivered, null, firstFailed);
            secondDelivered.setNeighbors(firstDelivered, thirdDelivered, null, secondFailed);
            thirdDelivered.setNeighbors(secondDelivered, fourthDelivered, null, thirdFailed);
            fourthDelivered.setNeighbors(thirdDelivered, null, null, fourthFailed);

            firstFailed.setNeighbors(null, secondFailed, firstDelivered, finishedButton);
            secondFailed.setNeighbors(firstFailed, thirdFailed, secondDelivered, finishedButton);
            thirdFailed.setNeighbors(secondFailed, fourthFailed, thirdDelivered, finishedButton);
            fourthFailed.setNeighbors(thirdFailed, null, fourthDelivered, finishedButton);

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
            endOfDayText.text = "End of Day: " + Game.CurrentDayNumber.ToString();
            checkForHover();
            checkForInput();
        }

        /// <summary>
        /// Initilaize the menu for start up.
        /// </summary>
        public void initializeQuestResults()
        {
            foreach(CookingQuest q in Game.QuestManager.quests)
            {
                if (q.IsCompleted == true) tickEnabledQuestDelivered(q);
                else tickEnabledQuestRemaining(q);
            }
        }

        /// <summary>
        /// Checks to see if any of the quest info for npcs has been hovered over yet.
        /// </summary>
        public void checkForHover()
        {
            bool hovered = false;
            for (int i = 0; i < deliveriesMade.Count; i++)
            {
                GameObject obj = deliveriesMade[i];
                if (obj.activeInHierarchy)
                {

                    if (GameCursorMenu.SimulateMouseHover(obj, false))
                    {
                        hoverInfo.SetActive(true);
                        hovered = true;
                        hoverText.text = deliveriesMadeInfo[i].Value + " for " + deliveriesMadeInfo[i].Key;
                    }
                }
            }

            for (int i = 0; i < deliveriesRemaining.Count; i++)
            {
               
                GameObject obj = deliveriesRemaining[i];
                if (obj.activeInHierarchy)
                {

                    if (GameCursorMenu.SimulateMouseHover(obj, false))
                    {
                        hoverInfo.SetActive(true);
                        hovered = true;
                        hoverText.text = deliveriesRemainingInfo[i].Value + " for " + deliveriesRemainingInfo[i].Key;
                    }
                }
            }

            if (hovered == false)
            {
                hoverInfo.SetActive(false);

            }
        }

        public void checkForInput()
        {
            if (GameCursorMenu.SimulateMousePress(finishedButton))
            {
                Game.returnToMainMenu();
            }
        }

        /// <summary>
        /// Ticks the number of active npc portraits to display completed quests up by 1.
        /// </summary>
        /// <param name="q"></param>
        private void tickEnabledQuestDelivered(CookingQuest q)
        {
            foreach (GameObject o in deliveriesMade)
            {
                if (o.activeInHierarchy == false)
                {
                    o.SetActive(true);
                    deliveriesMadeInfo.Add(new KeyValuePair<string, string>(q.PersonToDeliverTo, q.RequiredDish));
                    return;
                }
            }
        }

        /// <summary>
        /// Ticks the number of active npc portraits to display incompleted quests up by 1.
        /// </summary>
        /// <param name="q"></param>
        private void tickEnabledQuestRemaining(CookingQuest q)
        {
            foreach (GameObject o in deliveriesRemaining)
            {
                if (o.activeInHierarchy == false)
                {
                    o.SetActive(true);
                    deliveriesRemainingInfo.Add(new KeyValuePair<string, string>(q.PersonToDeliverTo, q.RequiredDish));
                    return;
                }
            }
        }

    }
}
