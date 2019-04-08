using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowDirection : MonoBehaviour
{

    /// <summary>
    /// The target object to look at.
    /// </summary>
    public Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            lookAt();
        }
    }
    /// <summary>
    /// https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
    /// </summary>
    private void lookAt()
    {
        Vector3 diff = targetObject.position - transform.position; //Get the vector of difference to point the way,
        diff.Normalize(); //Notmalize it.

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; //Get the angle in degrees. Convert from radians to degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90); //Create a new quaterion and make that the rotation.

        this.gameObject.transform.parent.rotation= Quaternion.Euler(0f, 0f, rot_z - 270);

    }

    public void setTargetObject(GameObject target)
    {
        if (target != null)
        {
            targetObject = target.transform;
        }
    }
}
