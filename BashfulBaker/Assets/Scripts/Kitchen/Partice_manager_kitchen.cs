using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partice_manager_kitchen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player(Clone)").GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
