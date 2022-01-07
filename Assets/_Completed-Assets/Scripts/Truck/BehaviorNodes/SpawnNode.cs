using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNode : Node
{
    TruckItem m_truckItem;

    public SpawnNode(TruckItem truckItem)
    {
        m_truckItem = truckItem;
    }

    public override NodeState Evaluate()
    {
        m_truckItem.SpawnItem();
        m_nodeState = NodeState.SUCCESS;
        return m_nodeState;
    }
}
