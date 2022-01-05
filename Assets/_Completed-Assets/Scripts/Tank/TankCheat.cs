using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TankCheat : NetworkBehaviour
{
    protected string m_cheatButton = "Cheat";
    protected Complete.TankHealth m_TankHealth;   // Reference to this tank's health.

    private void Awake()
    {
        m_TankHealth = GetComponent<Complete.TankHealth>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkClient.isHostClient && Input.GetButtonDown(m_cheatButton))
        {  // can be cheat on host
            SetInvisible();
        }
    }

    [Command]
    private void SetInvisible()
    {
        RPCSetInvisible();
    }

    [ClientRpc]
    private void RPCSetInvisible()
    {
        m_TankHealth.SetInvisible();
    }
}
