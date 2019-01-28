using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pantry : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D coll)
    {

        if(Assets.Scripts.GameInput.InputControls.APressed && coll.tag == "Player")
        {
          Debug.Log("aaaaahhhh");
            //open menu
            //load next scene

        }

    }
}
