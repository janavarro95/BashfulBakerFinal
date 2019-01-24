using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TODO:
/// Do Pysics.RayCast2D from this game object to player collider on the OnTriggerStay/Enter to determine if this entity actually sees the player or not for moving towards it.
/// </summary>
public class StealthAwarenessZone : MonoBehaviour
{

    /// <summary>
    /// Keeps track of all of the moves made so that the guard can hopefully return home.
    /// </summary>
    private Stack<Vector2> pathBackToStart;

    /// <summary>
    /// The staring location of the guard.
    /// </summary>
    private Vector2 startingLocation;
    /// <summary>
    /// The next spot to move towards.
    /// </summary>
    private Vector2 nextTargetSpot;
    /// <summary>
    /// The spot we are moving from.
    /// </summary>
    private Vector2 sequenceStartingSpot;

    /// <summary>
    /// How fast the guard should move when chasing or returning home.
    /// </summary>
    public float movementSpeed = 1f;

    /// <summary>
    /// The lerp to the next target spot.
    /// </summary>
    [SerializeField]
    private float proximityToTarget;

    /// <summary>
    /// The minimum amount of distance a player can move inside the awareness zone for their location to be added to the queue of spots to go to.
    /// Smaller number = more refined/smoother path.
    /// </summary>
    public float minAwarenessDistance=1f;

    /// <summary>
    /// A list of all the spots we should explore.
    /// </summary>
    private List<Vector2> spotsToGoTo;

    /// <summary>
    /// If the guard is currently aware of the player.
    /// </summary>
    private bool awareOfPlayer;

    /// <summary>
    /// If the guard is finished looking around.
    /// </summary>
    private bool finishedLookingAround;

    /// <summary>
    /// Should the guard return home.
    /// </summary>
    private bool returnHome;

    private List<Vector3> listOfSpotsToLookAt;
    [SerializeField]
    private float lookAroundLerp;
    private Vector3 lookAtStart;

    [SerializeField]
    private float lookSpeed = 1f;


    public enum AIType
    {
        None,
        LookAroundWhileUnAware,
        LookAroundAfterFollow,
        LookAroundWhileReturning
    }

    public AIType aiType;

    // Start is called before the first frame update
    void Start()
    {
        spotsToGoTo = new List<Vector2>();
        pathBackToStart = new Stack<Vector2>();
        listOfSpotsToLookAt = new List<Vector3>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (awareOfPlayer)
        {
            if (returnHome == true)
            {
                //Debug.Log("GOING HOME!");
                //randomly look around
                if (aiType == AIType.LookAroundWhileReturning)
                {
                    aiLookLogic();
                }
                else if(aiType== AIType.LookAroundAfterFollow && finishedLookingAround==false)
                {
                    aiLookLogic();
                    return;
                }
                
                if (hasReturnedHome())
                {
                    resetGuardAwareness();
                    return;
                }
                proximityToTarget += getProperMovementSpeed()*Time.deltaTime;
                this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);
                if (proximityToTarget >= 1.0f)
                {
                    getNextReturnSpot();
                }
                if (aiType != AIType.LookAroundWhileReturning)
                {
                    this.gameObject.transform.right = nextTargetSpot - (Vector2)transform.position;
                }
                return;
            }
            else if (shouldChasePlayer())
            {
                proximityToTarget += getProperMovementSpeed()*Time.deltaTime;
                this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);

                if (proximityToTarget >= 1.0f)
                {
                    getNextTargetSpot();
                }

                //https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
                this.gameObject.transform.right = nextTargetSpot - (Vector2)transform.position;
                return;
            }
            else
            {
                
                if (needsToGoHome())
                {
                    if (proximityToTarget >= 1.0f || proximityToTarget == 0.0f)
                    {
                        Debug.Log("Time to go home!");
                        getNextReturnSpot();
                    }
                }
                
            }
        }
        else
        {
            //randomly look around
            if (aiType != AIType.None)
            {
                aiLookLogic();
            }
        }
    }

    public void aiLookLogic()
    {
        if (listOfSpotsToLookAt.Count == 0)
        {
            createLookAroundPoints();
            finishedLookingAround = true;
        }
        else
        {
            lookAround();
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
                if (Vector2.Distance(spotsToGoTo[spotsToGoTo.Count - 1], collision.gameObject.transform.position) <= minAwarenessDistance)
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
            Debug.Log("ALL OUT OF SPOTS!");
            getNextReturnSpot();
            returnHome = true;
            finishedLookingAround = false;
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
        if (spotsToGoTo.Count == 0 && pathBackToStart.Count == 0 && (Vector2)this.gameObject.transform.position==this.startingLocation) return true;
        else return false;
    }

    private bool shouldChasePlayer()
    {
        if (this.spotsToGoTo.Count > 0 ||(this.returnHome==false && this.spotsToGoTo.Count==0))
        {
            finishedLookingAround = false;
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

    public void lookAround()
    {

        if (lookAroundLerp ==0.0f) {
            lookAtStart = this.gameObject.transform.right;
            Vector2 lookAtSpot = listOfSpotsToLookAt[0];
            Debug.Log("look at: " + lookAtSpot);
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart,lookAtSpot - (Vector2)lookAtStart,lookAroundLerp);
            lookAroundLerp += getProperLookSpeed();
            //if (this.gameObject.transform.position.z < 0) this.gameObject.transform.position += new Vector3(0, 0, 180);
            return;
        }
        else if(lookAroundLerp>0.0f && lookAroundLerp<1.0f)
        {
            Vector2 lookAtSpot = listOfSpotsToLookAt[0];
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - (Vector2)lookAtStart, lookAroundLerp);
            lookAroundLerp += getProperLookSpeed();
            //if (this.gameObject.transform.position.z < 0) this.gameObject.transform.position += new Vector3(0, 0, 180);
            return;
        }
        else if (lookAroundLerp >= 1.0f)
        {
            listOfSpotsToLookAt.RemoveAt(0);
            lookAroundLerp = 0.0f;
            return;
        }
    }

    public void createLookAroundPoints()
    {
        int amount=Random.Range(1, 5);
        for(int i = 0; i < amount; i++)
        {
            
            Vector2 lookAtSpot = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            listOfSpotsToLookAt.Add(lookAtSpot);
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float getProperMovementSpeed()
    {
        float dist=Vector2.Distance(this.sequenceStartingSpot, this.nextTargetSpot);
        return movementSpeed/dist; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
    }

    private float getProperLookSpeed()
    {
        //lookAtStart = this.gameObject.transform.right;
        Vector2 lookAtSpot = listOfSpotsToLookAt[0];

        float dist = Vector2.Distance(lookAtStart, lookAtSpot);
        float speed = lookSpeed / (dist);
        //return speed*Time.deltaTime;
        return lookSpeed*Time.deltaTime;
    }
    
}
