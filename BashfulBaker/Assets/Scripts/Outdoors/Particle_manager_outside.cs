using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_manager_outside : MonoBehaviour
{

    public float spawn_timer;
    public GameObject beacon; 

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ParticleSystem>().enableEmission = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       /* spawn_timer -= Time.deltaTime;
        if (spawn_timer < 0)
        {
            Instantiate(beacon);
            beacon.transform.position = (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
            spawn_timer = 1;
        }*/
    }
}
