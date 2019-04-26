using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public bool shouldFollowTarget;
    public GameObject followTarget;

    // Start is called before the first frame update
    void Start()
    {
        shouldFollowTarget = true;
        followTarget = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollowTarget)
        {
            this.gameObject.transform.position = followTarget.transform.position + new Vector3(0, 0, -10);
        }
    }
}
