using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]

public class SoundPropagation : MonoBehaviour
{
    // public vars to modify a sound
    public float soundStartRad = 0.0f;
    public float soundEndRad = 1.0f;
    public float soundGrowthSpeed = 0.5f;

    // Components
    AudioSource audioSource;
    CircleCollider2D circleCollider;


    // Start is called before the first frame update
    void Start()
    {
        // get components
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();

        // set radius
        circleCollider.radius = soundStartRad;
    }

    // the sound will grow up to a point
    // then it will destroy itself
    // if interacting with any guards
    //      alert them (update their "investigate")
    //      with this as the new position
    void Update()
    {
        // grow
        circleCollider.radius += soundGrowthSpeed;

        // destroy
        // also check that sound has finished playing?
        if (circleCollider.radius >= soundEndRad)
            Destroy(this.gameObject);
    }

    // interact with guards
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if guard
        GameObject g = collision.gameObject;
        if (g.CompareTag("Guard"))
        {
            // investiagte set
            g.GetComponentInChildren<StealthAwarenessZone>().investigate = this.transform;
        }
    }
}
