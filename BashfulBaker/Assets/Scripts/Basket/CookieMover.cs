using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookieMover : MonoBehaviour
{
    public GameObject[] Cookies;
    private int c;
    // Start is called before the first frame update
    void Start()
    {
        c = 0;
        for(int x = 0; x < 16; x++)
        {
            Cookies[x].SetActive(x < 8 ? true : false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((c < 4 ? KeyCode.Space : KeyCode.Tab)))
        {
            if (c < 8)
            {
                Cookies[c++].SetActive(false);
                Cookies[c + 7].SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("Kitchen");
            }
        }
    }
}
