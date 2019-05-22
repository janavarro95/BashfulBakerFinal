using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Stealth
{
    public class DogAnimationScript:GuardAnimationScript
    {
        public override void animateGuard(Vector3 currentPos, Vector3 nextPos, bool flip=false)
        {
            Vector3 nextTargetSpot = nextPos - currentPos;
            
            if (nextTargetSpot.x < 0)
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
            else if (nextTargetSpot.x > 0)
            {
                if(flip==false)animator.Play("dog_walk_left");
                else
                {
                    animator.Play("dog_walk_right");
                }
            }
            else if (nextTargetSpot.y < 0)
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
            else if (nextTargetSpot.y > 0)
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
            else if (nextTargetSpot.x == 0 && nextTargetSpot.y == 0)
            {
                // add more function for idle flipping
                if (flip == false)
                {
                    animator.Play("dog_idle_right");
                }
                else
                {
                    animator.Play("dog_idle_left");
                }
            }
        }
    }
}
