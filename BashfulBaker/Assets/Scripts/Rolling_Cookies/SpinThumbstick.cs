using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameInput
{
    public class SpinThumbstick : MonoBehaviour
    {
        private Vector2 startR, endR, startL, endL;
        private float sumR, sumL;
        private int count;
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
        }

        // Update is called once per frame
        void Update()
        {
            endR = new Vector2(GameInput.InputControls.RightJoystickHorizontal, GameInput.InputControls.RightJoystickVertical);
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

            if(sumR > 720 && sumL > 720 && InputControls.APressed)
            {
                Debug.Log(++count);
                sumR = 0;
                sumL = 0;
            }
        }
    }
}
