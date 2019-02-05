using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameInformation
{
    /// <summary>
    /// A class that deals with all of the options in the game.
    /// </summary>
    public class GameOptions
    {

        /// <summary>
        /// The volume for sound effects.
        /// </summary>
        public float sfxVolume;
        /// <summary>
        /// The volume for music.
        /// </summary>
        public float musicVolume;

        /// <summary>
        /// If the game's audio is muted.
        /// </summary>
        public bool muteVolume;

        public GameOptions()
        {
            sfxVolume = 1.00f;
            musicVolume = 1.00f;
            muteVolume = false;
        }

    }
}
