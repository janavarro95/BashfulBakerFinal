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
                    SceneManager.LoadScene(sceneToWarpTo);
                    collision.gameObject.transform.position = warpLocation;
                }
            }
            else
            {
                SceneManager.LoadScene(sceneToWarpTo);
                collision.gameObject.transform.position = warpLocation;
            }
        }
    }
}
