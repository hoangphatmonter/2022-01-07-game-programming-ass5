using UnityEngine;

class Move : State
{
    protected AIFindingVelocity m_findingVelocity;
    protected TankAIMovement m_tankAIMovement;
    private GameObject m_opponent;
    public Move(StateMachine stateMachine) : base(stateMachine)
    {
        m_findingVelocity = m_StateMachine.GetComponent<AIFindingVelocity>();
        Debug.AssertFormat(m_findingVelocity, "AIFindingVelocity is not found");

        m_tankAIMovement = m_StateMachine.GetComponent<TankAIMovement>();
        Debug.AssertFormat(m_tankAIMovement, "TankAIMovement is not found");

        m_opponent = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Complete.GameManager>().m_Tanks[0].m_Instance;
        Debug.AssertFormat(m_opponent, "opponent is not found");
    }

    public override bool EnterState()
    {
        base.EnterState();

        //TODO: make agent find path and go automatically
        Debug.Log("Entering Move state");

        m_findingVelocity.SetMovePosition(m_opponent.transform.position);

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
        if (directionToGo == Vector3.zero)
            m_tankAIMovement.Stop();
        else if (angle > 5)
        {
            m_tankAIMovement.TurnMoveLeft();
        }
        else if (angle < -5)
        {
            m_tankAIMovement.TurnMoveRight();
        }
        else
        {
            m_tankAIMovement.MoveForward();
        }

        //TODO: shoot ?
        // if (m_findingVelocity.reachedEndOfPath)
        //     this.EnterState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        m_findingVelocity.SetMovePosition(m_opponent.transform.position);
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting Move state");

        m_tankAIMovement.Stop();

        return true;
    }

    public override void idle()
    {
        ExitState();

        m_StateMachine.TransitionToState(new Idle(m_StateMachine));
    }

    public override void move()
    {
        //
    }

    public override void shoot()
    {
        ExitState();

        m_StateMachine.TransitionToState(new Shoot(m_StateMachine));
    }
}