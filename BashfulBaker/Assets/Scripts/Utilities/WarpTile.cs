using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpTile : MonoBehaviour
{

    [SerializeField]
    private bool pressAToWarp;
    [SerializeField]
    private string sceneToWarpTo;
    [SerializeField]
    private Vector2 warpLocation;

    [SerializeField]
    private float transitionTime = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION");
        if (collision.gameObject.tag == "Player")
        {
            if (pressAToWarp)
            {
                if (Assets.Scripts.GameInput.InputControls.APressed)
                {
                    ScreenTransitions.StartSceneTransition(transitionTime, sceneToWarpTo, Color.black, ScreenTransitions.TransitionState.FadeOut,new VoidDelegate(finishedTransition));
                }
            }
            else
            {
                ScreenTransitions.StartSceneTransition(transitionTime, sceneToWarpTo, Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
            }
        }
    }

    private void finishedTransition()
    {
        SceneManager.LoadScene(sceneToWarpTo);
        Game.Player.gameObject.transform.position = warpLocation;
        //ScreenTransitions.StartSceneTransition(transitionTime, "", Color.black, ScreenTransitions.TransitionState.FadeIn);
    }
}
