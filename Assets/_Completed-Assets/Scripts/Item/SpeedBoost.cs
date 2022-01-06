using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float m_SpeedBoostAmount = 2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            other.GetComponent<Complete.TankMovement>().OnTakeSpeedBoost(m_SpeedBoostAmount);
            Destroy(gameObject);
        }
    }
}
