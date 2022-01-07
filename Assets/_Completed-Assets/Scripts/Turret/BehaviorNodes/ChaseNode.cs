using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : Node
{
    private Transform m_target;
    private AIFindingVelocity m_findingVelocity;

    public ChaseNode(Transform target)
    {
        m_target = target;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(m_target.position, m_findingVelocity.transform.position);
        if (distance > 0.2f)
        {
            m_findingVelocity.SetMovePosition(m_target.position);
            m_nodeState = NodeState.RUNNING;
            return m_nodeState;
        }
        else
        {
            // m_findingVelocity.Stop();
            m_nodeState = NodeState.SUCCESS;
            return m_nodeState;
        }
    }
}
