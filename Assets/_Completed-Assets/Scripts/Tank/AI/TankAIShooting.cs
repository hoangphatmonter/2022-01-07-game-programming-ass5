using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIShooting : Complete.TankShooting
{
    public enum ShootState
    {
        IDLE,
        HOLD,
        SHOOT,
        SHOT
    }
    public ShootState m_shootState;
    private bool m_isPressHoldFirstTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        m_shootState = ShootState.IDLE;
        m_isPressHoldFirstTime = false;
    }

    private void Update()
    {
        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();

            m_shootState = ShootState.SHOT;
            m_isPressHoldFirstTime = false;
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (m_shootState == ShootState.HOLD && !m_isPressHoldFirstTime)
        {
            m_isPressHoldFirstTime = true;
            // ... reset the fired flag and reset the launch force.
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            // Change the clip to the charging clip and start it playing.
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (m_shootState == ShootState.HOLD && !m_Fired)
        {
            // Increment the launch force and update the slider.
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (m_shootState == ShootState.SHOOT && !m_Fired)
        {
            // ... launch the shell.
            Debug.Log("lau");
            Fire();

            //? reset shoot state
            m_shootState = ShootState.SHOT;
            m_isPressHoldFirstTime = false;
        }
    }
}
