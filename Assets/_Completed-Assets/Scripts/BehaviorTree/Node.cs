using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    protected NodeState m_nodeState;
    public NodeState nodeState { get; }
    public abstract NodeState Evaluate();
}

public enum NodeState
{
    SUCCESS,
    FAILURE,
    RUNNING
}