using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities.Timers;
using Assets.Scripts.Stealth;

public class FieldOfView : MonoBehaviour
{
    // seeing variables
    public bool sawPlayer = false;
    public bool seesPlayer = false;
    // stealth awareness zone
    StealthAwarenessZone zone;
    public float pathUpdateReset = 1f;
    private float pathUpdateTimer = 0f;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //[HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public int edgeResolveIterations;
    public float edgeDistThreshold;

    public bool Static, unlit;

    private DeltaTimer meshTimer;

    GameObject cam;
    public GameObject guard, alert;
    Vector3 startPoint;

    GuardAnimationScript guardAnimator;

    private void Start()
    {
        zone = GetComponent<StealthAwarenessZone>();
        cam = GameObject.Find("Main Camera");
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        DrawFieldOfView();

        //meshTimer = new DeltaTimer(0.1d, Assets.Scripts.Enums.TimerType.CountDown, true, new Assets.Scripts.Utilities.Delegates.VoidDelegate(DrawFieldOfView));
        //meshTimer.start();

        try
        {
            guard = gameObject.transform.parent.gameObject;
            guardAnimator = guard.GetComponent<GuardAnimationScript>();
        }
        catch(System.Exception err)
        {
            guard = this.gameObject;
        }
        startPoint = guard.transform.position;
    }

    private void LateUpdate()
    {
        if (Static) return;

        pathUpdateTimer += Time.deltaTime;
        if (pathUpdateTimer > pathUpdateReset)
        {
            FindVisibleTargets();
            pathUpdateTimer = 0;
        }


        if(visibleTargets.Count < 1)
        {
            //guard.transform.position = Vector3.MoveTowards(guard.transform.position, startPoint, 0.02f);
            //if (guardAnimator != null) guardAnimator.animateGuard(guard.transform.position, startPoint);
            alert.SetActive(false);
        }
        if (!unlit && Vector3.Distance(transform.position, cam.transform.position) < (Camera.main.orthographicSize * Screen.width / Screen.height) + viewRadius * 2)
        {
            DrawFieldOfView();
            //meshTimer.Update();

        }
    }

    void FindVisibleTargets()
    {
        if (!zone.talkingToPlayer)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null) return;
            sawPlayer = visibleTargets.Contains(GameObject.FindGameObjectWithTag("Player").transform);
            seesPlayer = false;
            visibleTargets.Clear();
            Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

            for (int x = 0; x < targetsInViewRadius.Length; x++)
            {
                Transform target = targetsInViewRadius[x].transform;
                Quaternion dirToTarget;
                // Get Angle in Radians
                float AngleRad = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x);
                // Get Angle in Degrees
                float AngleDeg = (180 / Mathf.PI) * AngleRad;
                dirToTarget = Quaternion.Euler(0, 0, AngleDeg);

                RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, target.position - this.gameObject.transform.position);
                if (hit.collider.gameObject.tag == "Obstacle")
                {
                    continue;
                }
                else if (!(target == GameObject.FindGameObjectWithTag("Player").transform && target.GetComponent<PlayerMovement>().hidden) && Quaternion.Angle(transform.rotation, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics2D.Raycast(transform.position, (target.position - transform.position).normalized, distToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                        if (target == GameObject.FindGameObjectWithTag("Player").transform)
                        {
                            if (!sawPlayer && target)
                            {
                                target.GetComponent<PlayerMovement>().Spotted();
                            }
                            seesPlayer = true;
                        }
                        //guard.transform.position = Vector3.MoveTowards(guard.transform.position, target.transform.position, .1f / distToTarget);
                        //if (guardAnimator != null) guardAnimator.animateGuard(guard.transform.position, startPoint,true);
                        zone.AddToPath(target.transform);
                        alert.SetActive(true);
                    }
                }
            }

            if (sawPlayer && !seesPlayer)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Escaped();
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int x = 0; x <= stepCount; x++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * x;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (x > 0)
            {
                bool edgeDistThresholdExceeded = Mathf.Abs(oldViewCast.dist - newViewCast.dist) > edgeDistThreshold;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA!= Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int x = 0; x < vertexCount - 1; x++)
        {
            vertices[x + 1] = transform.InverseTransformPoint(viewPoints[x]);

            if (x < vertexCount - 2)
            {
                triangles[x * 3] = 0;
                triangles[x * 3 + 1] = x + 2;
                triangles[x * 3 + 2] = x + 1;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);
        if (hit)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int x = 0; x < edgeResolveIterations; x++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistThresholdExceeded = Mathf.Abs(minViewCast.dist - newViewCast.dist) > edgeDistThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDistThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}