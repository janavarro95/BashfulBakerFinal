using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Enums
    {
        /// <summary>
        /// The cooking station the player is interacting with.
        /// </summary>
        public enum CookingStation
        {
            MixingBowl,
            Oven
        }

        /// <summary>
        /// The operating system that the game is run on.
        /// </summary>
        public enum OperatingSystem
        {
            Windows,
            Mac,
            Linux,
            PS3,
            PS4,
            XBoxOne,
            NintendoSwitch,
            Android,
            IPhone,
            Other
        }

        /// <summary>
        /// The state that says where we are in the game.
        /// </summary>
        public enum GameState
        {
            MainMenu,
            GamePlay,
            Credits
        }

        public enum QuestCompletionStatus
        {
            NotCompleted,
            Completed,
            SpecialMissionCompleted
        }

        public enum FacingDirection
        {
            Up,
            Left,
            Down,
            Right
        }

        /// <summary>
        /// Gets all of the values stored in an enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

    }
}
