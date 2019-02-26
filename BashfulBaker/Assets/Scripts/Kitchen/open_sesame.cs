using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_sesame : MonoBehaviour
{
    GameObject Jeb;
    private void Start()
    {
        Jeb = GameObject.Find("Jeb");
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().currentStep > 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(Jeb.transform.position.x > -.4)
        {
            this.gameObject.SetActive(false);
        }
    }
}
