using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetAnimation : MonoBehaviour
{
    public Animator flyingFood;
 





    public void resetFood()
    {

        flyingFood.SetInteger("addAnimation", 0);
    }


}
