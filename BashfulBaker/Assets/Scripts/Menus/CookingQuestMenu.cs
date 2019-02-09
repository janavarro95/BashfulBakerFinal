using Assets.Scripts.GameInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class CookingQuestMenu:Menu
    {

        GameObject canvas;

        private List<GameObject> questObjects;

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
            bool questHovered = false;
            foreach(GameObject obj in questObjects)
            {
                if (obj.activeInHierarchy)
                {
                    if (GameCursorMenu.SimulateMouseHover(obj, false))
                    {
                        Debug.Log("AHHHHHHHH A QUEST HOVER!");
                        questHovered = true;
                    }
                }
            }
            if (questHovered == false)
            {
                //Disable/hide the right info menu;
            }

            //if hovering over quest sheet display the info on the right.
        }

        


    }
}
