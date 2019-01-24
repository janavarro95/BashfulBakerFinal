using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthAwarenessZone : MonoBehaviour
{

    private Stack<Vector2> pathBackToStart;

    private Vector2 startingLocation;
    private Vector2 nextTargetSpot;
    private Vector2 sequenceStartingSpot;

    private float lerpToNextLocation;

    public float chaseSpeed = 0.01f; //Need to figure out npc speed and factor that into the equation somehow.
    public float returnSpeed = 0.01f;

    [SerializeField]
    private float proximityToTarget;

    private List<Vector2> spotsToGoTo;

    private bool awareOfPlayer;

    private bool finishedLookingAround;

    private bool returnHome;

    // Start is called before the first frame update
    void Start()
    {
        spotsToGoTo = new List<Vector2>();
        pathBackToStart = new Stack<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Path To:" + spotsToGoTo.Count);
        Debug.Log("Path back:"+pathBackToStart.Count);
        if (awareOfPlayer)
        {
            if (returnHome==true)
            {
                Debug.Log("Returning home");
                if (hasReturnedHome())
                {
                    resetGuardAwareness();
                    return;
                }
                proximityToTarget += returnSpeed;
                this.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);
                if (proximityToTarget >= 1.0f)
                {
                    getNextReturnSpot();
                }
                return;
            }
            else if (shouldChasePlayer())
            {
                proximityToTarget += chaseSpeed;
                this.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);

                if (proximityToTarget >= 1.0f)
                {
                    getNextTargetSpot();
                }
                return;
                //if looking horizontally
                //if y>old y then rotate awareness zone up
                //if y<old y then rotate awareness zone down

                //if looking vertically
                //if x < old x then rotate awareness zone left
                //if x > old x then rotate awareness zone right
            }
            else if (needsToGoHome())
            {
                Debug.Log("I WANA GO HOME!");
                if (proximityToTarget >= 1.0f || proximityToTarget==0.0f)
                {
                    getNextReturnSpot();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(this.pathBackToStart.Count==0 && this.awareOfPlayer==false)this.startingLocation = this.gameObject.transform.position;
            spotsToGoTo.Add(collision.gameObject.transform.position); //Add the spot where the player was seen onto the queue
            awareOfPlayer = true;
            getNextTargetSpot();
            returnHome = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            returnHome = false;
            awareOfPlayer = true;
            if (spotsToGoTo.Count > 0)
            {
                if (spotsToGoTo[spotsToGoTo.Count - 1] == (Vector2)collision.gameObject.transform.position)
                {
                    Debug.Log("Don't add");
                    return;
                }
                if (Vector2.Distance(spotsToGoTo[spotsToGoTo.Count - 1], collision.gameObject.transform.position) <= 1f)
                {
                    Debug.Log("Dont add small");
                    return;
                }
            }
            spotsToGoTo.Add(collision.gameObject.transform.position); //Add the spot where the player was seen onto the queue
        }
    }

    /// <summary>
    /// Gets the next spot the player was seen and try to move towards that location.
    /// </summary>
    private void getNextTargetSpot()
    {

        proximityToTarget = 0.0f;
        if (this.spotsToGoTo.Count == 0)
        {
            getNextReturnSpot();
            returnHome = true;
            return;
        }
        this.sequenceStartingSpot = this.gameObject.transform.position;
        this.nextTargetSpot = spotsToGoTo[0];
        this.pathBackToStart.Push(spotsToGoTo[0]);
        spotsToGoTo.RemoveAt(0);
    }

    private void getNextReturnSpot()
    {
        //if (this.pathBackToStart.Count == 0) return;

        if (this.pathBackToStart.Count == 0)
        {
            this.sequenceStartingSpot = this.gameObject.transform.position;
            proximityToTarget = 0.0f;
            this.nextTargetSpot = this.startingLocation;
            returnHome = true;
            return;
        }

        proximityToTarget = 0.0f;
        this.sequenceStartingSpot = this.gameObject.transform.position;
        
        this.nextTargetSpot = this.pathBackToStart.Pop();
        returnHome = true;
    }


    private bool needsToGoHome()
    {
        if (spotsToGoTo.Count == 0 && pathBackToStart.Count != 0) return true;
        else return false;
    }

    private bool hasReturnedHome()
    {
        if (spotsToGoTo.Count == 0 && pathBackToStart.Count == 0 && this.proximityToTarget >= 1.0f) return true;
        else return false;
    }

    private bool shouldChasePlayer()
    {
        if (this.spotsToGoTo.Count > 0)
        {
            returnHome = false;
            return true;
        }
        else return false;
    }

    private void resetGuardAwareness()
    {
        this.returnHome = false;
        this.awareOfPlayer = false;
        return;
    }
}
