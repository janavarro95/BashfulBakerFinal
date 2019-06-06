using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Scripts.GameInput
{
    public class StirringGameV2 : MonoBehaviour
    {
        public AudioClip chime;
        public AudioClip finishChime;
        public AudioSource stirringSource;
        private Vector2 Prev, Next;
        private float Percent_Stirred;
        public int Count;
        public GameObject HeldObject;
        public Material[] particles;
        public Sprite Choc;
        public Sprite Mint;
        public Sprite Raisin;
        public Sprite Pecan;
        public Sprite completeIcon;
        public GameObject[] buttons;
        public SpriteRenderer bowl;
        public Sprite[] bowlsprites;
        public Sprite[] mintsprites;
        public Sprite[] raisinprites;
        public Sprite[] pecansprites;
        public Animator[]foodAnimation;


        public GameObject progressBar, barFill;
        public Sprite[] progressBarSprites, barFillSprites;

        // Start is called before the first frame update
        void Start()
        {
            
            stirringSource.clip = chime;
            stirringSource.pitch = 0.8f;
            Prev = new Vector2(0, 0);
            Next = new Vector2(0, 0);
            Percent_Stirred = 0;
            Count = 0;

            buttons[0].SetActive(true);
            buttons[1].SetActive(false);

            progressBar.transform.position = new Vector3(progressBar.GetComponent<StartEnd>().start, progressBar.transform.position.y, progressBar.transform.position.z);

            Game.HUD.showOnlyTimer();

            Debug.Log(Game.Player.activeItem.Name);
            if (Game.Player.activeItem.Name == "Chocolate Chip Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Choc;
                GameObject.Find("chocChip_Bag").GetComponent<ParticleSystemRenderer>().material = particles[0];
                progressBar.GetComponent<SpriteRenderer>().sprite = progressBarSprites[0];
                barFill.GetComponent<SpriteRenderer>().sprite = barFillSprites[0];

            } else if (Game.Player.activeItem.Name == "Mint Chip Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Mint;
                GameObject.Find("chocChip_Bag").GetComponent<ParticleSystemRenderer>().material = particles[1];
                bowlsprites = mintsprites;
                progressBar.GetComponent<SpriteRenderer>().sprite = progressBarSprites[1];
                barFill.GetComponent<SpriteRenderer>().sprite = barFillSprites[1];
            }
            else if (Game.Player.activeItem.Name == "Oatmeal Raisin Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Raisin;
                GameObject.Find("chocChip_Bag").GetComponent<ParticleSystemRenderer>().material = particles[2];
                bowlsprites = raisinprites;
                progressBar.GetComponent<SpriteRenderer>().sprite = progressBarSprites[2];
                barFill.GetComponent<SpriteRenderer>().sprite = barFillSprites[2];
            }
            else if (Game.Player.activeItem.Name == "Pecan Crescent Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Pecan;
                GameObject.Find("chocChip_Bag").GetComponent<ParticleSystemRenderer>().material = particles[3];
                bowlsprites = pecansprites;
                progressBar.GetComponent<SpriteRenderer>().sprite = progressBarSprites[3];
                barFill.GetComponent<SpriteRenderer>().sprite = barFillSprites[3];
            }
            else
            {
                Debug.Log("default");
                GameObject.Find("chocChip_Bag").GetComponent<ParticleSystemRenderer>().material = particles[0];
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Choc;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Count <= foodAnimation.Length)
            {
                Next = new Vector2(InputControls.RightJoystickHorizontal, InputControls.RightJoystickVertical);

                if (!Prev.Equals(new Vector2(0, 0)) && !Next.Equals(new Vector2(0, 0)))
                {
                    Percent_Stirred += Vector2.Angle(Prev, Next) > 45 ? 45 : Vector2.Angle(Prev, Next);
                }
                Prev = Next;
            }

            if (Percent_Stirred >= 720)
            {
                Percent_Stirred = 720;
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
                if (InputControls.RightBumperPressed && Count < foodAnimation.Length+1)
                {
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(false);
                    Count++;

                    if (Count <= foodAnimation.Length)
                    {
                        foodAnimation[Count - 1].SetBool("enterBowl",true);
                        Percent_Stirred = 0;

                    }
                    else if (Count >= foodAnimation.Length)
                    {
                        stirringSource.clip = finishChime;
                        stirringSource.pitch = 1.0f;
                        Invoke("getOutOfStirring", 1.5f);
                    }

                    stirringSource.Play();
                    stirringSource.pitch += 0.05f;
                }
            }

            int angle = (int)Vector2.SignedAngle(new Vector2(1, 0), Next);
            if (Next.magnitude < (new Vector2(.1f, .1f)).magnitude)
            {
                angle = 7;
            }
            else
            {
                angle = ((angle / 25) + 7) % 14;
            }

            bowl.sprite = bowlsprites[angle];

            progressBar.transform.position = new Vector3(Mathf.Lerp(progressBar.GetComponent<StartEnd>().start, progressBar.GetComponent<StartEnd>().end, Percent_Stirred/720f), progressBar.transform.position.y, progressBar.transform.position.z);
        }
        void getOutOfStirring()
        {
            actuallyTransition();
        }
        private void actuallyTransition()
        {
            //GameObject.Find("MinigameTimer").GetComponent<MinigameTimer>().finishGame(Enums.CookingStationMinigame.MixingBowl);

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
    }
}
