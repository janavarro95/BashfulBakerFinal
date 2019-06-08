using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utilities.Delegates;
using Assets.Scripts.Utilities;

public class exitscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputControls.StartPressed)
        {
            ScreenTransitions.StartSceneTransition(2, "MainMenu", Color.black, ScreenTransitions.TransitionState.FadeOut);
        }
    }
}
