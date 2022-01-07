using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node
{
    private TurretShooting m_turretShooting;

    public ShootNode(TurretShooting turretShooting)
    {
        m_turretShooting = turretShooting;
    }

    public override NodeState Evaluate()
    {
        Fire();
        // m_
        // m_turretAI.SetColor(Color.red);
        m_nodeState = NodeState.RUNNING;
        return NodeState.RUNNING;
    }

    private void Fire()
    {
        m_turretShooting.Fire();
    }
}
