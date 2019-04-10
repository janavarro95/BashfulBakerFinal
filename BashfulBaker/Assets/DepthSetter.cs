using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSetter : MonoBehaviour
{
    public bool update = false;
    public bool other = true;
    public bool self = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateChildrenDepth();
    }

    private void Update()
    {
        if (update)
        {
            if (other)
                UpdateChildrenDepth();
            if (self)
                UpdateSelfDepth(this.transform);
        }
    }

    private void UpdateChildrenDepth()
    {
        foreach (Transform child in transform)
        {
            UpdateSelfDepth(child);
        }
    }
    private void UpdateSelfDepth(Transform mine)
    {
        float depth = mine.position.y;
        Vector3 newSet = new Vector3(mine.position.x, mine.position.y, depth);
        mine.position = newSet;
    }
}
