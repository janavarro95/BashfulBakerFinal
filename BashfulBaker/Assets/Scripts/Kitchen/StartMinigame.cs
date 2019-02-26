using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInput;
using Assets.Scripts;

public class StartMinigame : MonoBehaviour
{
    public string minigame;
    public GameObject arrow;
    public int thisStep;
    /// <summary>
    /// Used to determine if the player should be invisible in the minigame.
    /// </summary>
    public bool makePlayerInvisible;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>().currentStep == thisStep)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = false;
            arrow.GetComponent<progress>().A.SetActive(true);

            if (InputControls.APressed && collision.GetComponent<PlayerMovement>().currentStep == thisStep)
            {
                arrow.GetComponent<SpriteRenderer>().enabled = true;
                arrow.GetComponent<progress>().A.SetActive(false);
                collision.GetComponent<PlayerMovement>().NextStep();
                if (makePlayerInvisible) Assets.Scripts.GameInformation.Game.Player.setSpriteVisibility(Enums.Visibility.Invisible);
                SceneManager.LoadScene(minigame);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        arrow.GetComponent<SpriteRenderer>().enabled = true;
        arrow.GetComponent<progress>().A.SetActive(false);
    }
}
