using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
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
        public GameCursorMenu menuCursor;


        public virtual void Start()
        {
           
            
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

        public static void Instantiate<T>(bool OverrideMenu=false)
        {
            if(typeof(T) == typeof(Menu))
            {
                Instantiate("Menu",OverrideMenu);
            }
            else if (typeof(T) == typeof(MainMenu))
            {
                Instantiate("MainMenu",OverrideMenu);
            }
            else if(typeof(T)== typeof(OptionsMenu))
            {
                Instantiate("OptionsMenu",OverrideMenu);
            }
            else if(typeof(T)== typeof(InventoryMenu))
            {
                Instantiate("InventoryMenu",OverrideMenu);
            }
            else if(typeof(T)== typeof(PantryMenu))
            {
                Instantiate("PantryMenu",OverrideMenu);
            }
            else if (typeof(T) == typeof(GameMenu))
            {
                Instantiate("GameMenu", OverrideMenu);
            }
            else if (typeof(T) == typeof(ReturnToTitleConfirmationMenu))
            {
                Instantiate("ReturnToTitleConfirmationMenu", OverrideMenu);
            }
            else
            {
                throw new Exception("Hmm trying to call on a type of menu that doesn't exist.");
            }
        }

        public static void Instantiate(string Name,bool OverrideCurrentMenu=false)
        {
            if (OverrideCurrentMenu == false)
            {
                if (Game.IsMenuUp) return;
                Game.Menu = LoadMenuFromPrefab(Name).GetComponent<Menu>();
            }
            else
            {
                Game.Menu.exitMenu();
                Game.Menu = LoadMenuFromPrefab(Name).GetComponent<Menu>();
            }
            
        }

        protected static GameObject LoadMenuFromPrefab(string ItemName)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Menus"), ItemName);
            GameObject menuObj=Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
            return menuObj;
        }
    }
}
