using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;
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
    public Sprite[] cookiesprites;
    public GameObject[] cookieObj;
    public AudioClip chime;
    public AudioClip finishChime;
    public AudioSource moverSource;



    private void Start()
    {
        Game.HUD.showOnlyTimer();
        setCookies();
        moverSource.clip = chime;
        moverSource.pitch = 0.7f;
        Count = 0;
        whichButton = new int[cookies.Length];
        for (int x = 0; x < cookies.Length; x++)
        {
            whichButton[x] = (int)Mathf.Floor(Random.Range(0, 4));
            if (x > 0)
            {
               while (whichButton[x] == whichButton[x-1])
                {
                    whichButton[x] = (int)Mathf.Floor(Random.Range(0, 4));
                }
            }
        }
    }

    private void Update()
    {
        bool playSound = false;
        if (Count < cookies.Length)
        {
            buttonPrompt.sprite = XYBA[whichButton[Count]];

            if (InputControls.XPressed && whichButton[Count] == 0)
            {
                playSound = true;
                moverSource.pitch += 0.05f;
                cookies[Count].SetBool("moveToBasket", true);
                Count++;
                buttonPrompt.sprite = XYBA[whichButton[Count]];
            }

            if (InputControls.YPressed && whichButton[Count] == 1)
            {
                playSound = true;
                moverSource.pitch += 0.05f;
                cookies[Count].SetBool("moveToBasket", true);
                Count++;
                buttonPrompt.sprite = XYBA[whichButton[Count]];
            }

            if (InputControls.BPressed && whichButton[Count] == 2)
            {
                playSound = true;
                moverSource.pitch += 0.05f;
                cookies[Count].SetBool("moveToBasket", true);
                Count++;
                buttonPrompt.sprite = XYBA[whichButton[Count]];
            }

            if (InputControls.APressed && whichButton[Count] == 3)
            {
                playSound = true;
                moverSource.pitch += 0.05f;
                cookies[Count].SetBool("moveToBasket", true);
                Count++;
                buttonPrompt.sprite = XYBA[whichButton[Count]];
            }

        }
        else if (buttonPrompt.enabled)
        {
            moverSource.pitch = 1;
            moverSource.volume = 1.5f;
            moverSource.clip = finishChime;
            playSound = true;
            buttonPrompt.enabled = false;
        }
        else
        {
            Invoke("exitcookieMover", 1f);
        }

        if (playSound)
        {
            moverSource.Play();
        }
    }


    private void exitcookieMover()
    {
        actuallyTransition();
        
    }

    private void actuallyTransition()
    {
        //GameObject.Find("MinigameTimer").GetComponent<MinigameTimer>().finishGame(Enums.CookingStationMinigame.PackingStation);
        ScreenTransitions.StartSceneTransition(.5f, "Kitchen", Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
    }
    private void finishedTransition()
    {
        Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
        Game.HUD.showHUD = true;
        Game.HUD.showAll();
        Game.LoadCorrectKitchenScene();

        //Debug.Log("Arrow shows the way!");
        if (Game.CurrentDayNumber == 1 || Game.CurrentDayNumber == 0)
        {
            Game.Player.arrowDirection.gameObject.SetActive(true);
        }
        else
        {
            Game.Player.arrowDirection.gameObject.SetActive(false);
        }
        ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
    }
    private void setCookies()
    {
        if (Game.Player.activeItem.Name == "Mint Chip Cookies")
        {
            for (int i = 0; i < 6; i++)
            {
                cookieObj[i].GetComponent<SpriteRenderer>().sprite = cookiesprites[0];
            }
        }
        else if (Game.Player.activeItem.Name == "Oatmeal Raisin Cookies")
        {
            for (int i = 0; i < 6; i++)
            {
                cookieObj[i].GetComponent<SpriteRenderer>().sprite = cookiesprites[1];
            }
        }
        else if (Game.Player.activeItem.Name == "Pecan Crescent Cookies")
        {
            for (int i = 0; i < 6; i++)
            {
                cookieObj[i].GetComponent<SpriteRenderer>().sprite = cookiesprites[2];
            }
        }
        else
        {
            Debug.Log("default");
        }
    }
}

