using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.U2D;

namespace Assets.Scripts.Stealth
{
    public class StealthManager : MonoBehaviour
    {
        /// <summary>
        /// A list of all of the guards that are aware of Dane.
        /// </summary>
        public List<StealthAwarenessZone> AwareGuards = new List<StealthAwarenessZone>();

        public AnxietyMeter AnxietyMeter;
        public float anxietyGain = 0.002f;


        /// <summary>
        /// Are there any guards aware of Dane?
        /// </summary>
        public bool AreGuardsAware
        {
            get
            {
                return AwareGuards.Count > 0;
            }
        }

        /// <summary>
        /// Adds a guard to the list of aware guards.
        /// </summary>
        /// <param name="Guard"></param>
        public void AddAwareGuard(StealthAwarenessZone Guard)
        {
            if (this.AwareGuards.Contains(Guard)) return;
            AwareGuards.Add(Guard);
        }

        /// <summary>
        /// Removes a guard from the list of aware guards.
        /// </summary>
        /// <param name="Guard"></param>
        public void RemoveAwareGuard(StealthAwarenessZone Guard)
        {
            if (!this.AwareGuards.Contains(Guard)) return;
            AwareGuards.Remove(Guard);
        }

        public void ClearAlertGuards()
        {
            this.AwareGuards.Clear();
        }

        public void caughtByGuard()
        {
            this.AnxietyMeter.caughtByGuard();
            ClearAlertGuards();
        }



        public void Start()
        {
            if (GameInformation.Game.StealthManager == null)
            {
                GameInformation.Game.StealthManager = this;
                DontDestroyOnLoad(this.gameObject);
            }
            if (AnxietyMeter == null) AnxietyMeter = new AnxietyMeter(Camera.main);
        }

        public void Update()
        {
            return;
            if (AreGuardsAware == true)
            {
                AnxietyMeter.gainAnxiety(this.anxietyGain);
            }
            else
            {
                AnxietyMeter.relax(this.anxietyGain);
            }
        }

        public void OnLevelWasLoaded(int level)
        {
            if (this.AnxietyMeter != null)
            {
                this.AnxietyMeter.setCamera(Camera.main);
            }
        }
    }
}
