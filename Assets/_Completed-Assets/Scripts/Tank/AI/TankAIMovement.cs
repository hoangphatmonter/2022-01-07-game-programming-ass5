using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIMovement : Complete.TankMovement
{
    private void Update()
    {
        // Store the value of both input axes.
        // m_MovementInputValue = 0;//Input.GetAxis(m_MovementAxisName);
        // m_TurnInputValue = 0;//Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    }

    public void TurnRight()
    {
        m_MovementInputValue = 0;
        m_TurnInputValue = 1;
    }

    public void TurnLeft()
    {
        m_MovementInputValue = 0;
        m_TurnInputValue = -1;
    }

    public void TurnMoveRight()
    {
        m_MovementInputValue = 0.2f * m_originalSpeed / m_currentSpeed;
        m_TurnInputValue = 1;
    }
    public void TurnMoveLeft()
    {
        m_MovementInputValue = 0.2f * m_originalSpeed / m_currentSpeed;
        m_TurnInputValue = -1;
    }

    public void MoveForward()
    {
        m_TurnInputValue = 0;
        m_MovementInputValue = 1;
    }

    public void MoveBackward()
    {
        m_TurnInputValue = 0;
        m_MovementInputValue = -1;
    }

    public void Stop()
    {
        m_MovementInputValue = 0;
        m_TurnInputValue = 0;
    }
}
