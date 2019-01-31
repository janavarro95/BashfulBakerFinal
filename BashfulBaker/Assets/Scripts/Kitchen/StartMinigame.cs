using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInput;

public class StartMinigame : MonoBehaviour
{
    public string minigame;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputControls.APressed)
        {
            SceneManager.LoadScene(minigame);
        }
    }
}
