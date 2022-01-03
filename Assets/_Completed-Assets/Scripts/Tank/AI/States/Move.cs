using UnityEngine;

class Move : State
{
    protected AIFindingVelocity m_findingVelocity;
    protected TankAIMovement m_tankAIMovement;
    public Move(StateMachine stateMachine) : base(stateMachine)
    {
        m_findingVelocity = m_StateMachine.GetComponent<AIFindingVelocity>();
        Debug.AssertFormat(m_findingVelocity, "AIFindingVelocity is not found");

        m_tankAIMovement = m_StateMachine.GetComponent<TankAIMovement>();
        Debug.AssertFormat(m_tankAIMovement, "TankAIMovement is not found");
    }

    public override bool EnterState()
    {
        base.EnterState();

        //TODO: make agent find path and go automatically
        Debug.Log("Entering Move state");

        Collider[] hitColliders = Physics.OverlapSphere(m_findingVelocity.transform.position, 100, LayerMask.GetMask("Players"));
        foreach (var hitCollider in hitColliders)
        {
            m_findingVelocity.SetMovePosition(hitCollider.transform.position);
            return true;
        }

        return true;
    }

    public override void UpdateState()
    {
        Vector3 directionToGo = m_findingVelocity.m_currentVelocity;
        directionToGo.y = 0;

        float angle = Vector3.Angle(m_tankAIMovement.transform.forward, directionToGo);
        Vector3 cross = Vector3.Cross(m_tankAIMovement.transform.forward, directionToGo);
        if (cross.y > 0)
        {
            angle = -angle;
        }

        // if (angle < 0)
        // Debug.Log(angle);
        if (directionToGo == Vector3.zero)
            m_tankAIMovement.Stop();
        else if (angle > 5)
        {
            m_tankAIMovement.TurnLeft();
        }
        else if (angle < -5)
        {
            m_tankAIMovement.TurnRight();
        }
        else
        {
            m_tankAIMovement.MoveForward();
        }

        //TODO: shoot ?
        if (m_findingVelocity.reachedEndOfPath)
            this.EnterState();
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting Move state");
        return true;
    }

    public override void idle()
    {
        m_StateMachine.TransitionToState(new Idle(m_StateMachine));
    }

    public override void move()
    {
        //
    }
}