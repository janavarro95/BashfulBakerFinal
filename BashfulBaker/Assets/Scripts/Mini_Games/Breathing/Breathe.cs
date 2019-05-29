using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;

public class Breathe : MonoBehaviour
{
    public float LT;
    public float progress;
    public GameObject pbar, top, bot;
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
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(top.transform.localPosition.y, bot.transform.localPosition.y, LT), transform.localPosition.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("zone") && progress < 5)
        {
            progress += .01f;
            pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
        }
    }
}
