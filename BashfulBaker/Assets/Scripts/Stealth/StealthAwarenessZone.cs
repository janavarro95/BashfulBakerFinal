using Assets.Scripts.GameInformation;
using Assets.Scripts.Stealth;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TODO:
/// Also add in moving logic.
/// </summary>
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(StealthCaughtZone))]
public class StealthAwarenessZone : MonoBehaviour
{
    /// <summary>
    /// Tracking guard states with ints
    /// </summary>
    public int myState = 0;

    /// <summary>
    /// The flashlight that lights the player up
    /// </summary>
    public FieldOfView flashlight;
    public StealthCaughtZone catching;

    /// <summary>
    /// Keeps track of all of the moves made so that the guard can hopefully return home.
    /// </summary>
    private Stack<Vector2> pathBackToStart;

    /// <summary>
    /// The starting location of the guard.
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
    public float minAwarenessDistance = 1f;

    /// <summary>
    /// A list of all the spots we should explore.
    /// </summary>
    private List<Vector2> spotsToGoTo;

    /// <summary>
    /// If the guard is currently aware of the player.
    /// </summary>
    private bool awareOfPlayer;

    /// <summary>
    /// If the guard is currently aware of the player.
    /// </summary>
    private Transform investigate = null;

    /// <summary>
    /// If the guard is finished looking around.
    /// </summary>
    private bool finishedLookingAround;

    /// <summary>
    /// Should the guard return home.
    /// </summary>
    public bool returnHome;
    public bool talkingToPlayer = false;

    private List<Vector3> listOfSpotsToLookAt;
    [SerializeField]
    private float lookAroundLerp;
    private Vector3 lookAtStart;

    [SerializeField]
    public float lookSpeed = 1f;


    GameObject patrollInformation;
    private List<Vector3> patrollPoints;
    public Vector3 capturePatrolPoint;
    private Vector3 capturePatrolPointReset;
    private int currentPatrolPoint;
    public float movementLerp;

    [SerializeField]
    private DeltaTimer patrolPauseTimer;
    [SerializeField]
    private float minPauseTime = 1f;
    [SerializeField]
    private float maxPauseTime = 5f;

    public enum LookingType
    {
        None,
        LookAroundWhileUnAware,
        LookAroundAfterFollow,
        LookAroundWhileReturning
    }

    public enum MovementType
    {
        None,
        ContinuousPatrolling,
        PatrollAndPause
    }

    public MovementType movementLogic;

    public LookingType aiType;

    public bool chasesPlayer;
    public bool shouldMove;

    public GuardAnimationScript animationScript;

    public GameObject question;

    // Start is called before the first frame update
    void Start()
    {

        spotsToGoTo = new List<Vector2>();
        pathBackToStart = new Stack<Vector2>();
        listOfSpotsToLookAt = new List<Vector3>();
        flashlight = GetComponent<FieldOfView>();
        catching = GetComponent<StealthCaughtZone>();

        capturePatrolPointReset = new Vector3(-1000, -1000, -1000);
        capturePatrolPoint = capturePatrolPointReset;

        this.startingLocation = this.transform.position;

        if (movementLogic == MovementType.ContinuousPatrolling || this.movementLogic == MovementType.PatrollAndPause)
        {
            setUpPatrolPoint();
        }
        if (movementLogic == MovementType.PatrollAndPause)
        {
            float pauseTime = Random.Range(minPauseTime, maxPauseTime);
            this.patrolPauseTimer = new DeltaTimer((double)pauseTime, Assets.Scripts.Enums.TimerType.CountDown, false, null);
        }


    }

    /// <summary>
    /// Gets all of the patrol points for the guard.
    /// </summary>
    private void setUpPatrolPoint()
    {
        patrollPoints = new List<Vector3>();
        patrollInformation = this.gameObject.transform.parent.transform.Find("PatrolInformation").gameObject;

        if (patrollInformation.transform.childCount == 0) return;

        foreach (Transform point in patrollInformation.transform)
        {
            patrollPoints.Add(point.gameObject.transform.position);
        }
        this.startingLocation = patrollPoints[0];
        patrollPoints.Add(patrollPoints[0]);
        currentPatrolPoint = 0;

        if (this.patrollPoints.Count == 0 && (this.movementLogic == MovementType.ContinuousPatrolling || this.movementLogic == MovementType.PatrollAndPause))
        {
            //Debug.Log("WARNING! GUARD AI HAS NO MOVEMENT POINTS! SETTING TO NON-PATROLLING AI!");
            this.movementLogic = MovementType.None;
        }
        movementLerp = 0f;
    }

    /// <summary>
    /// Patrols around different points looking for the player.
    /// </summary>
    private void patrol()
    {
        // patrol and pause
        if (this.movementLogic == MovementType.PatrollAndPause)
        {
            this.patrolPauseTimer.tick();
            if (this.patrolPauseTimer.IsFinished)
            {
                this.patrolPauseTimer.maxTime = (double)Random.Range(minPauseTime, maxPauseTime);
                this.patrolPauseTimer.restart();
                this.patrolPauseTimer.pause();
            }
            else if (this.patrolPauseTimer.IsTicking)
            {
                return;
            }
        }

        // patrol point wrap around
        if (currentPatrolPoint + 1 >= patrollPoints.Count)
        {
            currentPatrolPoint = 0;
        }

        // movement lerp
        if (!talkingToPlayer)
        {
            Vector3 here, there;
            if (capturePatrolPoint != capturePatrolPointReset)
            {
                here = capturePatrolPoint;
                there = patrollPoints[currentPatrolPoint + 1];
            }
            else
            {
                here = patrollPoints[currentPatrolPoint];
                there = patrollPoints[currentPatrolPoint + 1];
            }
            this.movementLerp += getProperMovementSpeed(here, there);
            this.gameObject.transform.parent.transform.position = Vector3.Lerp(here, there, movementLerp);
            animateGuard(here, there);
        }

        // looking around while returning?
        // might just comment out
        if (aiType != LookingType.LookAroundWhileReturning)
        {
            this.gameObject.transform.right = (Vector2)patrollPoints[currentPatrolPoint+1] - (Vector2)transform.position;
        }

        // lerp it up
        if (this.movementLerp >= 1.00f)
        {
            currentPatrolPoint++;
            this.startingLocation = this.transform.position;
            this.movementLerp = 0f;
            this.capturePatrolPoint = this.capturePatrolPointReset;

            if (this.movementLogic == MovementType.PatrollAndPause)
            {
                this.patrolPauseTimer.start();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // state tracking
        myState = 1;

        awareOfPlayer = (!catching.hasEaten && (flashlight.seesPlayer || this.investigate != null || returnHome));
        shouldMove = awareOfPlayer && !talkingToPlayer;

        // INTRIGUED
        if (awareOfPlayer)
        {
            // state tracking
            myState = 2;

            // PURSUIT
            if (flashlight.seesPlayer)
            {
                Game.StealthManager.AddAwareGuard(this);
                // state tracking
                myState = 64;
                question.SetActive(false);
                //Debug.Log("Aware of Player");

                // movement
                if (shouldMove)
                {
                    proximityToTarget += getProperMovementSpeed();

                    // get next spot in the path
                    if (proximityToTarget >= 1.0f)
                    {
                        getNextTargetSpot();
                    }

                    // lerp there
                    this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);
                    // keep flashlight looking at player
                    aiLookLogic();
                }

                //Animate here
                animateGuard(sequenceStartingSpot, nextTargetSpot);

                return;
            }
            // INVESTIGATING
            else if (this.investigate != null)
            {
                //Debug.Log("Investigating Player");
                question.SetActive(true);
                // state tracking
                myState = 50;

                // movement
                if (shouldMove)
                {
                    proximityToTarget += getProperMovementSpeed();

                    // get next spot in the path
                    if (proximityToTarget >= 1.0f)
                    {
                        getNextTargetSpot();
                    }

                    // lerp there
                    this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);
                    // keep flashlight looking at player
                    aiLookLogic();
                }

                //Animate here
                animateGuard(sequenceStartingSpot, nextTargetSpot);
                
                return;
            }// returning home
            else
            {
                question.SetActive(false);
                // state tracking
                myState = 8;
                //Debug.Log("Returning Home");
                Game.StealthManager.RemoveAwareGuard(this);
                // movement
                if (shouldMove)
                {
                    proximityToTarget += getProperMovementSpeed();

                    // get next spot in the path
                    if (proximityToTarget >= 1.0f)
                    {
                        getNextReturnSpot();
                    }

                    // lerp there
                    this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);
                    // keep flashlight looking at player
                    aiLookLogic();
                }
                //Animate here
                animateGuard(sequenceStartingSpot, nextTargetSpot);
            }
        }
        // returning home
        /*else if (returnHome)
        {
            question.SetActive(false);
            // state tracking
            myState = 8;
            //Debug.Log("Returning Home");

            // movement
            if (shouldMove)
            {
                proximityToTarget += getProperMovementSpeed();

                // get next spot in the path
                if (proximityToTarget >= 1.0f)
                {
                    getNextReturnSpot();
                }

                // lerp there
                this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, proximityToTarget);
                // keep flashlight looking at player
                aiLookLogic();
            }
            //Animate here
            animateGuard(sequenceStartingSpot, nextTargetSpot);
        }*/
        // UNAWARE
        else
        {
            question.SetActive(false);
            // state tracking
            myState = 4;
            //Debug.Log("UNaware of Player");

            

            // looking
            if (aiType != LookingType.None)
            {
                aiLookLogic();
            }

            // movement
            if(movementLogic == MovementType.None)
            {
                //Do nothing.
                //Animate here
                animateGuard(this.gameObject.transform.position, this.gameObject.transform.position);
            }
            else if (movementLogic == MovementType.ContinuousPatrolling || movementLogic== MovementType.PatrollAndPause)
            {
                patrol();
                // Animate here
                //animateGuard(sequenceStartingSpot, nextTargetSpot);
            }
        }
    }

    public void animateGuard(Vector3 currentPos,Vector3 nextPos)
    {
        animationScript.animateGuard(currentPos,nextPos);
    }

    /// <summary>
    /// Randomly look around in a circle in an attempt to find the player.
    /// </summary>
    private void aiLookLogic()
    {
        if (flashlight == null) return;
        if(flashlight.visibleTargets.Count > 0)
        {
            Transform target = flashlight.visibleTargets[0].transform;
            Quaternion dirToTarget;
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            dirToTarget = Quaternion.Euler(0, 0, AngleDeg);
            this.transform.rotation = dirToTarget;
        }
        else if (listOfSpotsToLookAt.Count == 0)
        {
            createLookAroundPoints();
            finishedLookingAround = true;
        }
        else
        {
            lookAround();
        }
    }

    public void AddToPath(Transform t)
    {
        if (this.pathBackToStart.Count == 0)
        {
            this.pathBackToStart.Push(this.startingLocation);
        }
        this.investigate = t;
        spotsToGoTo.Add(t.position); //Add the spot where the player was seen onto the queue
        getNextTargetSpot();
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
            this.investigate = null;
            return;
        }

        this.sequenceStartingSpot = this.gameObject.transform.position;
        this.nextTargetSpot = spotsToGoTo[0];
        this.pathBackToStart.Push(spotsToGoTo[0]);
        spotsToGoTo.RemoveAt(0);
    }

    /// <summary>
    /// Gets the next spot in the path of places to return to to find the guard's way back to their original
    /// </summary>
    private void getNextReturnSpot()
    {
        proximityToTarget = 0.0f;

        if (this.pathBackToStart.Count == 0)
        {
            returnHome = hasReturnedHome();
            this.sequenceStartingSpot = this.gameObject.transform.position;
            this.nextTargetSpot = this.startingLocation;
            this.movementLerp = 0;
            return;
        }

        //returnHome = hasReturnedHome();
        this.sequenceStartingSpot = this.gameObject.transform.position;
        this.nextTargetSpot = this.pathBackToStart.Pop();
    }


    private bool needsToGoHome()
    {
        if (spotsToGoTo.Count == 0 && pathBackToStart.Count != 0 && this.investigate != null) return true;
        else return false;
    }

    private bool hasReturnedHome()
    {
        Debug.Log("returning home??");
        if ((Vector2)this.gameObject.transform.position==this.startingLocation) return true;
        else return false;
    }

    private bool shouldChasePlayer()
    {
        if (chasesPlayer == false) return false;
        if (this.spotsToGoTo.Count > 0 || (this.returnHome==false && this.spotsToGoTo.Count==0))
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

    /// <summary>
    /// Actually look around in a circle.
    /// </summary>
    private void lookAround()
    {

        if (lookAroundLerp == 0.0f) {
            lookAtStart = this.gameObject.transform.right;
            Vector2 lookAtSpot = listOfSpotsToLookAt[0];
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart,lookAtSpot - (Vector2)lookAtStart,lookAroundLerp);
            lookAroundLerp += getProperLookSpeed();
            //if (this.gameObject.transform.position.z < 0) this.gameObject.transform.position += new Vector3(0, 0, 180);
            return;
        }
        else if(lookAroundLerp > 0.0f && lookAroundLerp < 1.0f)
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

    private void createLookAroundPoints()
    {
        int amount=Random.Range(1, 5);
        for(int i = 0; i < amount; i++)
        {
            
            Vector2 lookAtSpot = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            listOfSpotsToLookAt.Add(lookAtSpot);
        }
        
    }

    /// <summary>
    /// Get the movement speed of the guard.
    /// </summary>
    /// <returns></returns>
    private float getProperMovementSpeed()
    {
        float dist=Vector2.Distance(this.sequenceStartingSpot, this.nextTargetSpot);
        return (movementSpeed/dist) * Time.deltaTime; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
    }

    private float getProperMovementSpeed(float threshold)
    {
        float dist = Vector2.Distance(this.sequenceStartingSpot, this.nextTargetSpot);
        float speed;
        if (dist < threshold)
            speed = (movementSpeed / dist) * Time.deltaTime;
        else speed = 0;

        return speed; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
    }

    private float getProperMovementSpeed(Vector2 startSpot, Vector2 endSpot)
    {
        float dist = Vector2.Distance(startSpot, endSpot);
        return (movementSpeed / dist)*Time.deltaTime; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
    }

    private float getProperMovementSpeed(Vector2 startSpot, Vector2 endSpot, float threshold)
    {
        float dist = Vector2.Distance(startSpot, endSpot);
        float speed;
        if (dist < threshold)
            speed = (movementSpeed / dist) * Time.deltaTime;
        else speed = 0;

        return speed; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
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

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<PlayerMovement>().hidden)
        {
            if (!shouldMove) return;

            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, collision.gameObject.transform.position - this.gameObject.transform.position);
            if (hit.collider.gameObject.tag == "Obstacle")
            {
                return;
            }

            if(this.pathBackToStart.Count==0 && this.awareOfPlayer==false)
                this.startingLocation = this.gameObject.transform.position;

            this.investigate = collision.gameObject.transform;
            spotsToGoTo.Add(investigate.position); //Add the spot where the player was seen onto the queue
            awareOfPlayer = true;
            getNextTargetSpot();
            returnHome = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<PlayerMovement>().hidden)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, collision.gameObject.transform.position - this.gameObject.transform.position);
            if (hit.collider.gameObject.tag == "Obstacle")
            {
                return;
            }

            returnHome = false;
            awareOfPlayer = true;
            this.investigate = collision.gameObject.transform;

            if (spotsToGoTo.Count > 0)
            {
                if (spotsToGoTo[spotsToGoTo.Count - 1] == (Vector2)investigate.position)
                {
                    return;
                }
                if (Vector2.Distance(spotsToGoTo[spotsToGoTo.Count - 1], investigate.position) <= minAwarenessDistance)
                {
                    return;
                }
            }
            spotsToGoTo.Add(investigate.position); //Add the spot where the player was seen onto the queue
        }
    }*/
}
