using UnityEngine;

[RequireComponent(typeof(AIFindingVelocity))]   // use for move state
public class StateMachine : MonoBehaviour
{
    // [SerializeField] State m_StartState;
    State m_currentState;

    public void Awake()
    {
        m_currentState = null;
    }

    public void Start()
    {
        if (m_currentState == null)
        {
            TransitionToState(new Idle(this));
        }
    }

    public void TransitionToState(State state)
    {
        m_currentState = state;
        m_currentState.EnterState();
    }

    public void Update()
    {
        if (m_currentState != null)
        {
            m_currentState.UpdateState();
        }
    }

    public void idle()
    {
        m_currentState.idle();
    }

    public void move()
    {
        m_currentState.move();
    }
}