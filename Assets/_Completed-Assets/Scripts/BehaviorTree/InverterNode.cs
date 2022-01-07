using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    protected Node m_node;
    public InverterNode(Node node)
    {
        m_node = node;
    }
    public override NodeState Evaluate()
    {
        switch (m_node.Evaluate())
        {
            case NodeState.FAILURE:
                m_nodeState = NodeState.SUCCESS;
                break;
            case NodeState.RUNNING:
                m_nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                m_nodeState = NodeState.FAILURE;
                break;
            default:
                break;
        }
        return m_nodeState;
    }
}
