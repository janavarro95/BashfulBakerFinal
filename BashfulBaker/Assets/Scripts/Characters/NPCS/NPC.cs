using Assets.Scripts.QuestSystem.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Characters.NPCS
{
    public class NPC:Character
    {
        public NPCPreferences preferences;
        public CookingQuest generateCookingRequest()
        {
            return this.preferences.generateCookingQuest(this.Name);
        }
    }
}
