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
    [SerializeField]
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
    /// A list of all the spots we should explore.
    /// </summary>
    public List<Vector2> spotsToGoTo;

    /// <summary>
    /// If the guard is currently aware of the player.
    /// </summary>
    private bool awareOfPlayer;

    /// <summary>
    /// Guard is not aware, but needs to investigate
    /// </summary>
    public Transform investigate = null;

    /// <summary>
    /// Should the guard return home.
    /// </summary>
    public bool returnHome;
    public bool talkingToPlayer = false;

    private List<Vector3> listOfSpotsToLookAt;
    [SerializeField]
    private float lookAroundLerp;
    private Vector2 lookAtStart;

    [SerializeField]
    public float lookSpeed = 1f;


    GameObject patrolInformation;
    private List<Vector3> patrolPoints;
    public Vector3 capturePatrolPoint;
    private Vector3 capturePatrolPointReset;
    private int currentPatrolPoint;
    public float movementLerp;
    private float movementLerpChange;


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
    public bool paused = false;

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

        float pauseTime = Random.Range(minPauseTime, maxPauseTime);
        this.patrolPauseTimer = new DeltaTimer((double)pauseTime, Assets.Scripts.Enums.TimerType.CountDown, false, null);


    }

    /// <summary>
    /// Gets all of the Patrol points for the guard.
    /// </summary>
    private void setUpPatrolPoint()
    {
        patrolPoints = new List<Vector3>();
        patrolInformation = this.gameObject.transform.parent.transform.Find("PatrolInformation").gameObject;

        if (patrolInformation.transform.childCount == 0) return;

        foreach (Transform point in patrolInformation.transform)
        {
            patrolPoints.Add(point.gameObject.transform.position);
        }
        this.startingLocation = patrolPoints[0];
        patrolPoints.Add(patrolPoints[0]);
        currentPatrolPoint = 0;

        if (this.patrolPoints.Count == 0 && (this.movementLogic == MovementType.ContinuousPatrolling || this.movementLogic == MovementType.PatrollAndPause))
        {
            //Debug.Log("WARNING! GUARD AI HAS NO MOVEMENT POINTS! SETTING TO NON-PATROLLING AI!");
            this.movementLogic = MovementType.None;
        }
        movementLerp = 0f;
    }


    /// <summary>
    /// Operate on state
    /// </summary>
    void FixedUpdate()
    {
        // state tracking
        myState = 1;

        awareOfPlayer = (!catching.hasEaten && (flashlight.seesPlayer || this.investigate != null));
        shouldMove = (awareOfPlayer || returnHome) && !talkingToPlayer;

        // AWARE
        if (awareOfPlayer)
        {
            // PURSUIT or INVESTIGATE
            bool pursuit = (flashlight.seesPlayer);
            bool invest  = (this.investigate != null);
            myState = 64 - (invest ? 0 : 14);

            // set question mark
            question.SetActive(invest && !pursuit);
            //Debug.Log("Aware of Player");

            // movement
            if (shouldMove)
            {
                movementLerpChange = getProperMovementSpeed(2f);
                movementLerp += movementLerpChange;

                // get next spot in the path
                if (movementLerp >= 1.0f)
                {
                    getNextTargetSpot();
                }
                // lerp there
                this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, movementLerp);
                // keep flashlight looking at player
                aiLookLogic();
            }

            //Animate here
            if (movementLerpChange != 0 && !talkingToPlayer)
            {
                animateGuard(this.transform.position, nextTargetSpot);
            }
            else
            {
                animateGuard(this.transform.position, this.transform.position, (this.transform.position.x < nextTargetSpot.x));
            }
        }
        // RETURN HOME
        else if (returnHome)
        {
            question.SetActive(false);
            // state tracking
            myState = 8;
            //Debug.Log("Returning Home");
            if (shouldMove)
            {
                // movement
                movementLerpChange = getProperMovementSpeed();
                movementLerp += movementLerpChange;

                // get next spot in the path
                if (movementLerp >= 1.0f)
                {
                    getNextReturnSpot();
                }

                // lerp there
                this.transform.parent.gameObject.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, movementLerp);
                // keep flashlight looking at player
                aiLookLogic();
            }
            //Animate here
            if (movementLerpChange != 0 && !talkingToPlayer)
            {
                animateGuard(this.transform.position, nextTargetSpot);
            }
            else
            {
                animateGuard(this.transform.position, this.transform.position, (this.transform.position.x < nextTargetSpot.x));
            }
        }
        // UNAWARE
        else
        {
            question.SetActive(false);
            // state tracking
            myState = 4;
            //Debug.Log("UNaware of Player");

            // looking
            aiLookLogic();

            // movement
            if(movementLogic == MovementType.None)
            {
                //Do nothing.
                //Animate here
                animateGuard(this.gameObject.transform.position, this.gameObject.transform.position);
            }
            else if (movementLogic == MovementType.ContinuousPatrolling || movementLogic== MovementType.PatrollAndPause)
            {
                //Patrol
                Patrol();
            }
        }
    }

    /// <summary>
    /// Patrols around different points looking for the player.
    /// </summary>
    private void Patrol()
    {
        // pause
        if (paused)
        {
            animateGuard(this.transform.position, this.transform.position);
            PatrolPauseTick();
            return;
        }

        // Patrol point wrap around
        if (currentPatrolPoint >= patrolPoints.Count || currentPatrolPoint < 0)
        {
            currentPatrolPoint = 0;
        }

        // Only move if not talking to player
        if (!talkingToPlayer)
        {
            // lerp 
            this.movementLerp += getProperMovementSpeed(sequenceStartingSpot, nextTargetSpot);  
            
            // reset sequences
            if (this.movementLerp >= 1.00f)
            {                
                // deterine sequences
                if (capturePatrolPoint != capturePatrolPointReset)
                {
                    sequenceStartingSpot = capturePatrolPoint;
                    nextTargetSpot = patrolPoints[(currentPatrolPoint) % patrolPoints.Count];

                    // reset capture
                    this.capturePatrolPoint = this.capturePatrolPointReset;
                    // deiterate
                    //currentPatrolPoint-=2;
                }
                else
                {
                    // patrol
                    sequenceStartingSpot = this.transform.position;
                    nextTargetSpot = patrolPoints[currentPatrolPoint];
                    this.startingLocation = nextTargetSpot;
                    // iterate
                    currentPatrolPoint++;
                }


                // reset movement lerps
                this.movementLerp = 0f;
                this.lookAroundLerp = 0f;
            }

            // move and animate
            this.gameObject.transform.parent.transform.position = Vector3.Lerp(sequenceStartingSpot, nextTargetSpot, movementLerp);
            animateGuard(sequenceStartingSpot, nextTargetSpot);
        }
    }

    void PatrolPauseTick()
    {
        question.SetActive(true);
        this.patrolPauseTimer.tick();
        if (this.patrolPauseTimer.IsFinished)
        {
            this.patrolPauseTimer.maxTime = (double)Random.Range(minPauseTime, maxPauseTime);
            this.patrolPauseTimer.restart();
            this.patrolPauseTimer.pause();
            paused = false;
            this.investigate = null;
            question.SetActive(false);
        }
    }

    public void PatrolPause()
    {
        paused = true;
        this.patrolPauseTimer.maxTime = (double)Random.Range(minPauseTime, maxPauseTime);
        this.patrolPauseTimer.restart();
        this.patrolPauseTimer.start();
    }
    public void PatrolPause(float time)
    {
        paused = true;
        this.patrolPauseTimer.maxTime = (double)time;
        this.patrolPauseTimer.restart();
        this.patrolPauseTimer.start();
    }

    /// <summary>
    /// animates the guard using its animator
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="nextPos"></param>
    public void animateGuard(Vector3 currentPos,Vector3 nextPos)
    {
        animationScript.animateGuard(currentPos,nextPos);
    }
    public void animateGuard(Vector3 currentPos,Vector3 nextPos, bool flip)
    {
        animationScript.animateGuard(currentPos,nextPos, flip);
    }

    /// <summary>
    /// ADD TO PATH
    /// </summary>
    /// <param name="t"></param>
    public void AddToPath(Transform t)
    {
        if (pathBackToStart.Count == 0)
            this.pathBackToStart.Push(this.transform.position);

        this.investigate = t;
        returnHome = false;
        spotsToGoTo.Add(t.position);
        getNextTargetSpot();
    }

    /// <summary>
    /// Gets the next spot the player was seen and try to move towards that location.
    /// </summary>
    private void getNextTargetSpot()
    {
        movementLerp = 0;

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
        movementLerp = 0;

        if (this.pathBackToStart.Count == 0)
        {
            returnHome = false;
            this.sequenceStartingSpot = this.gameObject.transform.position;
            this.nextTargetSpot = this.startingLocation;
            return;
        }
        
        this.sequenceStartingSpot = this.gameObject.transform.position;
        this.nextTargetSpot = this.pathBackToStart.Pop();
    }


    /// <summary>
    /// Randomly look around in a circle in an attempt to find the player.
    /// </summary>
    private void aiLookLogic()
    {
        if (flashlight == null) return;

        if (flashlight.seesPlayer && flashlight.visibleTargets.Count > 0)
        {
            aiLookDirectlyAt(flashlight.visibleTargets[0].transform.position);
        }
        else if (this.investigate != null)
        {
            aiLookDirectlyAt(investigate.position);
        }
        else
        {
            if (movementLogic == MovementType.ContinuousPatrolling && !paused && !returnHome)
            {
                aiLookDirectlyAt(patrolPoints[(currentPatrolPoint + patrolPoints.Count - 1) % patrolPoints.Count]);
            }
            else if (listOfSpotsToLookAt.Count == 0)
            {
                createLookAroundPoints();
            }
            else
            {
                lookAround();
            }
        }
    }

    /// <summary>
    /// LOOK AT THIS POINT OR TRANSFORM
    /// </summary>
    /// <param name="t"></param>
    #region aiLookAt(t or v)
    private void aiLookAt(Transform t)
    {
        Vector3 v = transform.position;
        aiLookAt(v);
    }
    private void aiLookAt(Vector3 v)
    {
        lookAround(v - this.transform.position);
    }
    private void aiLookDirectlyAt(Vector3 v)
    {
        Quaternion dirToTarget;
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(v.y - transform.position.y, v.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        dirToTarget = Quaternion.Euler(0, 0, AngleDeg);
        this.transform.rotation = dirToTarget;
    }
    #endregion

    /// <summary>
    /// Actually look around in a circle.
    /// Or at a given vector
    /// </summary>
    #region lookAround()
    private void lookAround()
    {
        if (lookAroundLerp == 0.0f)
        {
            lookAtStart = this.gameObject.transform.right;
            Vector2 lookAtSpot = listOfSpotsToLookAt[0];
            lookAroundLerp += getProperLookSpeed();
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - lookAtStart, lookAroundLerp);
            return;
        }
        else if(lookAroundLerp > 0.0f && lookAroundLerp < 1.0f)
        {
            Vector2 lookAtSpot = listOfSpotsToLookAt[0];
            lookAroundLerp += getProperLookSpeed();
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - lookAtStart, lookAroundLerp);
            return;
        }
        else if (lookAroundLerp >= 1.0f)
        {
            listOfSpotsToLookAt.RemoveAt(0);
            lookAroundLerp = 0.0f;
            return;
        }
    }
    private void lookAround(Vector2 v)
    {
        if (lookAroundLerp == 0.0f)
        {
            lookAtStart = this.gameObject.transform.right;
            Vector2 lookAtSpot = v;

            // check for 180 degree exception
            if (lookAtStart.y == lookAtSpot.y)
                lookAtStart.y += (lookAtStart.x > lookAtSpot.x ? 1 : -1);

            lookAroundLerp += getProperLookSpeed(v);
            //this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - lookAtStart, lookAroundLerp);
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - lookAtStart, movementLerp);
            return;
        }
        else if(lookAroundLerp > 0.0f && lookAroundLerp < 1.0f)
        {
            Vector2 lookAtSpot = v;
            lookAroundLerp += getProperLookSpeed(v);
            //this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - lookAtStart, lookAroundLerp);
            this.gameObject.transform.right = Vector2.Lerp(lookAtStart, lookAtSpot - lookAtStart, movementLerp);
            return;
        }
        else if (lookAroundLerp >= 1.0f)
        {
            lookAroundLerp = 0.0f;
            return;
        }
    }
    private void lookAround(Transform t)
    {
        Vector2 v = t.position;
        lookAround(v);
    }
    #endregion

    private void createLookAroundPoints()
    {
        //if (aiType == LookingType.None)
        //{
            int amount = Random.Range(1, 5);
            for (int i = 0; i < amount; i++)
            {

                Vector2 lookAtSpot = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                listOfSpotsToLookAt.Add(lookAtSpot);
            }
        //}
        /*else
        {
            Vector2 me = (Vector2)patrolPoints[currentPatrolPoint];
            Vector2 to = (Vector2)patrolPoints[(currentPatrolPoint + 1) % patrolPoints.Count];
            Vector2 next = (Vector2)patrolPoints[(currentPatrolPoint + 2) % patrolPoints.Count];

            // the direction to look
            Vector2 add = LookMod(me, to, 1f);
            add += LookMod(me, next, 0.5f);
            //add += LookMod(to, next, 0.2f);
            // add
            listOfSpotsToLookAt.Add(add);
        }*/
        
    }

    /// <summary>
    /// Get the movement speed of the guard.
    /// </summary>
    /// <returns></returns>
    #region PropermMovementSpeed
    private float getProperMovementSpeed()
    {
        float dist=Vector2.Distance(this.sequenceStartingSpot, this.nextTargetSpot);
        return (movementSpeed/dist) * Time.deltaTime; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
    }

    private float getProperMovementSpeed(float threshold)
    {
        float dist = Vector2.Distance(this.sequenceStartingSpot, this.nextTargetSpot);
        float speed;
        if (dist > threshold)
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
        if (dist > threshold)
            speed = (movementSpeed / dist) * Time.deltaTime;
        else speed = 0;

        return speed; //If dist is small then guard will move to target seemingly faster. If dist is large, then guard will move to target seemingly slower but all of it is at the same "speed"
    }
    #endregion

    private float getProperLookSpeed()
    {
        Vector2 lookAtSpot = listOfSpotsToLookAt[0];

        float dist = Vector2.Distance(lookAtStart, lookAtSpot);
        float sp = lookSpeed / (dist);
        return sp*Time.deltaTime;
        //return lookSpeed*Time.deltaTime;
    }
    private float getProperLookSpeed(Vector2 v)
    {
        Vector2 lookAtSpot = v;

        float dist = Vector2.Distance(lookAtStart, lookAtSpot);
        float sp = lookSpeed / (dist);
        return sp*Time.deltaTime;
        //return lookSpeed*Time.deltaTime;
    }

    private Vector2 LookMod(Vector2 here, Vector2 there)
    {
        Vector2 ret = new Vector2(0, 0);

        if (here.y == there.y)
        {
            if (here.x > there.x)
                ret.x -= 1;
            else
                ret.x += 1;
        }
        if (here.x == there.x)
        {
            if (here.y > there.y)
                ret.y -= 1;
            else
                ret.y += 1;
        }

        return ret;
    }

    private Vector2 LookMod(Vector2 here, Vector2 there, float weight)
    {
        Vector2 ret = new Vector2(0, 0);

        if (here.y == there.y)
        {
            if (here.x > there.x)
                ret.x -= weight;
            else
                ret.x += weight;
        }
        if (here.x == there.x)
        {
            if (here.y > there.y)
                ret.y -= weight;
            else
                ret.y += weight;
        }

        return ret;
    }
}
