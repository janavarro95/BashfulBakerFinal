﻿using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class Menu:MonoBehaviour
    {
        [SerializeField]
        Button StartButton;

        public virtual void Start()
        {
            StartButton.Select();
            
        }

        public virtual void Update()
        {

        }

        public virtual void exitMenu()
        {
            Destroy(this.gameObject);
        }


        public static void Instantiate()
        {
            Instantiate<Menu>();
        }

        public static void Instantiate<T>()
        {
            if(typeof(T) == typeof(Menu))
            {
                Instantiate("Menu");
            }
            if (typeof(T) == typeof(MainMenu))
            {
                Instantiate("MainMenu");
            }

            else
            {
                Instantiate("Menu");
            }
        }

        public static void Instantiate(string Name)
        {
            Game.Menu=LoadMenuFromPrefab(Name).GetComponent<Menu>();
        }

        protected static GameObject LoadMenuFromPrefab(string ItemName)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Menus"), ItemName);
            GameObject menuObj=Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
            return menuObj;
        }
    }
}
