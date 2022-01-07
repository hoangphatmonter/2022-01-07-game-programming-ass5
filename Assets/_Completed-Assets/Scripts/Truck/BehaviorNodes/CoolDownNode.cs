using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownNode : Node
{
    protected TruckItem m_truckItem;

    public CoolDownNode(TruckItem truckItem)
    {
        m_truckItem = truckItem;
    }

    public override NodeState Evaluate()
    {
        if (m_truckItem.m_currentCooldownSpawnTime > 0)
        {
            m_nodeState = NodeState.FAILURE;
        }
        else
        {
            m_nodeState = NodeState.SUCCESS;
        }
        return m_nodeState;
    }
}
