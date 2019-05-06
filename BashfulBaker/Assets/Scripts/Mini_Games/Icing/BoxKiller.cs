using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxKiller : MonoBehaviour
{
    public GameObject cake;
    private void OnTriggerEnter2D()
    {
        cake.GetComponent<BoxMaker>().boxes--;
        Destroy(gameObject);
    }
}
