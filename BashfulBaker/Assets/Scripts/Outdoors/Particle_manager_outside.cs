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
        GameObject.Find("Player(Clone)").GetComponent<ParticleSystem>().enableEmission = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        spawn_timer -= Time.deltaTime;
        if (spawn_timer < 0)
        {
            Instantiate(beacon);
            beacon.transform.position = (GameObject.Find("Player(Clone)").GetComponent<Transform>().position);
            spawn_timer = 1;
        }
    }
}
