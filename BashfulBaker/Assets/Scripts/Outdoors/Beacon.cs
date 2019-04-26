using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public float Lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Lifetime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Lifetime -= Time.deltaTime;
        if (Lifetime < 0)
        {
           // //Debug.Log(this.transform.position.x);
            Destroy(gameObject);
        }
    }
}
