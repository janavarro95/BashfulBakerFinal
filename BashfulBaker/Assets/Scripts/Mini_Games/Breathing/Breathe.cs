using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;

public class Breathe : MonoBehaviour
{
    public float LT;
    public float progress;
    public float progressToWin = 5;
    public GameObject pbar, sweat, top, bot;
    public float proficiencyBase = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        //pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        LT = InputControls.LeftTrigger;
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(top.transform.localPosition.y, bot.transform.localPosition.y, LT), transform.localPosition.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("zone") && progress <= progressToWin)
        {
            progress += .01f;
            progress += proficiencyBase * Game.Player.PlayerMovement.breathingProficiency;
            //pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
			var sweaty = sweat.emission;
			sweaty.rateOverTime = Mathf.Floor(progressToWin / progress);
        }
    }

    public bool isFinished()
    {
        return (progress >= progressToWin);
    }
}
