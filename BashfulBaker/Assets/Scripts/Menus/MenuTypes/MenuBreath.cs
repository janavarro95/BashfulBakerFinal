using Assets.Scripts.Utilities.Delegates;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    [System.Serializable]
    public class MenuBreath
    {
        DeltaTimer timer;
        Canvas canvas;
        VoidDelegate intro;
        VoidDelegate outro;
        double time;

        public MenuBreath(Canvas c, double time,VoidDelegate onFinishedBreathIn,VoidDelegate onFinishedBreathOut)
        {
            this.canvas = c;
            this.intro = onFinishedBreathIn;
            this.outro = onFinishedBreathOut;
            this.time = time;
        }

        public void breathIn()
        {
            this.timer = new DeltaTimer(time, Enums.TimerType.CountUp, false, intro);
            this.timer.start();
        }
        public void breathOut()
        {
            this.timer = new DeltaTimer(time, Enums.TimerType.CountDown, false, outro);
            this.timer.start();
        }

        public bool Update()
        {
            timer.Update();
            canvas.scaleFactor = (float)(timer.currentTime / timer.maxTime);
            if (timer.state != Enums.TimerState.Finished) return false;
            else return true;
        }

    }
}
