using UnityEngine;

class Idle : State
{
    public Idle(StateMachine stateMachine) : base(stateMachine)
    {
        Debug.Log("hello");
    }

    public override void UpdateState()
    {
        Debug.Log("idle");
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting Move state");
        return true;
    }

    public override void idle()
    {
        //
    }

    public override void move()
    {
        m_StateMachine.TransitionToState(new Move(m_StateMachine));
    }
}