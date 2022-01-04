using UnityEngine;

class Idle : State
{
    public Idle(StateMachine stateMachine) : base(stateMachine)
    {
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

    public override void shoot()
    {
        //
    }
}