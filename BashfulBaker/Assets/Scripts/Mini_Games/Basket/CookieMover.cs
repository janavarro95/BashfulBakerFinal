using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookieMover : MonoBehaviour
{
    public GameObject[] Cookies;
    public GameObject[] buttons;
    private int c;
    // Start is called before the first frame update
    void Start()
    {
        c = 0;
        for(int x = 0; x < 16; x++)
        {
            Cookies[x].SetActive(x < 8 ? true : false);
        }

        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((c < 4 ? KeyCode.Space : KeyCode.Tab)) || (c < 4 ?  InputControls.XPressed : InputControls.YPressed))
        {
            if (c == 3)
            {
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
            }
            if (c < 8)
            {
                Cookies[c++].SetActive(false);
                Cookies[c + 7].SetActive(true);
            }
            else
            {
                Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
                SceneManager.LoadScene("Kitchen");
            }
        }
    }
}
