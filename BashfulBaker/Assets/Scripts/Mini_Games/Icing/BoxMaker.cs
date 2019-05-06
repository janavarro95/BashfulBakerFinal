using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMaker : MonoBehaviour
{
    public GameObject Box;
    public int boxes;
    public SpriteRenderer complete;
    void Start()
    {
        boxes = 1;
        Vector3 start = new Vector3(transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.extents.y, 0);
        Vector3 end = new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x, transform.position.y + GetComponent<SpriteRenderer>().bounds.extents.y, 0);

        for (float x = start.x; x < end.x; x += .25f)
        {
            for (float y = start.y; y < end.y; y += .25f)
            {
                Instantiate(Box, new Vector3(x, y, -3f), Box.transform.rotation);
                boxes++;
            }
        }
    }

    private void Update()
    {
        if (boxes > 0) { return; }

        complete.enabled = true;
    }
}
