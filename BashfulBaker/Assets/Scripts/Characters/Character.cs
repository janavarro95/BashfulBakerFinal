using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class Character :MonoBehaviour
    {
        public string characterName;

        public string Name
        {
            get
            {
                return characterName;
            }
        }

        public Sprite facePortrait;
        public Sprite npcSprite;


        public void Start()
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = this.npcSprite;
        }
    }
}
