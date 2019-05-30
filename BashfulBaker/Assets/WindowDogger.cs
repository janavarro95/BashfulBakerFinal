using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDogger : MonoBehaviour
{
    // bools and motions
    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
    }

    // when the player enters, play the dog pop up, then wag
    // set speed to 1
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.enabled = true;
        }
    }
}
