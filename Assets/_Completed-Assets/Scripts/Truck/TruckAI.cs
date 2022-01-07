using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAI : MonoBehaviour
{
    public AIFindingVelocity m_findingVelocity;
    public TruckAIMovement m_truckAIMovement;
    public TruckItem m_truckItem;
    protected Transform m_randomPoint1;
    protected Transform m_randomPoint2;

    protected Node m_behaviorTree;
    // Start is called before the first frame update
    void Start()
    {
        m_randomPoint1 = GameObject.FindGameObjectWithTag("RandomPoint1").transform;
        m_randomPoint2 = GameObject.FindGameObjectWithTag("RandomPoint2").transform;
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        CoolDownNode coolDownNode = new CoolDownNode(m_truckItem);
        SpawnNode spawnNode = new SpawnNode(m_truckItem);
        MoveNode moveNode = new MoveNode(m_findingVelocity, m_truckAIMovement, m_randomPoint1, m_randomPoint2);

        SequenceNode spawn = new SequenceNode(new List<Node> { coolDownNode, spawnNode });

        m_behaviorTree = new SelectorNode(new List<Node> { spawn, moveNode });
    }

    // Update is called once per frame
    void Update()
    {
        m_truckAIMovement.Stop();
        m_behaviorTree.Evaluate();
    }

    public void Reset()
    {
        m_truckAIMovement.Stop();
        m_truckItem.Reset();
    }
}
