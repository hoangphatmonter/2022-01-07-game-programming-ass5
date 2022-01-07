using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    protected List<Node> m_nodes = new List<Node>();
    public SequenceNode(List<Node> nodes)
    {
        m_nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    m_nodeState = NodeState.FAILURE;
                    return m_nodeState;
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                default:
                    break;
            }
        }
        m_nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return m_nodeState;
    }
}
