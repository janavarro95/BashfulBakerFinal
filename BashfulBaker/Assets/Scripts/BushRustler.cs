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

    public List<AudioClip> jimmies = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim != null)
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
            if (anim != null)
                anim.SetBool("shaking", true);
            ps.Play();
            Invoke("StopShaking", 0.5f);

            // play sound
            GameObject s = Instantiate(soundPrefab, this.transform.position, Quaternion.identity);
            s.GetComponent<AudioSource>().clip = jimmies[Random.Range(0, jimmies.Count-1)];
        }
    }

    void StopShaking()
    {
        if (anim != null)
            anim.SetBool("shaking", false);
        ps.Stop();
    }
}
