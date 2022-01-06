using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Turn to the right direction and then shoot. </summary>
public class Shoot : State
{
    protected TankAIMovement m_tankAIMovement;
    protected TankAIShooting m_tankAIShooting;
    protected GameObject m_opponent;

    private float m_maxTimeToShoot; // depend the tank shoot speed, may prevent the tank shoot if too low
    private float m_currentTimeToShoot;
    public Shoot(StateMachine stateMachine) : base(stateMachine)
    {
        m_tankAIMovement = m_StateMachine.GetComponent<TankAIMovement>();
        Debug.AssertFormat(m_tankAIMovement, "TankAIMovement is not found");

        m_tankAIShooting = m_StateMachine.GetComponent<TankAIShooting>();
        Debug.AssertFormat(m_tankAIShooting, "TankAIShooting is not found");

        m_opponent = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Complete.GameManager>().m_Tanks[0].m_Instance;
        Debug.AssertFormat(m_opponent, "opponent is not found");

        m_maxTimeToShoot = m_tankAIShooting.m_MaxChargeTime / 1.5f;
    }

    public override bool EnterState()
    {
        base.EnterState();

        //TODO: make agent find path and go automatically
        Debug.Log("Entering Shoot state");

        m_currentTimeToShoot = Random.Range(m_maxTimeToShoot / 2, m_maxTimeToShoot);
        m_tankAIShooting.m_shootState = TankAIShooting.ShootState.IDLE;

        return true;
    }

    public override void UpdateState()
    {
        Vector3 directionToShoot = m_opponent.transform.position - m_tankAIMovement.transform.position;
        directionToShoot.y = 0;

        float angle = Vector3.Angle(m_tankAIMovement.transform.forward, directionToShoot);
        Vector3 cross = Vector3.Cross(m_tankAIMovement.transform.forward, directionToShoot);
        if (cross.y > 0)
        {
            angle = -angle;
        }

        //? turn or shoot
        if (angle > 10)
        {
            m_tankAIMovement.TurnLeft();
        }
        else if (angle < -10)
        {
            m_tankAIMovement.TurnRight();
        }

        if (m_tankAIShooting.m_shootState == TankAIShooting.ShootState.IDLE)
        {
            m_tankAIShooting.m_shootState = TankAIShooting.ShootState.HOLD;

        }

        if (m_tankAIShooting.m_shootState == TankAIShooting.ShootState.HOLD)
        {
            //TODO: shoot, calculate how long for holding fire
            m_currentTimeToShoot -= Time.deltaTime;
            if (m_currentTimeToShoot <= 0)
            {
                m_tankAIShooting.m_shootState = TankAIShooting.ShootState.SHOOT;
                m_currentTimeToShoot = m_maxTimeToShoot;
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting Shoot state");
        m_tankAIShooting.m_shootState = TankAIShooting.ShootState.IDLE;

        return true;
    }

    public override void idle()
    {
        //
    }

    public override void move()
    {
        ExitState();

        m_StateMachine.TransitionToState(new Move(m_StateMachine));
    }

    public override void shoot()
    {
        if (m_tankAIShooting.m_shootState == TankAIShooting.ShootState.SHOT)
        {
            EnterState();
        }
    }
}
