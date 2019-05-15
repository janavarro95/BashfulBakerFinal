using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Stealth
{
    public class DogAnimationScript:GuardAnimationScript
    {
        public Animator animator;

        public void Start()
        {
            animator = this.gameObject.GetComponent<Animator>();
        }

        public void animateGuard(Vector3 currentPos,Vector3 nextPos,bool flip=false)
        {
            Vector3 nextTargetSpot = nextPos - currentPos;

            if (Mathf.Abs(nextTargetSpot.x) > Mathf.Abs(nextTargetSpot.y))
            {
                if (nextTargetSpot.x < 0)
                {
                    if (flip == false)
                    {
                        animator.Play("dog_walk_left");
                    }
                    else
                    {
                        animator.Play("dog_walk_right");
                    }
                }
                else if (nextTargetSpot.x > 0)
                {
                    if(flip==false)animator.Play("dog_walk_right");
                    else
                    {
                        animator.Play("dog_walk_left");
                    }
                }
            }
            else if (Mathf.Abs(nextTargetSpot.x) < Mathf.Abs(nextTargetSpot.y))
            {
                if (nextTargetSpot.y < 0)
                {
                    if (flip == false)
                    {
                        animator.Play("dog_walk_left");
                    }
                    else
                    {
                        animator.Play("dog_walk_right");
                    }
                }
                else if (nextTargetSpot.y > 0)
                {
                    if (flip == false)
                    {
                        animator.Play("dog_walk_right");
                    }
                    else
                    {
                        animator.Play("dog_walk_left");
                    }
                }
            }
            else if (Mathf.Abs(nextTargetSpot.x) == Mathf.Abs(nextTargetSpot.y) && (nextTargetSpot.x != 0 && nextTargetSpot.y != 0))
            {
                if (nextTargetSpot.x < 0)
                {
                    if (flip == false)
                    {
                        animator.Play("dog_walk_left");
                    }
                    else
                    {
                        animator.Play("dog_walk_right");
                    }
                }
                else if (nextTargetSpot.x > 0)
                {
                    if (flip == false) animator.Play("dog_walk_right");
                    else
                    {
                        animator.Play("dog_walk_left");
                    }
                }
            }
            else if (nextTargetSpot.x == 0 && nextTargetSpot.y == 0)
            {
                // add more function for idle flipping
                animator.Play("dog_idle_left");
            }
        }
    }
}
