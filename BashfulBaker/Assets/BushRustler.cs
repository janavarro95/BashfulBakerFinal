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

    private void LateUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //anim.enabled = true;
            anim.SetBool("shaking", true);
            Invoke("StopShaking", .2f);
        }
    }
    void StopShaking()
    {
        anim.SetBool("shaking", false);
    }
    /*  private void OnTriggerExit2D(Collider2D collision)
      {
          if (collision.gameObject.CompareTag("Player"))
          {
              anim.enabled = false;
          }
      } */
}
