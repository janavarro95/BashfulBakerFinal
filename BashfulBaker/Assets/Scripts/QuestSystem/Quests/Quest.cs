using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.QuestSystem.Quests
{
    /// <summary>
    /// The core class for all quests.
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// Variable to check if the quest has been completed.
        /// </summary>
        protected bool isCompleted;
        
        /// <summary>
        /// Checks if the quest is completed. Also can set the quest to be complete, but not the other way!
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
            set
            {
                if (this.isCompleted == true) return; //If the quest has already been completed we don't want to set it to false for some reason.
                this.isCompleted = value; //Set the value to true since it has to be false anyways.
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Quest()
        {
            this.isCompleted = false;
        }

        /// <summary>
        /// Gets a copy of this quest.
        /// </summary>
        /// <returns></returns>
        public virtual Quest Clone()
        {
            return new Quest();
        }

        /// <summary>
        /// Checks to see if a quest has been completed.
        /// </summary>
        public virtual void checkForCompletion()
        {
            //Implement this in all derived classes.
        }

        /// <summary>
        /// Checks to see if the special mission for this quest has been completed.
        /// </summary>
        /// <returns></returns>
        public virtual bool specialMissionCompleted()
        {
            return IsCompleted;
        }

    }
}
