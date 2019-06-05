using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushRustler : MonoBehaviour
{
    // get animations
    private Animator anim;
    private Animation rustle;
    private ParticleSystem ps;
    public GameObject soundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = true;
        //rustle = GetComponent<Animation>();
        ps = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetShake(collision.gameObject);
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        SetShake(collision.gameObject);
    }*/

    void SetShake(GameObject g)
    {
        if (g.CompareTag("Player"))
        {
            // animate the bush
            anim.SetBool("shaking", true);
            ps.Play();
            Invoke("StopShaking", 0.5f);

            // play sound
            Instantiate(soundPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void StopShaking()
    {
        anim.SetBool("shaking", false);
        ps.Stop();
    }
}
