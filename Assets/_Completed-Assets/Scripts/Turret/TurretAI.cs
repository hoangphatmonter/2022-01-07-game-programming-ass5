using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public float m_shootingRange = 8f;
    // public Material m_material;
    // public TankAIMovement m_tankAIMovement;
    // public AIFindingVelocity m_findingVelocity;
    public TurretShooting m_turretShooting;
    // public GameObject m_shellPrefab;
    // Start is called before the first frame update

    private Node m_behaviorTree;
    void Start()
    {
        // m_material = GetComponent<Renderer>().material;
        // m_tankAIMovement = GetComponent<TankAIMovement>();

        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        IdleNode idleNode = new IdleNode();
        RangeNode rangeNode = new RangeNode(m_shootingRange, transform);
        ShootNode shootNode = new ShootNode(m_turretShooting);

        SequenceNode shoot = new SequenceNode(new List<Node> { rangeNode, shootNode });

        m_behaviorTree = new SequenceNode(new List<Node> { shoot, idleNode });
    }

    // Update is called once per frame
    void Update()
    {
        m_behaviorTree.Evaluate();

    }

    // internal void SetColor(Color color)
    // {
    //     m_material.color = color;
    // }
}
