using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInput;
using Assets.Scripts;

public class StartMinigame : MonoBehaviour
{
    public string minigame;
    public int thisStep;
    /// <summary>
    /// Used to determine if the player should be invisible in the minigame.
    /// </summary>
    public bool makePlayerInvisible;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputControls.APressed && collision.GetComponent<PlayerMovement>().currentStep == thisStep)
        {
            collision.GetComponent<PlayerMovement>().NextStep();
            if (makePlayerInvisible) Assets.Scripts.GameInformation.Game.Player.setSpriteVisibility(Enums.Visibility.Invisible);
            SceneManager.LoadScene(minigame);
        }
    }
}
