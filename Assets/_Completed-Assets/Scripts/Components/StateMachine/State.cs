using UnityEngine;
using System.Collections;

public enum ExecutionState
{
    ACTIVE,
    COMPLETED,
    TERMINATED
}

public abstract class State
{
    public ExecutionState m_ExecutionState { get; protected set; }

    protected StateMachine m_StateMachine;

    /// <summary> init variables </summary>
    public State(StateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
    }

    /// <summary> called after assign to this state </summary>
    public virtual bool EnterState()
    {
        m_ExecutionState = ExecutionState.ACTIVE;
        return true;
    }

    public abstract void UpdateState();
    public virtual void FixedUpdateState()
    {

    }

    public virtual bool ExitState()
    {
        m_ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public abstract void idle();
    public abstract void move();
    public abstract void shoot();
}