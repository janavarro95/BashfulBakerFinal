using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TimerType = Assets.Scripts.Enums.TimerType;
using TimerState = Assets.Scripts.Enums.TimerState;
using Assets.Scripts.Utilities.Delegates;

namespace Assets.Scripts.Utilities.Timers
{
    /// <summary>
    /// Experimental timer class which uses Unity's delta time. Make sure to call this timer's update function in an appropriate monobehavior script!
    /// </summary>
    [SerializeField,Serializable]
    public class DeltaTimer
    {
        public float currentTime;
        /// <summary>
        /// The time (in seconds) it should take this timer to tick to completion. Note it is a float so you can have fractions of a second.
        /// </summary>
        public float maxTime;

        public TimerType type;
        public TimerState state;

        public Assets.Scripts.Utilities.Delegates.VoidDelegate onFinished;

        public bool autoRestart;

        public DeltaTimer()
        {

            state = TimerState.Initialized;
        }

        public DeltaTimer(int TimeToCompletion,TimerType Type,bool AutoRestart, VoidDelegate OnFinished=null)
        {
            this.type = Type;
            this.autoRestart = AutoRestart;
            if(Type == TimerType.CountDown)
            {
                this.currentTime = TimeToCompletion;
            }
            else if( Type== TimerType.CountUp)
            {
                this.currentTime = TimeToCompletion;
            }
            this.maxTime = TimeToCompletion;
            this.onFinished = OnFinished;
        }


        public void start()
        {
            this.state = TimerState.Ticking;
        }
        public void stop()
        {
            this.currentTime = -1;
            this.state = TimerState.Stopped;
        }
        public void pause()
        {
            this.state = TimerState.Paused;
        }
        public void resume()
        {
            this.state = TimerState.Ticking;
        }

        public void restart()
        {
            if (type == TimerType.CountDown)
            {
                currentTime = maxTime;
            }
            else if (type == TimerType.CountUp)
            {
                currentTime = 0;
            }
            this.state = TimerState.Ticking;
        }


        public bool IsInitialized
        {
            get
            {
                return this.state == TimerState.Initialized;
            }
        }

        public bool IsTicking
        {
            get
            {
                return this.state == TimerState.Ticking;
            }
        }

        public bool IsPaused
        {
            get
            {
                return this.state == TimerState.Paused;
            }
        }

        public bool IsStopped
        {
            get
            {
                return this.state == TimerState.Stopped;
            }
        }

        public bool IsFinished
        {
            get
            {
                return this.state == TimerState.Finished;
            }
        }

        public void tick()
        {
            Update();
        }

        public void Update()
        {
            if (state != TimerState.Ticking) return; //Only update if timer should tick.

            if (type == TimerType.CountUp)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= maxTime)
                {
                    //do something
                    invoke();
                    state = TimerState.Finished;
                    if (autoRestart == true) restart();
                }
            }
            else if(type== TimerType.CountDown)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    //do something.
                    invoke();
                    state = TimerState.Finished;
                    if (autoRestart == true) restart();
                }
            }
        }

        private void invoke()
        {
            if (this.onFinished == null) return;
            else onFinished.Invoke();
        }

    }
}
