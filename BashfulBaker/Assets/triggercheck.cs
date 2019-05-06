using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggercheck : MonoBehaviour
{

    public bool beenHit;
    // Start is called before the first frame update
    void Start()
    {
        beenHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        beenHit = true;
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().defaultSpeed = 0f;
    }
}
