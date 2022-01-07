using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : Node
{
    protected AIFindingVelocity m_findingVelocity;
    protected TruckAIMovement m_truckAIMovement;
    protected Transform m_randomPoint1;
    protected Transform m_randomPoint2;

    protected float timeToResetPath = 3f;
    protected float currentTimeToResetPath = 3f;
    protected Vector3 m_previousPosition;

    public MoveNode(AIFindingVelocity findingVelocity, TruckAIMovement truckAIMovement, Transform randomPoint1, Transform randomPoint2)
    {
        m_findingVelocity = findingVelocity;
        m_truckAIMovement = truckAIMovement;
        m_randomPoint1 = randomPoint1;
        m_randomPoint2 = randomPoint2;

        m_previousPosition = new Vector3(0, 0, 0);
    }

    public override NodeState Evaluate()
    {
        GeneratePathIfNeeded();

        // move
        Vector3 directionToGo = m_findingVelocity.m_currentVelocity;
        directionToGo.y = 0;

        float angle = Vector3.Angle(m_truckAIMovement.transform.forward, directionToGo);
        Vector3 cross = Vector3.Cross(m_truckAIMovement.transform.forward, directionToGo);
        if (cross.y > 0)
        {
            angle = -angle;
        }

        // if (angle < 0)
        if (directionToGo == Vector3.zero)
            m_truckAIMovement.Stop();
        else if (angle > 5)
        {
            m_truckAIMovement.TurnMoveLeft();
        }
        else if (angle < -5)
        {
            m_truckAIMovement.TurnMoveRight();
        }
        else
        {
            m_truckAIMovement.MoveForward();
        }

        m_nodeState = NodeState.RUNNING;
        return m_nodeState;
    }

    protected void GeneratePathIfNeeded()
    {
        // bool resetFlag = 

        if (m_findingVelocity.reachedEndOfPath || m_findingVelocity.path == null || IsNeedToReset())
        {
            currentTimeToResetPath = timeToResetPath;

            float x = 0;
            float z = 0;
            if (m_randomPoint1.position.x > m_randomPoint2.position.x)

                x = Random.Range(m_randomPoint2.position.x, m_randomPoint1.position.x);

            else

                x = Random.Range(m_randomPoint1.position.x, m_randomPoint2.position.x);

            if (m_randomPoint1.position.z > m_randomPoint2.position.z)

                z = Random.Range(m_randomPoint2.position.z, m_randomPoint1.position.z);

            else

                z = Random.Range(m_randomPoint1.position.z, m_randomPoint2.position.z);

            m_findingVelocity.SetMovePosition(new Vector3(x, 0f, z));
        }
    }

    private bool IsNeedToReset()
    {
        if (currentTimeToResetPath <= 0)
        {
            currentTimeToResetPath = timeToResetPath;
            if (Vector3.Distance(m_previousPosition, m_truckAIMovement.transform.position) < 1)
            {
                m_previousPosition = m_truckAIMovement.transform.position;
                return true;
            }
            m_previousPosition = m_truckAIMovement.transform.position;
            return false;
        }
        else
        {
            currentTimeToResetPath -= Time.deltaTime;
            return false;
        }
    }
}
