using Assets.Scripts;
using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Scripts.GameInput
{
    public class StirringGameV2 : MonoBehaviour
    {
        private Vector2 Prev, Next;
        private float Percent_Stirred;
        public int Count; 
        public Sprite completeIcon;
        //public Animator flyingFood;
        public GameObject[] buttons;
        public SpriteRenderer bowl;
        public Sprite[] bowlsprites;
        public Animator[]foodAnimation;
       // public SpriteRenderer butterSource;
       // public SpriteRenderer eggSource;
       // public Sprite removedEgg;


        public GameObject progressBar;

        // Start is called before the first frame update
        void Start()
        {
            Prev = new Vector2(0, 0);
            Next = new Vector2(0, 0);
            Percent_Stirred = 0;
            Count = 0;

            buttons[0].SetActive(true);
            buttons[1].SetActive(false);

            progressBar.transform.localScale = new Vector3(.1f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

           // Game.HUD.showHUD = false;
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

            if (Percent_Stirred >= 720 || Count >= foodAnimation.Length)
            {
                Percent_Stirred = 720;
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
                if (InputControls.APressed)
                {
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(false);
                    Count++;
                    Percent_Stirred = 0;
                    //Debug.Log(Count);
                    if (Count < foodAnimation.Length)
                    {
                        foodAnimation[Count - 1].SetBool("enterBowl",true);
                    }
                    else if (Count <= foodAnimation.Length)
                    {
                        Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
                        Game.HUD.showHUD = true;
                        SceneManager.LoadScene("Kitchen");
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

            progressBar.transform.localScale = new Vector3((Percent_Stirred * 30)/720f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        }
    }
}
