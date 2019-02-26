using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progress : MonoBehaviour
{
    public int step;
    public GameObject A;
    public Vector3 pos0, pos1, pos2, pos3, pos4;
    public GameObject Gandalf, BlackKnight,Player;

    // Start is called before the first frame update
    void Start()
    {
        A.SetActive(false);
        //this.transform.position = pos0;
        Gandalf.SetActive(false);
        Player = GameObject.Find("Player(Clone)");
        Player.GetComponent<PlayerMovement>().arrow = this.gameObject;
        step = Player.GetComponent<PlayerMovement>().currentStep;
        SetStep(step);
    }

    // Update is called once per frame
    void Update()
    {
        if (step < 4 && Player.transform.position.y > -3)
        {
            Gandalf.SetActive(true);
        }
    }

    public void SetStep(int next)
    {
        step = next;
        Debug.Log("moving to step " + step);
        switch (step)
        {
            case 0:
                {
                    this.transform.position = pos0;
                    break;
                }
            case 1:
                {
                    this.transform.position = pos1;
                    break;
                }
            case 2:
                {
                    this.transform.position = pos2;
                    break;
                }
            case 3:
                {
                    this.transform.position = pos3;
                    break;
                }
            case 4:
                {
                    Gandalf.SetActive(false);
                    BlackKnight.SetActive(false);
                    this.transform.position = pos4;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
