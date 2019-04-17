using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInformation;

namespace Assets.Scripts.GameInput
{
    public class pouringWithController : MonoBehaviour
    {
        public GameObject bowl;
        public ParticleSystem lePour;
      

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (InputControls.APressed)
            {
                Debug.Log(bowl.transform.localEulerAngles.z);
            }
            ParticleSystem.ShapeModule pourshape = lePour.shape;
           // Transform.Rotation tiltsize = bowl.transform.eulerAngles;


            if (bowl.transform.localEulerAngles.z < 330) {
                lePour.emissionRate = (330/bowl.transform.localEulerAngles.z)*20;
                pourshape.arc = (330 / bowl.transform.localEulerAngles.z) * 35;
            }
           else{
                lePour.emissionRate = 0.1f;
                pourshape.arc = 10;
            }

            if (InputControls.LeftTrigger == 0 && bowl.transform.localEulerAngles.z >275)
            {
                if (bowl.transform.localEulerAngles.z < 360 && bowl.transform.localEulerAngles.z > 270)
                {
                    bowl.transform.Rotate(new Vector3(0, 0, 1), (float)(-InputControls.RightTrigger * 5.4f - .01f), Space.World);
                }
                //bowl.transform.Rotate(new Vector3(0, 0, 1), (float)(InputControls.LeftTrigger * 3.6), Space.World);

                if (bowl.transform.localEulerAngles.z < 350 && bowl.transform.localEulerAngles.z > 255)
                    bowl.transform.Rotate(new Vector3(0, 0, 1), 3.6f, Space.World);

            }
            else
            {
                if(InputControls.RightTrigger > .90f)
                {

                }
                else
                { if (InputControls.LeftTrigger == 0)
                        bowl.transform.Rotate(new Vector3(0, 0, 1), 3.6f, Space.World);
                }
            }
        }
    }

}
