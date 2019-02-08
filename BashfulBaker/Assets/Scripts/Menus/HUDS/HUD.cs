using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus.HUDS
{
    public class HUD: MonoBehaviour
    {

        private GameObject canvas;

        public virtual void Start()
        {
            canvas = this.gameObject.transform.Find("Canvas").gameObject;
        }

        public virtual void Update()
        {
            
        }

        public virtual void setVisibility(Enums.Visibility visibility)
        {
           if (visibility == Enums.Visibility.Invisible) canvas.SetActive(false);
           if (visibility == Enums.Visibility.Visible) canvas.SetActive(true);
        }

        
    }
}
