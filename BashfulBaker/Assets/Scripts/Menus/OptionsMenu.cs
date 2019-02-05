using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
            exitButton = canvas.transform.Find("QuitButton").gameObject.GetComponent<Button>();

            sfxSlider = canvas.transform.Find("SFXSlider").gameObject.GetComponent<Slider>();
            musicSlider = canvas.transform.Find("MusicSlider").gameObject.GetComponent<Slider>();

            muteToggle = canvas.transform.Find("MuteToggle").gameObject.GetComponent<Toggle>();

            sfxSlider.value = Game.Options.sfxVolume;
            musicSlider.value = Game.Options.musicVolume;
            muteToggle.isOn = Game.Options.muteVolume;

        }

        public override void Update()
        {
            if (GameInput.GameCursor.SimulateMousePress(exitButton))
            {
                this.exitMenu();
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
