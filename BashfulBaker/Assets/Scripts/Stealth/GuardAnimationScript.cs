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

        public void animateGuard(Vector3 currentPos,Vector3 nextPos)
        {
            Vector3 nextTargetSpot = nextPos - currentPos;

            if (Mathf.Abs(nextTargetSpot.x) > Mathf.Abs(nextTargetSpot.y))
            {
                if (nextTargetSpot.x < 0)
                {
                    animator.Play("GuardLeftWalkAnimation");
                }
                else if (nextTargetSpot.x > 0)
                {
                    animator.Play("GuardRightWalkAnimation");
                }
            }
            else if (Mathf.Abs(nextTargetSpot.x) < Mathf.Abs(nextTargetSpot.y))
            {
                if (nextTargetSpot.y < 0)
                {
                    animator.Play("GuardDownWalkAnimation");
                }
                else if (nextTargetSpot.y > 0)
                {
                    animator.Play("GuardUpWalkAnimation");
                }
            }
            else if (Mathf.Abs(nextTargetSpot.x) == Mathf.Abs(nextTargetSpot.y) && (nextTargetSpot.x != 0 && nextTargetSpot.y != 0))
            {
                if (nextTargetSpot.x < 0)
                {
                    animator.Play("GuardLeftWalkAnimation");
                }
                else if (nextTargetSpot.x > 0)
                {
                    animator.Play("GuardRightWalkAnimation");
                }
            }
            else if (nextTargetSpot.x == 0 && nextTargetSpot.y == 0)
            {
                animator.Play("GuardIdleAnimation");
            }
        }
    }
}
