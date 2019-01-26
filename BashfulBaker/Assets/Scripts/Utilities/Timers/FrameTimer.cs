using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utilities.Delegates;

namespace Assets.Scripts.Utilities.Timers
{

    /// <summary>
    /// A timer that ticks down every time update is called.
    /// Note, must call timer.Update in an appropriate other script since this does not derive from MonoBehavior.
    /// </summary>
    public class FrameTimer
    {
        /// <summary>
        /// The number of frames (update ticks) remaining until this timer expires.
        /// </summary>
        public int lifespanRemaining;
        /// <summary>
        /// The max amount of frames this timer has been assigned.
        /// </summary>
        public int maxLifespan;
        /// <summary>
        /// Should the timer restart itself when it counts down to 0.
        /// </summary>
        public bool autoRestart;
        /// <summary>
        /// The function that is called when the timer hits zero frames remaining.
        /// </summary>
        public VoidDelegate onFinished;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="LifeSpan">How many frames aka update ticks should this timer exist for.</param>
        public FrameTimer(int LifeSpan,VoidDelegate OnFinished=null,bool AutoRestart=false)
        {
            this.maxLifespan = LifeSpan;
            this.lifespanRemaining = this.maxLifespan;
            this.autoRestart = AutoRestart;
            this.onFinished = OnFinished;
        }

        /// <summary>
        /// Ticks the timer's remaining lifespan down one frame.
        /// </summary>
        public void tick()
        {
            if (lifespanRemaining >= 0)
            {
                lifespanRemaining--;
                if (finished())
                {
                    invoke();
                    if (autoRestart) lifespanRemaining = maxLifespan;
                }
            }
            else return;
        }

        /// <summary>
        /// Checks if the timer has finished running.
        /// </summary>
        /// <returns></returns>
        public bool finished()
        {
            return this.lifespanRemaining <= 0;
        }

        /// <summary>
        /// Wrapper for tick(); Does the exact same thing.
        /// </summary>
        public void Update()
        {
            tick();
        }

        /// <summary>
        /// Calls whatever function is associated with this timer when it has expired.
        /// </summary>
        public void invoke()
        {
            if (this.onFinished != null)
            {
                this.onFinished.Invoke();
            }
        }

    }
}
