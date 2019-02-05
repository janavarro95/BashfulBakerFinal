﻿using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// Deals with dealing with editing the game's options.
    /// </summary>
    public class OptionsMenu:Menu
    {
        [SerializeField]
        Button exitButton;

        [SerializeField]
        Slider sfxSlider;
        [SerializeField]
        Slider musicSlider;

        [SerializeField]
        Toggle muteToggle;

        public override void Start()
        {
            GameObject canvas = this.transform.Find("Canvas").gameObject;

            Canvas actualCanvas = canvas.GetComponent<Canvas>();
            actualCanvas.worldCamera = Camera.main;

            exitButton = canvas.transform.Find("QuitButton").gameObject.GetComponent<Button>();

            sfxSlider = canvas.transform.Find("SFXSlider").gameObject.GetComponent<Slider>();
            musicSlider = canvas.transform.Find("MusicSlider").gameObject.GetComponent<Slider>();

            muteToggle = canvas.transform.Find("MuteToggle").gameObject.GetComponent<Toggle>();

            sfxSlider.value = Game.Options.sfxVolume;
            musicSlider.value = Game.Options.musicVolume;
            muteToggle.isOn = Game.Options.muteVolume;

            menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }

        public override void Update()
        {
            if (GameInput.GameCursorMenu.SimulateMousePress(exitButton))
            {
                Debug.Log("HELLO");
                this.exitMenu();
            }

            if (GameInput.GameCursorMenu.SimulateMousePress(sfxSlider))
            {
                sfxSlider.Select();
            }
            if (GameInput.GameCursorMenu.SimulateMousePress(musicSlider))
            {
                musicSlider.Select();
            }
            if (GameInput.GameCursorMenu.SimulateMousePress(muteToggle))
            {
                muteToggle.Select();
            }


        }

        /// <summary>
        /// https://forum.unity.com/threads/gamepad-precision-with-sliders.381802/
        /// </summary>
        private void checkForSliderUpdate()
        {
            if (musicSlider == EventSystem.current.currentSelectedGameObject)
            {
                float sliderChange = Input.GetAxis("Horizontal");
                float sliderValue = musicSlider.value;
                float tempValue = sliderValue + sliderChange;
                if (tempValue <= musicSlider.maxValue && tempValue >= musicSlider.minValue)
                {
                    sliderValue = tempValue;
                }
                musicSlider.value = sliderValue;
            }

            if (sfxSlider == EventSystem.current.currentSelectedGameObject)
            {
                float sliderChange = Input.GetAxis("Horizontal");
                float sliderValue = sfxSlider.value;
                float tempValue = sliderValue + sliderChange;
                if (tempValue <= sfxSlider.maxValue && tempValue >= sfxSlider.minValue)
                {
                    sliderValue = tempValue;
                }
                sfxSlider.value = sliderValue;
            }
        }

        /// <summary>
        /// What happens when the exit button is clicked.
        /// </summary>
        public override void exitMenu()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Menu.Instantiate<MainMenu>();
                base.exitMenu();
                GameInformation.Game.Menu = null;
            }
            else
            {
                base.exitMenu();
                GameInformation.Game.Menu = null;
            }
        }

        public void onSFXVolumeChanged()
        {
            Game.Options.sfxVolume = sfxSlider.value;
        }

        public void onMusicVolumeChanged()
        {
            Game.Options.musicVolume = musicSlider.value;
        }

        public void muteButtonClicked()
        {
            Game.Options.muteVolume = muteToggle.isOn;
        }

    }
}