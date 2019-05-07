using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;

namespace Assets.Scripts.GameInput
{
    public class SpinThumbstick : MonoBehaviour
    {
        private Vector2 startR, endR, startL, endL;
        private float sumR, sumL;
        private int count;
        private int spinning;
        public GameObject[] cookies;
        public GameObject[] buttons;
        private SpriteRenderer sprite;
        public Sprite[] sprites;
        public Sprite[] mint;
        public Sprite[] raisin;
        public Sprite[] pecan;
        public Sprite[] flavorcookies;
        public GameObject progressBar;
        public GameObject Bowl;
        public Sprite[] bowlsprites;

        public AudioClip chime;
        public AudioSource spinningSource;

        // Start is called before the first frame update
        void Start()
        {
            setcookies();
            setDough();
            spinningSource.clip = chime;
            startR = new Vector2(0, 0);
            startL = new Vector2(0, 0);
            endR = new Vector2(0, 0);
            endL = new Vector2(0, 0);
            sumR = 0;
            sumL = 0;
            count = 0;
           
            for (int x = 0; x < 7; x++)
            {
                cookies[x].SetActive(false);
            }

            sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
            sprite.sprite = sprites[0];

            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
            buttons[2].SetActive(false);
            buttons[3].SetActive(false);

            progressBar.transform.localScale = new Vector3(.1f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

            
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<SpriteRenderer>().enabled)
            {
                spinning = 0;
                endR = new Vector2(InputControls.RightJoystickHorizontal, InputControls.RightJoystickVertical);
                if (!startR.Equals(new Vector2(0, 0)) && !endR.Equals(new Vector2(0, 0)))
                {
                    sumR += Vector2.Angle(startR, endR) > 45f ? 45f : Vector2.Angle(startR, endR);
                    spinning++;
                }
                startR = endR;

                endL = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if (!startL.Equals(new Vector2(0, 0)) && !endL.Equals(new Vector2(0, 0)))
                {
                    sumL += Vector2.Angle(startL, endL) > 45f ? 45f : Vector2.Angle(startL, endL);
                    spinning++;
                }
                startL = endL;

                sumR = sumR > 720 ? 720 : sumR;
                sumL = sumL > 720 ? 720 : sumL;

                sprite.sprite = sumR + sumL < 720 ? sumR + sumL > 360 ? sprites[1] : sprites[0] : sumR + sumL > 1080 ? sprites[3] : sprites[2];

                if (sumR < 720f || sumL < 720f)
                {
                    progressBar.transform.localScale = new Vector3(((sumR + sumL) * 30) / 1440f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
                    transform.Rotate(0f, 0f, 90f * Time.deltaTime * spinning);
                    //this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Mathf.Lerp(0f, 720f, ((sumR < 720f ? sumR : 720f) + (sumL < 720f ? sumL : 720f)) / 1440f), this.transform.rotation.w);
                }
                else
                {
                    buttons[0].SetActive(false);
                    buttons[1].SetActive(false);
                    buttons[3].SetActive(true);
                    if (InputControls.RightTrigger > .99)
                    {
                        spinningSource.Play();
                        buttons[3].SetActive(false);
                        cookies[count++].SetActive(true);
                        Debug.Log(count);
                        if (count >= 6)
                        {
                            cookies[6].SetActive(true);
                        }
                        sumR = 0;
                        sumL = 0;
                        GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            else if (InputControls.LeftTrigger >.99)
            {
                if (count >= 6 )
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].SetActive(false);
                    }
                    Invoke("exitspinnning", 1.5f);
                }
                else
                {
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(true);
                    buttons[2].SetActive(false);
                    GetComponent<SpriteRenderer>().enabled = true;
                    sprite.sprite = sprites[0];
                    spinningSource.Play();
                }
            }
            else
            {
                buttons[2].SetActive(true);
            }
        }
        void exitspinnning()
        {
            actuallyTransition();
            //SceneManager.LoadScene("Kitchen");
        }

        private void actuallyTransition()
        {
            ScreenTransitions.StartSceneTransition(.5f, "Kitchen", Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
        }
        private void finishedTransition()
        {
            Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
            Game.HUD.showHUD = true;
            Game.HUD.showAll();
            Game.LoadCorrectKitchenScene();
            ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
        }

        private void setDough()
        {
             if (Game.Player.activeItem.Name == "Mint Chip Cookies")
            {
                sprites = mint;
            }
            else if (Game.Player.activeItem.Name == "Oatmeal Raisin Cookies")
            {
                sprites = raisin;
            }
            else if (Game.Player.activeItem.Name == "Pecan Crescent Cookies")
            {
                sprites = pecan;
            }
            else
            {
                Debug.Log("default");
            }
        }
        private void setcookies()
        {
            for (int i = 0; i < 6; i++)
            {
                if (Game.Player.activeItem.Name == "Mint Chip Cookies")
                {
                    cookies[i].GetComponent<SpriteRenderer>().sprite = flavorcookies[0];
                    Bowl.GetComponent<SpriteRenderer>().sprite = bowlsprites[0];
                }
                else if (Game.Player.activeItem.Name == "Oatmeal Raisin Cookies")
                {
                    cookies[i].GetComponent<SpriteRenderer>().sprite = flavorcookies[1];
                    Bowl.GetComponent<SpriteRenderer>().sprite = bowlsprites[1];
                }
                else if (Game.Player.activeItem.Name == "Pecan Crescent Cookies")
                {
                    cookies[i].GetComponent<SpriteRenderer>().sprite = flavorcookies[2];
                    Bowl.GetComponent<SpriteRenderer>().sprite = bowlsprites[2];
                }
                else
                {
                    Debug.Log("default");
                }
                
            }

        }
    }

}
