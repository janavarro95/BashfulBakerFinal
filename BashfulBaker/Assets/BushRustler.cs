using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushRustler : MonoBehaviour
{
    // get animations
    private Animator anim;
    private Animation rustle;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = true;
        //rustle = GetComponent<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetShake(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SetShake(collision.gameObject);
    }

    void SetShake(GameObject g)
    {
        if (g.CompareTag("Player"))
        {
            anim.SetBool("shaking", true);
            Invoke("StopShaking", 0.5f);
        }
    }

    void StopShaking()
    {
        anim.SetBool("shaking", false);
    }
}
