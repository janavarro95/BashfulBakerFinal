using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.HUDS
{
    public class TimerHUD : HUD
    {
        private GameObject timerCanvas;
        private Text timeRemaining;
        private Image timerKnob;
        private Image giftImage;

        public float timerSpeedMultiplier=.5f;
        public float timerStartTime = 5f;

        /// <summary>
        /// Start the monobehaviour for the timer hud.
        /// </summary>
        public override void Start()
        {
            timerCanvas = this.gameObject.transform.Find("Canvas").gameObject;
            timeRemaining = timerCanvas.transform.Find("TimeText").GetComponent<Text>();
            timerKnob = timerCanvas.transform.Find("TimerKnob").GetComponent<Image>();

            giftImage = timerCanvas.gameObject.transform.Find("TimerImage").Find("Image").gameObject.GetComponent<Image>();
        }

        /// <summary>
        /// Update the timer HUD.
        /// </summary>
        public override void Update()
        {
            if (Game.PhaseTimer != null)
            {
#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.T)) Game.PhaseTimer.currentTime = Game.PhaseTimer.maxTime;
#endif

                Game.PhaseTimer.Update(.5f);

                if (Game.PhaseTimer.seconds % 2 == 0)
                {

                    timeRemaining.text = (timerStartTime+(Game.PhaseTimer.minutes) + ":" + parseSeconds()+" PM");
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

                    timeRemaining.text = (timerStartTime + (Game.PhaseTimer.minutes) + " " + parseSeconds()+" PM");
                }
                updateGiftImage();
                updateKnobRotation();
            }
            else
            {
                Game.HUD.showTimer = false;
            }
        }


        /// <summary>
        /// Says it all.
        /// </summary>
        private void oldUpdate()
        {
            if (Game.PhaseTimer != null)
            {
#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.T)) Game.PhaseTimer.currentTime = 1;
#endif

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
                updateGiftImage();
                updateKnobRotation();
            }
            else
            {
                Game.HUD.showTimer = false;
            }
        }

        private void updateGiftImage()
        {
            this.giftImage.transform.localPosition = Vector3.Lerp(new Vector3(-40, 35, 0), new Vector3(40, 35, 0),(float)Game.PhaseTimer.TimeFractionRemaining);
        }

        private void updateKnobRotation()
        {
            Quaternion q = timerKnob.rectTransform.localRotation;
            Vector3 euler = q.eulerAngles;
            euler.z = ((360f * (float)Game.PhaseTimer.currentTime / (float)Game.PhaseTimer.maxTime)*10f);
            q.eulerAngles = euler;
            timerKnob.rectTransform.localRotation = q;

        }

        /// <summary>
        /// Gets a proper display for the number of seconds.
        /// </summary>
        /// <returns></returns>
        private string parseSeconds()
        {
            string seconds = "";
            int time = Game.PhaseTimer.seconds;
            int first = (time / 10);
            int second = (time % 10<5)? 0:5;

            if (Game.PhaseTimer.seconds < 10)
            {
                seconds = "0" + second;
            }
            else
            {
                seconds = first.ToString()+second.ToString();
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
