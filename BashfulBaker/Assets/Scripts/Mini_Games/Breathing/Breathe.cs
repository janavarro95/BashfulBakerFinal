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
    public GameObject pbar, dane, top, bot;
	public ParticleSystem sweat, playerSchweat;
    public float proficiencyBase = 0.005f;

	public AudioSource fast, slow;
    // Start is called before the first frame update
    void Start()
    {
        progress = .5f;
        //pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
		playerSchweat = Game.Player.gameObject.GetComponentInChildren<ParticleSystem>();
		fast = dane.GetComponents<AudioSource>()[0];
		slow = dane.GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        LT = InputControls.LeftTrigger;
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(top.transform.localPosition.y, bot.transform.localPosition.y, LT), transform.localPosition.z);
		var sweaty = sweat.emission;
		sweaty.rateOverTime = Mathf.Ceil(progressToWin / progress);
		var schweaty = playerSchweat.emission;
		schweaty.rateOverTime = Mathf.Ceil(progressToWin / progress);
		if(!(fast.isPlaying || slow.isPlaying))
		{
			fast.Play();
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("zone") && progress < progressToWin)
        {
            progress += .01f;
            progress += proficiencyBase * Game.Player.PlayerMovement.breathingProficiency;
            //pbar.transform.localScale = new Vector3(progress, pbar.transform.localScale.y, 1);
			
			if(progress >= progressToWin)
			{
				var schweaty = playerSchweat.emission;
				schweaty.rateOverTime = 0;
				var sweaty = sweat.emission;
				sweaty.rateOverTime = 0;
			}
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag.Equals("zone") && !slow.isPlaying)
		{
			fast.Stop();
			slow.Play();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag.Equals("zone") && !fast.isPlaying)
		{
			slow.Stop();
			fast.Play();
		}
	}

    public bool isFinished()
    {
        return (progress >= progressToWin);
    }
}
