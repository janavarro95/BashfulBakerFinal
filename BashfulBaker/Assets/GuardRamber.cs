using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInformation;

public class GuardRamber : MonoBehaviour
{
    public List<string> GuardRambles = new List<string>();
    public int specificRamble = 0;
    public int numTutorialDialogs = 0;
    public int numTutorialDialogsPassed = 0;

    public string RandomRamble()
    {
        return GuardRambles[(int)Random.Range(0, GuardRambles.Count-1)];
    }

    public string NextRamble()
    {
        string ret = GuardRambles[specificRamble];

        if (!Game.Player.PlayerMovement.hasBeenCaughtBefore)
        {
            if (numTutorialDialogs == numTutorialDialogsPassed)
            {
                Game.Player.PlayerMovement.hasBeenCaughtBefore = true;
            }
            else
            {
                GuardRambles.RemoveAt(0);
                numTutorialDialogsPassed++;
            }
        }
        else
        {
            specificRamble++;
            specificRamble = specificRamble % GuardRambles.Count;
        }

        return ret;
    }
}
