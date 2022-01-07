using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float m_range;
    private Transform m_origin;
    public RangeNode(float range, Transform origin)
    {
        this.m_range = range;
        this.m_origin = origin;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(m_origin.position, m_range);
        foreach (Collider collider in colliders)
        {
            // Debug.Log(collider.gameObject.name);
            if (collider.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                m_nodeState = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
        }
        m_nodeState = NodeState.FAILURE;
        return m_nodeState;
    }
}
