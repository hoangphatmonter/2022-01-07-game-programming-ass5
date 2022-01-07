using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    protected List<Node> m_nodes = new List<Node>();
    public SelectorNode(List<Node> nodes)
    {
        m_nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    break;
                case NodeState.RUNNING:
                    m_nodeState = NodeState.RUNNING;
                    return m_nodeState;
                case NodeState.SUCCESS:
                    m_nodeState = NodeState.SUCCESS;
                    return m_nodeState;
                default:
                    break;
            }
        }
        m_nodeState = NodeState.FAILURE;
        return m_nodeState;
    }
}
