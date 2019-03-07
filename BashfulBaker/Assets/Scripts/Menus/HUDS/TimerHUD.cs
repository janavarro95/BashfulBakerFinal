using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.HUDS
{
    public class TimerHUD:HUD
    {
        private GameObject timerCanvas;
        private Text timeRemaining;

        /// <summary>
        /// Start the monobehaviour for the timer hud.
        /// </summary>
        public override void Start()
        {
            timerCanvas = this.gameObject.transform.Find("Canvas").gameObject;
            timeRemaining = timerCanvas.transform.Find("TimeText").GetComponent<Text>();
        }

        /// <summary>
        /// Update the timer HUD.
        /// </summary>
        public override void Update()
        {
            if (Game.PhaseTimer != null)
            {
                Game.PhaseTimer.Update();

                if (Game.PhaseTimer.seconds % 2 == 0)
                {
                 
                    timeRemaining.text = Game.PhaseTimer.minutes + ":" + parseSeconds();
                }
                else
                {
                    string seconds = "";
                    if (Game.PhaseTimer.seconds < 10)
                    {
                        seconds = "0" + Game.PhaseTimer.seconds;
                    }
                    else
                    {
                        seconds = Game.PhaseTimer.seconds.ToString();
                    }


                    timeRemaining.text = Game.PhaseTimer.minutes + " " + parseSeconds();
                }
            }
            else
            {
                Game.HUD.showTimer = false;
            }
        }

        /// <summary>
        /// Gets a proper display for the number of seconds.
        /// </summary>
        /// <returns></returns>
        private string parseSeconds()
        {
            string seconds = "";
            if (Game.PhaseTimer.seconds < 10)
            {
                seconds = "0" + Game.PhaseTimer.seconds;
            }
            else
            {
                seconds = Game.PhaseTimer.seconds.ToString();
            }

            return seconds;
        }

        public override void setVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible) timerCanvas.SetActive(false);
            if (visibility == Enums.Visibility.Visible) timerCanvas.SetActive(true);
        }
    }
}
