using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;

public class Breathe : MonoBehaviour
{
    public float LT;
    private float progress;
    public GameObject pbar;
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        LT = InputControls.LeftTrigger;
        transform.position = new Vector3(transform.position.x, (1 - 2 * LT) * 4.1f, transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("zone") && progress < 20)
        {
            progress += .03f;
            pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
        }
    }
}
