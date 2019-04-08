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
      

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (bowl.transform.rotation.z >= -160)
            {
                bowl.transform.Rotate(new Vector3(0, 0, 1), (float)(-InputControls.RightTrigger * 3.6), Space.World);
            }
               //bowl.transform.Rotate(new Vector3(0, 0, 1), (float)(InputControls.LeftTrigger * 3.6), Space.World);
            

            if (bowl.transform.rotation.z < -12)
            {
                bowl.transform.Rotate(new Vector3(0, 0, 1), 1.8f, Space.World);
            }
        }
    }

}
