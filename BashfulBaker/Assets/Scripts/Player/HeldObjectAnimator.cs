using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
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


        public Dish displayedDish;

        public void Start()
        {
            this.animator = this.gameObject.GetComponent<Animator>();
        }


        public void playAnimation(Enums.FacingDirection direction, bool IsMoving,Dish D)
        {
            this.displayedDish = D;
            if (this.animator == null || this.animator.runtimeAnimatorController==null)
            {
                loadAnimatorFromPrefab(Game.Player.activeItem.Name);
            }

            if (D.currentDishState == Enums.DishState.Ingredients)
            {
                if (IsMoving)
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FWalk_Ingredients");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LWalk_Ingredients");

                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RWalk_Ingredients");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
                else
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FIdle_Ingredients");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LIdle_Ingredients");
                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RIdle_Ingredients");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
            }
            if (D.currentDishState == Enums.DishState.Mixed)
            {
                if (IsMoving)
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FWalk_Mixed");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LWalk_Mixed");

                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RWalk_Mixed");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
                else
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FIdle_Mixed");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LIdle_Mixed");
                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RIdle_Mixed");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
            }
            if (D.currentDishState == Enums.DishState.Prepped)
            {
                if (IsMoving)
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FWalk_Prepped");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LWalk_Prepped");

                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RWalk_Prepped");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
                else
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FIdle_Prepped");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LIdle_Prepped");
                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RIdle_Prepped");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
            }
            if (D.currentDishState == Enums.DishState.Baked)
            {
                if (IsMoving)
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FWalk_Baked");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LWalk_Baked");

                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RWalk_Baked");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
                else
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FIdle_Baked");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LIdle_Baked");
                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RIdle_Baked");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
            }
            if (D.currentDishState == Enums.DishState.Burnt)
            {
                if (IsMoving)
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FWalk_Burnt");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LWalk_Burnt");

                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RWalk_Burnt");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
                else
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FIdle_Burnt");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LIdle_Burnt");
                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RIdle_Burnt");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }

            }
            if (D.currentDishState == Enums.DishState.Packaged)
            {
                if (IsMoving)
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FWalk_Packaged");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LWalk_Packaged");

                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RWalk_Packaged");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
                else
                {
                    if (direction == Enums.FacingDirection.Down)
                    {
                        animator.Play("FIdle_Packaged");
                    }
                    if (direction == Enums.FacingDirection.Left)
                    {
                        animator.Play("LIdle_Packaged");
                    }
                    if (direction == Enums.FacingDirection.Right)
                    {
                        animator.Play("RIdle_Packaged");
                    }
                    if (direction == Enums.FacingDirection.Up)
                    {
                        //animator.Play("BWalk");
                    }
                }
            }
        }

        public void loadAnimatorFromPrefab(string heldObjectName)
        {
            if(heldObjectName=="Chocolate Chip Cookies" || heldObjectName=="Mint Chip Cookies" || heldObjectName=="Oatmeal Raisin Cookies" || heldObjectName=="Pecan Crescent Cookies")
            {
                
                this.animator = this.gameObject.GetComponent<Animator>();
                this.animator.runtimeAnimatorController= Resources.Load<RuntimeAnimatorController>("CarryingAnimations/"+heldObjectName);
            }
        }

        public void clearAnimationController()
        {
            if (this.animator == null)
            {
                return;
            }
            else
            {
                this.animator.runtimeAnimatorController = null;
            }
        }
    }
}
