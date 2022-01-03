using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class AIBehavior : MonoBehaviour
{
    private StateMachine m_stateMachine;
    public void Awake()
    {
        m_stateMachine = GetComponent<StateMachine>();
    }

    public void Start() { }

    public void Update()
    {
        //TODO: if player in the radius, shoot, out radius follow
        if (true)
        {
            m_stateMachine.move();
        }
    }
}