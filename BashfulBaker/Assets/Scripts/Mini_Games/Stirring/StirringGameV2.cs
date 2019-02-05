﻿using Assets.Scripts;
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

        // Start is called before the first frame update
        void Start()
        {
            Prev = new Vector2(0, 0);
            Next = new Vector2(0, 0);
            Percent_Stirred = 0;
            Count = 0;


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

            if (Percent_Stirred >= 720 && InputControls.APressed)
            {
                Count++;
                Debug.Log(Count);
                 if (Count == 1){
                    GameObject Checkmark1 = new GameObject();
                    Checkmark1.AddComponent<SpriteRenderer>();
                    Checkmark1.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark1.transform.position = new Vector3(-4.45f, 1.2f, 0);
                    Checkmark1.layer = 1;
                }
                else if(Count ==2){
                    GameObject Checkmark2 = new GameObject();
                    Checkmark2.AddComponent<SpriteRenderer>();
                    Checkmark2.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark2.transform.position = new Vector3(-4.45f, .25f, 0);
                    Checkmark2.layer = 1;
                }
                else if( Count == 3){
                    GameObject Checkmark3 = new GameObject();
                    Checkmark3.AddComponent<SpriteRenderer>();
                    Checkmark3.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark3.transform.position = new Vector3(-4.45f, -.85f, 0);
                    Checkmark3.layer = 1;
                }
                else if (Count == 4){
                    GameObject Checkmark4 = new GameObject();
                    Checkmark4.AddComponent<SpriteRenderer>();
                    Checkmark4.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark4.transform.position = new Vector3(-4.45f, -1.9f, 0);
                    Checkmark4.layer = 1;
                } else if(Count == 5)
                {
                    Game.Player.setVisibility(Enums.Visibility.Visible);
                    SceneManager.LoadScene("Kitchen");
                }

            }
        }


    }
}