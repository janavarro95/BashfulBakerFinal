using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HeldObjectAnimator:MonoBehaviour
    {
        public Animator animator;

        public void playAnimation(Enums.FacingDirection direction, bool IsMoving)
        {
            if (IsMoving)
            {
                if (direction == Enums.FacingDirection.Down)
                {
                    animator.Play("FWalk");
                }
                if (direction == Enums.FacingDirection.Left)
                {
                    animator.Play("LWalk");

                }
                if (direction == Enums.FacingDirection.Right)
                {
                    animator.Play("RWalk");
                }
                if (direction == Enums.FacingDirection.Up)
                {
                    animator.Play("BWalk");
                }
            }
            else
            {
                if (direction == Enums.FacingDirection.Down)
                {
                    animator.Play("DWalk");
                }
                if (direction == Enums.FacingDirection.Left)
                {
                    animator.Play("LWalk");
                }
                if (direction == Enums.FacingDirection.Right)
                {
                    animator.Play("RWalk");
                }
                if (direction == Enums.FacingDirection.Up)
                {
                    animator.Play("BWalk");
                }
            }
        }

        public void loadAnimatorFromPrefab(string heldObjectName)
        {
            if(heldObjectName=="Chocolate Chip Cookies" || heldObjectName=="Chocolate Chip Cookie")
            {
               this.animator= Resources.Load<Animator>(Path.Combine("CarryingAnimations", heldObjectName));
            }
        }
    }
}
