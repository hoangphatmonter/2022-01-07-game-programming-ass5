using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class AIBehavior : MonoBehaviour
{
    /// <summary> when out of shoot radius amount of chaseRadius, stop shooting and chase
    public float chaseRadius = 40f;
    public float m_shootRadius = 20f;
    private StateMachine m_stateMachine;
    private Complete.GameManager m_gameManager;
    public void Awake()
    {
        m_stateMachine = GetComponent<StateMachine>();
    }

    public void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Complete.GameManager>();
    }

    public void Update()
    {
        //TODO: if player in the radius, shoot, out radius follow
        float distance = float.PositiveInfinity;
        Vector3 m_playerPos = m_gameManager.m_Tanks[0].m_Instance.transform.position;
        distance = Vector3.Distance(m_playerPos, transform.position);

        // Debug.Log(distance);
        if (distance <= m_shootRadius)
        {
            m_stateMachine.shoot();
        }
        else if (distance >= chaseRadius)
        {
            m_stateMachine.move();
        }
        else
        {
            m_stateMachine.move();
        }
    }
}