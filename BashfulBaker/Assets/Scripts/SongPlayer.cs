using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    public AudioClip songToPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (Game.SoundManager != null)
        {
            Game.SoundManager.playSong(songToPlay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
