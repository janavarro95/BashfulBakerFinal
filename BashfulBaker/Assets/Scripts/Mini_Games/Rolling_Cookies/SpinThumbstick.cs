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
        private int spinning;
        public GameObject[] cookies;
        public GameObject[] buttons;
        private SpriteRenderer sprite;
        public Sprite[] sprites;

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

            sprite = this.GetComponent<SpriteRenderer>();
            sprite.enabled = true;
            sprite.sprite = sprites[0];

            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
            buttons[2].SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (this.GetComponent<SpriteRenderer>().enabled)
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

                sprite.sprite = sumR + sumL < 720 ? sumR + sumL > 360 ? sprites[1] : sprites[0] : sumR + sumL > 1080 ? sprites[3] : sprites[2];

                if (sumR < 720f || sumL < 720f)
                {
                    this.transform.Rotate(0f, 0f, 90f * Time.deltaTime * spinning);
                    //this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Mathf.Lerp(0f, 720f, ((sumR < 720f ? sumR : 720f) + (sumL < 720f ? sumL : 720f)) / 1440f), this.transform.rotation.w);
                }
                else
                {
                    buttons[0].SetActive(false);
                    buttons[1].SetActive(false);
                    buttons[2].SetActive(true);
                    if (InputControls.APressed)
                    {
                        buttons[2].SetActive(false);
                        cookies[count++].SetActive(true);
                        if (count >= 8)
                        {
                            cookies[8].SetActive(true);
                        }
                        sumR = 0;
                        sumL = 0;
                        this.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            else if (InputControls.APressed)
            {
                if (count >= 8 && InputControls.APressed)
                {
                    Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
                    SceneManager.LoadScene("Kitchen");
                }
                else
                {
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(true);
                    buttons[2].SetActive(false);
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    sprite.sprite = sprites[0];
                }
            }
            else
            {
                buttons[2].SetActive(true);
            }
        }
    }
}
