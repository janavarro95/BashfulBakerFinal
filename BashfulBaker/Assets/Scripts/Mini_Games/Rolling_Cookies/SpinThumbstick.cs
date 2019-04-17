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
        public GameObject progressBar;

        public AudioClip chime;
        public AudioSource spinningSource;

        // Start is called before the first frame update
        void Start()
        {
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

            Game.HUD.showHUD = false;
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
                if (count >= 6 && InputControls.APressed)
                {
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
            Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
            Game.HUD.showHUD = true;
            SceneManager.LoadScene("Kitchen");
        }
    }

}
