using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnd : MonoBehaviour
{
    public float start, end;
    public GameObject s, e;
    // Start is called before the first frame update
    void Start()
    {
        start = s.transform.position.x;
        end   = e.transform.position.x;
    }
}
