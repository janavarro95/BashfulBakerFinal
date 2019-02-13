using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
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
        MenuComponent exitButton;

        [SerializeField]
        SliderComponent sfxSlider;
        [SerializeField]
        SliderComponent musicSlider;

        [SerializeField]
        ToggleComponent muteToggle;

        public override void Start()
        {
            GameObject canvas = this.transform.Find("Canvas").gameObject;

            Canvas actualCanvas = canvas.GetComponent<Canvas>();
            actualCanvas.worldCamera = Camera.main;

            exitButton =new MenuComponent(canvas.transform.Find("QuitButton").gameObject.GetComponent<Button>());

            sfxSlider = new SliderComponent(canvas.transform.Find("SFXSlider").gameObject.GetComponent<Slider>());
            musicSlider = new SliderComponent(canvas.transform.Find("MusicSlider").gameObject.GetComponent<Slider>());

            muteToggle =new ToggleComponent(canvas.transform.Find("MuteToggle").gameObject.GetComponent<Toggle>());

            sfxSlider.value = Game.Options.sfxVolume;
            musicSlider.value = Game.Options.musicVolume;
            muteToggle.isOn = Game.Options.muteVolume;

            menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;

            setUpForSnapping();
        }

        public override void setUpForSnapping()
        {
            musicSlider.setNeighbors(null, null, null, sfxSlider);
            sfxSlider.setNeighbors(null, null, musicSlider, muteToggle);
            muteToggle.setNeighbors(null, null, sfxSlider, exitButton);
            exitButton.setNeighbors(null, null, muteToggle, null);
            this.selectedComponent = musicSlider;
            menuCursor.snapToCurrentComponent();
        }

        public override bool snapCompatible()
        {
            return true;
        }

        public override void Update()
        {
            if (GameInput.GameCursorMenu.SimulateMousePress(exitButton))
            {
                this.exitButtonClick();
                return;
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
            if (musicSlider.gameObject == EventSystem.current.currentSelectedGameObject)
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

            if (sfxSlider.gameObject == EventSystem.current.currentSelectedGameObject)
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

        public void exitButtonClick()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Debug.Log("HELLO?");
                Menu.Instantiate<MainMenu>(true);
            }
            else
            {
                Debug.Log("NANI???");
                Game.Menu = null;
                base.exitMenu();
            }
        }

        /// <summary>
        /// What happens when the exit button is clicked.
        /// </summary>
        public override void exitMenu()
        {
            base.exitMenu();
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
