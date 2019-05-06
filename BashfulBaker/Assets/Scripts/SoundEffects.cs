using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip menuButtonSnapClick;
    public AudioClip mainMenu_StartButton;
    public AudioClip menuCloseSound;
    public AudioClip itemSelectSound;
    public AudioClip specialIngredientPickUp;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playMenuButtonMovementSnap()
    {
        Game.SoundManager.playSound(menuButtonSnapClick, 1f);
    }

    public void playMainMenuStartButton()
    {
        Game.SoundManager.playSound(mainMenu_StartButton,1f);
    }

    public void playMenuCloseSound()
    {
        Game.SoundManager.playSound(menuCloseSound, 1f);
    }

    public void playItemSelectSound()
    {
        Game.SoundManager.playSound(itemSelectSound, 1f);
    }

    /// <summary>
    /// Plays the special ingredient pickup sound
    /// </summary>
    public void playSIPickUpSound()
    {
        Game.SoundManager.playSound(specialIngredientPickUp, 1f);
    }
}
