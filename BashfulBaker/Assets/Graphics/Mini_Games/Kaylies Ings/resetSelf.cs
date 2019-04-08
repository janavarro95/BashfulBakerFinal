using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetSelf : MonoBehaviour
{
    public Animator myAnimator;

    void resetAnimation()
    {
        myAnimator.SetBool("enterBowl", false);
    }
}
