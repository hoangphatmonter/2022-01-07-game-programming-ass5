using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : Node
{
    public override NodeState Evaluate()
    {
        m_nodeState = NodeState.RUNNING;
        return m_nodeState;
    }
}
