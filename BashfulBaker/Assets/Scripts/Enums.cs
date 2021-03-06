﻿using System;
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
        /// Deals with all of the states a timer could be in.
        /// </summary>
        public enum TimerState
        {
            /// <summary>
            /// The timer has been initialized but is not ticking.
            /// </summary>
            Initialized,
            /// <summary>
            /// The timer has started ticking.
            /// </summary>
            Ticking,
            /// <summary>
            /// The timer has finished.
            /// </summary>
            Finished,
            /// <summary>
            /// The timer has stopped.
            /// </summary>
            Stopped,
            /// <summary>
            /// The timer has paused.
            /// </summary>
            Paused
        }

        public enum TimerType
        {
            CountDown,
            CountUp
        }

        /// <summary>
        /// Deals with visibility on certain objects.
        /// </summary>
        public enum Visibility
        {
            Visible,
            Invisible
        }

        /// <summary>
        /// Deals with tracking the state of any given dish.
        /// </summary>
        public enum DishState
        {
            Ingredients,
            Mixed,
            Prepped,
            Baked,
            Packaged,
            Burnt
        }

        public enum InventoryViewMode
        {
            DishView,
            SpecialIngredientView
        }

        /// <summary>
        /// A list of all of the dishes we can make in our game.
        /// </summary>
        public enum Dishes
        {
            ChocolateChipCookies,
            MintChipCookies,
            PecanCookies,
            OatmealRaisinCookies
        }

        /// <summary>
        /// A list of all of the special ingredients in the game.
        /// </summary>
        public enum SpecialIngredients
        {
            ChocolateChips,
            MintChips,
            Pecans,
            Raisins,
            Carrots,
            Strawberries
        }

        /// <summary>
        /// The different minigame sessions that exist.
        /// </summary>
        public enum CookingStationMinigame
        {
            MixingBowl,
            RollingStation,
            Oven,
            PackingStation,
            TrashCan
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
