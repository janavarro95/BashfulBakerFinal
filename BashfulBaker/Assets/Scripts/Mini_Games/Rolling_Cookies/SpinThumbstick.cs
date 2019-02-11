using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInformation;

namespace Assets.Scripts.GameInput
{
    public class SpinThumbstick : MonoBehaviour
    {
        private Vector2 startR, endR, startL, endL;
        private float sumR, sumL;
        private int count;
        public GameObject[] cookies;
        // Start is called before the first frame update
        void Start()
        {
            startR = new Vector2(0, 0);
            startL = new Vector2(0, 0);
            endR = new Vector2(0, 0);
            endL = new Vector2(0, 0);
            sumR = 0;
            sumL = 0;
            count = 0;
           
            for (int x = 0; x < 9; x++)
            {
                cookies[x].SetActive(false);
            }

            this.GetComponent<SpriteRenderer>().enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (this.GetComponent<SpriteRenderer>().enabled)
            {
                endR = new Vector2(InputControls.RightJoystickHorizontal, InputControls.RightJoystickVertical);
                if (!startR.Equals(new Vector2(0, 0)) && !endR.Equals(new Vector2(0, 0)))
                {
                    sumR += Vector2.Angle(startR, endR) > 45 ? 45 : Vector2.Angle(startR, endR);
                }
                startR = endR;

                endL = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if (!startL.Equals(new Vector2(0, 0)) && !endL.Equals(new Vector2(0, 0)))
                {
                    sumL += Vector2.Angle(startL, endL) > 45 ? 45 : Vector2.Angle(startL, endL);
                }
                startL = endL;

                if (count >= 9 && InputControls.APressed)
                {
                    Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
                    SceneManager.LoadScene("Kitchen");
                }

                if(sumR < 720 || sumL < 720)
                {
                    this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Mathf.Lerp(0f, 540, ((sumR < 720 ? sumR : 720) + (sumL < 720 ? sumL : 720))/1440f), this.transform.rotation.w);
                }
                else if (InputControls.APressed)
                {
                    cookies[count++].SetActive(true);
                    sumR = 0;
                    sumL = 0;
                    if (count == 8)
                    {
                        this.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            else if (InputControls.APressed)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
