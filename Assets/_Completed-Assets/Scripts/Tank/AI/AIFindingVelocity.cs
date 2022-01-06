using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(TankAIMovement))]
public class AIFindingVelocity : MonoBehaviour
{
    // public TankAIMovement m_TankAIMovement;

    // /*[HideInInspector]*/ public Transform target;
    private Seeker seeker;
    Rigidbody rb;
    private Vector3 movePosition;

    // AI stuffs
    private float nextWaypointDistance;
    Path path;
    int currentWaypoint;
    public bool reachedEndOfPath { get; private set; }


    public Vector3 m_currentVelocity { get; private set; }

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();

        this.nextWaypointDistance = 1f;
        this.currentWaypoint = 0;
        this.reachedEndOfPath = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (path == null || reachedEndOfPath) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            ReachDestination();
            return;
        }

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        //TODO: make the tank rotate to the direction
        // m_TankAIMovement.SetVelocity(dir);
        m_currentVelocity = dir;

        // Check if we are close enough to the next waypoint
        float distance = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (currentWaypoint >= path.vectorPath.Count - 1 && distance < 0.5)
        {
            ReachDestination();
            return;
        }

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    private void ReachDestination()
    {
        reachedEndOfPath = true;
        //TODO: stop the tank
        // GetComponent<IMoveVelocity>().SetVelocity(Vector3.zero);
        m_currentVelocity = Vector3.zero;
    }

    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
        seeker.StartPath(transform.position, movePosition, OnPathComplete);
    }

    public Vector3 GetDestination()
    {
        return movePosition;
    }

    /// <summary> callback when creating path complete </summary>
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            this.reachedEndOfPath = false;
        }
    }
}
