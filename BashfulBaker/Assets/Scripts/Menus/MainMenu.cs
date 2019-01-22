using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Menus
{
    public class MainMenu:Menu
    {
        public override void exitMenu()
        {
            Destroy(this.gameObject);
        }   
    }
}
