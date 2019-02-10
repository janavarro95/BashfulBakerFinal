using Assets.Scripts.GameInput;
using Assets.Scripts.QuestSystem.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// TODO:Disable/show quests on the right info menu when hivering/not hovering.
    /// </summary>
    public class CookingQuestMenu:Menu
    {

        GameObject canvas;

        private List<GameObject> questObjects;

        private List<Quest> heldQuests;

        Text foodName;
        Text targetNPC;
        Text listOfIngredients;

        private Button exitButton;

        public override void Start()
        {
           

            questObjects = new List<GameObject>();
            GameObject canvas = this.transform.Find("Canvas").gameObject;
            GameObject icons=canvas.transform.Find("QuestIcons").gameObject;
            foreach(Transform t in icons.transform)
            {
                questObjects.Add(t.gameObject);
            }

            setUpMenuForDisplay();
            menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();


            GameObject info = canvas.transform.Find("QuestInfoBackground").gameObject;

            foodName=info.transform.Find("FoodName").gameObject.GetComponent<Text>();
            targetNPC = info.transform.Find("TargetNPC").gameObject.GetComponent<Text>();
            listOfIngredients = info.transform.Find("ListOfIngredients").gameObject.GetComponent<Text>();

            exitButton = canvas.transform.Find("CloseButton").GetComponent<Button>();

        }

        private void setUpMenuForDisplay()
        {
            disableAllQuests();

            List<QuestSystem.Quests.Quest> quests= GameInformation.Game.QuestManager.quests.FindAll(q => q.GetType() == typeof(QuestSystem.Quests.CookingQuest));
            int count = quests.Count;

            for(int i = 0; i < count; i++)
            {
                questObjects[i].SetActive(true);
            }
            heldQuests = quests;

        }

        private void disableAllQuests()
        {
            foreach(GameObject obj in questObjects)
            {
                obj.SetActive(false);
            }
        }

        public override void Update()
        {
            checkForQuestHover();

            if (GameCursorMenu.SimulateMousePress(exitButton))
            {
                exitButtonPress();
            }

            //if hovering over quest sheet display the info on the right.
        }

        private void checkForQuestHover()
        {
            bool questHovered = false;


            for (int i = 0; i < questObjects.Count; i++)
            {
                GameObject obj = questObjects[i];
                if (obj.activeInHierarchy)
                {
                    if (GameCursorMenu.SimulateMouseHover(obj, false))
                    {
                        //Debug.Log("AHHHHHHHH A QUEST HOVER!");
                        questHovered = true;
                        foodName.text = (heldQuests[i] as CookingQuest).RequiredDish;
                        targetNPC.text = (heldQuests[i] as CookingQuest).PersonToDeliverTo;
                        StringBuilder ingredients = new StringBuilder();
                        foreach (string ingredient in (heldQuests[i] as CookingQuest).wantedIngredients)
                        {
                            ingredients.Append(ingredient);
                            ingredients.Append(Environment.NewLine);
                        }
                        listOfIngredients.text = ingredients.ToString();

                    }
                    else
                    {
                        foodName.text = "";
                        targetNPC.text = "";
                        listOfIngredients.text = "";
                        continue;
                    }
                }
            }


            if (questHovered == false)
            {
                //Debug.Log("WHAAAAAAAA");
                //Disable/hide the right info menu;
                //foodName.text = "";
                //targetNPC.text = "";
                //listOfIngredients.text = "";
            }
        }

        
        public void exitButtonPress()
        {
            this.exitMenu();
        }

    }
}
