using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class MultiDirectionalSprite
    {
        public Texture2D front;
        public Texture2D left;
        public Texture2D right;

        public MultiDirectionalSprite()
        {

        }

        public MultiDirectionalSprite(Texture2D Front, Texture2D Left, Texture2D Right)
        {
            this.front = Front;
            this.left = Left;
            this.right = Right;
        }

        public Texture2D getTextureFromDirection(Enums.FacingDirection Dir)
        {
            if(Dir== Enums.FacingDirection.Down)
            {
                return front;
            }
            if(Dir== Enums.FacingDirection.Left)
            {
                return left;
            }
            if(Dir== Enums.FacingDirection.Right)
            {
                return right;
            }
            if(Dir== Enums.FacingDirection.Up)
            {
                return front;
            }
            return front;
        }
    }
}
