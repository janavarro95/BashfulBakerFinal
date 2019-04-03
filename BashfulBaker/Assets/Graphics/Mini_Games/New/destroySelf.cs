using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySelf : MonoBehaviour
{
    //public Animator myAnimator;

    void resetAnimation()
    {
        Destroy(gameObject);
    }
}
