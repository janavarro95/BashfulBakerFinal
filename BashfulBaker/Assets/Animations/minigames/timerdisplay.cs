using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerdisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText;
    
    float timepassed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timepassed += Time.deltaTime;
        timerText.text = timepassed.ToString("00"); 
    }
}
