using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBarker : MonoBehaviour
{
    // sound
    public GameObject soundPrefab;

    // timer
    public float timer = 0;
    public float timerReset = 5;
    public float timerResetVariance = 2;

    private FieldOfView awareness;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerReset;
        awareness = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // reset
            timer = timerReset + Random.Range(-timerResetVariance, timerResetVariance);
            if (awareness.seesPlayer)
            {
                timer /= 3;
            }
            // trigger
            Instantiate(soundPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
