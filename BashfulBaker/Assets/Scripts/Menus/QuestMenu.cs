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
    public class QuestMenu:Menu
    {

        GameObject canvas;

        private List<GameObject> questObjects;

        private List<Quest> heldCookingQuests;
        private List<Quest> heldDeliveryQuests;

        Text foodName;
        Text targetNPC;
        Text listOfIngredients;

        private Button exitButton;

        Image cookedImage;
        Image deliveredImage;
        Image specialImage;

        [SerializeField]
        Sprite noSprite;
        [SerializeField]
        Sprite yesSprite;

        [SerializeField]
        Sprite specialSprite;


        public override void Start()
        {
           

            questObjects = new List<GameObject>();
            GameObject canvas = this.transform.Find("Canvas").gameObject;
            GameObject icons=canvas.transform.Find("QuestIcons").gameObject;
            foreach(Transform t in icons.transform)
            {
                questObjects.Add(t.gameObject);
            }



            menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();


            GameObject info = canvas.transform.Find("QuestInfoBackground").gameObject;

            foodName = info.transform.Find("FoodName").gameObject.GetComponent<Text>();
            targetNPC = info.transform.Find("TargetNPC").gameObject.GetComponent<Text>();
            listOfIngredients = info.transform.Find("ListOfIngredients").gameObject.GetComponent<Text>();

            exitButton = canvas.transform.Find("CloseButton").GetComponent<Button>();

            cookedImage = info.transform.Find("CookedStatus").Find("StatusImage").gameObject.GetComponent<Image>();
            deliveredImage = info.transform.Find("DeliveredStatus").Find("StatusImage").gameObject.GetComponent<Image>();
            specialImage = info.transform.Find("SpecialStatus").Find("StatusImage").gameObject.GetComponent<Image>();

            setUpMenuForDisplay();


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
            heldCookingQuests = quests;

        }

        private void disableAllQuests()
        {
            foreach(GameObject obj in questObjects)
            {
                obj.SetActive(false);
            }
            deliveredImage.enabled = false;
            cookedImage.enabled = false;
            specialImage.enabled = false;
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
                        foodName.text = (heldCookingQuests[i] as CookingQuest).RequiredDish;
                        targetNPC.text = (heldCookingQuests[i] as CookingQuest).PersonToDeliverTo;
                        StringBuilder ingredients = new StringBuilder();
                        foreach (string ingredient in (heldCookingQuests[i] as CookingQuest).wantedIngredients)
                        {
                            ingredients.Append(ingredient);
                            ingredients.Append(Environment.NewLine);
                        }
                        listOfIngredients.text = ingredients.ToString();
                        cookedImage.enabled = true;
                        deliveredImage.enabled = true;
                        specialImage.enabled = true;
                        cookedImage.sprite = (heldCookingQuests[i] as CookingQuest).HasBeenCooked ? yesSprite : noSprite;
                        deliveredImage.sprite = (heldCookingQuests[i] as CookingQuest).HasBeenDelivered ? yesSprite : noSprite;
                        specialImage.sprite= (heldCookingQuests[i] as CookingQuest).SpecialMissionCompleted ? specialSprite : noSprite;
                    }
                    else
                    {
                        foodName.text = "";
                        targetNPC.text = "";
                        listOfIngredients.text = "";
                        cookedImage.enabled = false;
                        deliveredImage.enabled = false;
                        specialImage.enabled = false;
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
