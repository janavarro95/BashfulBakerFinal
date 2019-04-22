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
        public AudioSource stirringSource;
        private Vector2 Prev, Next;
        private float Percent_Stirred;
        public int Count;
        public Sprite Choc;
        public Sprite Mint;
        public Sprite Raisin;
        public Sprite Pecan;
        public Sprite completeIcon;
        public GameObject[] buttons;
        public SpriteRenderer bowl;
        public Sprite[] bowlsprites;
        public Animator[]foodAnimation;


        public GameObject progressBar;

        // Start is called before the first frame update
        void Start()
        {         
            stirringSource.clip = chime;
            Prev = new Vector2(0, 0);
            Next = new Vector2(0, 0);
            Percent_Stirred = 0;
            Count = 0;

            buttons[0].SetActive(true);
            buttons[1].SetActive(false);

            progressBar.transform.localScale = new Vector3(.1f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

            Game.HUD.showHUD = false;
            Game.HUD.showOnlyTimer();

            if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().heldObject.name == "Chocolate Chip Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Choc;
            } else if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().heldObject.name == "Mint Chip Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Mint;
            }
            else if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().heldObject.name == "Oatmeal Raisin Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Raisin;
            }
            else if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().heldObject.name == "Pecan Crescent Cookies")
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Pecan;
            }
            else
            {
                GameObject.Find("chocChip_Bag").GetComponent<SpriteRenderer>().sprite = Choc;
            }

        }

        // Update is called once per frame
        void Update()
        {
            Next = new Vector2(InputControls.RightJoystickHorizontal, InputControls.RightJoystickVertical);
            
            if (!Prev.Equals(new Vector2(0, 0)) && !Next.Equals(new Vector2(0, 0)))
            {
                Percent_Stirred += Vector2.Angle(Prev, Next) > 45 ? 45 : Vector2.Angle(Prev, Next);
            }
            Prev = Next;

            if (Percent_Stirred >= 720)
            {
                Percent_Stirred = 720;
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
                if (InputControls.RightBumperPressed)
                {
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(false);
                    Count++;
                    Percent_Stirred = 0;
                    stirringSource.Play();
                    if (Count <= foodAnimation.Length)
                    {
                        foodAnimation[Count - 1].SetBool("enterBowl",true);
                        
                    }
                    else if (Count > foodAnimation.Length)
                    {
                        Invoke("getOutOfStirring", 1.5f);
                       
                    }

                }
            }

            int angle = (int)Vector2.SignedAngle(new Vector2(1, 0), Next);

            if (Mathf.Abs(angle) > 144)
            {
                angle = 2;
            }
            else if (Mathf.Abs(angle) > 72)
            {
                angle = angle > 0 ? 3 : 1;
            }
            else
            {
                angle = angle > 0 ? 4 : 0;
            }

            bowl.sprite = bowlsprites[angle];

            progressBar.transform.localScale = new Vector3((Percent_Stirred * 20)/720f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        }
        void getOutOfStirring()
        {
            actuallyTransition();
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
            SceneManager.LoadScene("Kitchen");
            ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
        }
    }
}
