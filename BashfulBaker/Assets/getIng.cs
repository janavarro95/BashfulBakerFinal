using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameInput {
    public class getIng : MonoBehaviour
    {
        public GameObject arrow;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (InputControls.APressed && collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().currentStep == 0)
            {
                Debug.Log("Picked up ingredients");
                collision.GetComponent<PlayerMovement>().NextStep();
            }
        }
    }
}