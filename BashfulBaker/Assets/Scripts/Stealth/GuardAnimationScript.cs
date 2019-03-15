using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Stealth
{
    public class GuardAnimationScript:MonoBehaviour
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
                        animator.Play("GuardLeftWalkAnimation");
                    }
                    else
                    {
                        animator.Play("GuardRightWalkAnimation");
                    }
                }
                else if (nextTargetSpot.x > 0)
                {
                    if(flip==false)animator.Play("GuardRightWalkAnimation");
                    else
                    {
                        animator.Play("GuardLeftWalkAnimation");
                    }
                }
            }
            else if (Mathf.Abs(nextTargetSpot.x) < Mathf.Abs(nextTargetSpot.y))
            {
                if (nextTargetSpot.y < 0)
                {
                    if (flip == false)
                    {
                        animator.Play("GuardDownWalkAnimation");
                    }
                    else
                    {
                        animator.Play("GuardUpWalkAnimation");
                    }
                }
                else if (nextTargetSpot.y > 0)
                {
                    if (flip == false)
                    {
                        animator.Play("GuardUpWalkAnimation");
                    }
                    else
                    {
                        animator.Play("GuardDownWalkAnimation");
                    }
                }
            }
            else if (Mathf.Abs(nextTargetSpot.x) == Mathf.Abs(nextTargetSpot.y) && (nextTargetSpot.x != 0 && nextTargetSpot.y != 0))
            {
                if (nextTargetSpot.x < 0)
                {
                    if (flip == false)
                    {
                        animator.Play("GuardLeftWalkAnimation");
                    }
                    else
                    {
                        animator.Play("GuardRightWalkAnimation");
                    }
                }
                else if (nextTargetSpot.x > 0)
                {
                    if (flip == false) animator.Play("GuardRightWalkAnimation");
                    else
                    {
                        animator.Play("GuardLeftWalkAnimation");
                    }
                }
            }
            else if (nextTargetSpot.x == 0 && nextTargetSpot.y == 0)
            {
                animator.Play("GuardIdleAnimation");
            }
        }
    }
}
