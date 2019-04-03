using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookieMover : MonoBehaviour
{
        public SpriteRenderer buttonPrompt;
        public Sprite[] XYBA;
        public Animator[] cookies;
        private int Count;
        private int[] whichButton;
    public AudioClip chime;
    public AudioSource moverSource;



    private void Start()
    {
        moverSource.clip = chime;
        Count = 0;
        whichButton = new int[cookies.Length];
        for(int x = 0; x < cookies.Length; x++)
        {
            whichButton[x] = (int)Mathf.Floor(Random.Range(0, 4));
            Debug.Log(whichButton[x]);
        }

    }

    private void Update()
    {

        if (Count < cookies.Length)
        {
            buttonPrompt.sprite = XYBA[whichButton[Count]];
        }
        

        if (InputControls.XPressed && whichButton[Count] == 0)
        {
            moverSource.Play();
            cookies[Count].SetBool("moveToBasket", true);
            Count++;
            buttonPrompt.sprite = XYBA[whichButton[Count]];
        }

        if (InputControls.YPressed && whichButton[Count] == 1)
        {
            moverSource.Play();
            cookies[Count].SetBool("moveToBasket", true);
            Count++;
            buttonPrompt.sprite = XYBA[whichButton[Count]];
        }

        if (InputControls.BPressed && whichButton[Count] == 2)
        {
            moverSource.Play();
            cookies[Count].SetBool("moveToBasket", true);
            Count++;
            buttonPrompt.sprite = XYBA[whichButton[Count]];
        }

        if (InputControls.APressed && whichButton[Count] == 3)
        {
            moverSource.Play();
            cookies[Count].SetBool("moveToBasket", true);
            Count++;
            buttonPrompt.sprite = XYBA[whichButton[Count]];
        }


        if (Count >= cookies.Length)
        {
            Invoke("exitcookieMover", 1.5f);
        }
    }
    private void exitcookieMover()
    {
        Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
        Game.HUD.showHUD = true;
        SceneManager.LoadScene("Kitchen");
    }

}








































    /* public GameObject[] Cookies;
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
        Game.HUD.showHUD = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (c <= 3)
        {
            buttons[0].SetActive(true);
        }
        else 
        { 
            buttons[1].SetActive(true);
        }
        if (Input.GetKeyDown((c < 4 ? KeyCode.Space : KeyCode.Tab)) || (c < 4 ?  InputControls.XPressed : InputControls.YPressed))
        {
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            if (c < 8)
            {
                Cookies[c++].SetActive(false);
                Cookies[c + 7].SetActive(true);
            }
            else
            {
                Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
                Game.HUD.showHUD = true;
                SceneManager.LoadScene("Kitchen");
            }
        }
    }*/

