using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerTank : Complete.GameManager
{
    public struct StartGameMessage : NetworkMessage
    {
        public bool isStart;
    }
    public struct PlayerMessage : NetworkMessage
    {
        public List<NetworkIdentity> playerID;
    }
    protected int m_numOfConnectedPlayers = 0;
    protected List<NetworkIdentity> m_listIdentifyPlayer = new List<NetworkIdentity>();
    protected bool m_client_isStartGame = false;
    public override void Start()
    {
#if UNITY_SERVER
        if (autoStartServerBuild)
        {
            StartServer();
        }
#endif
        if (!NetworkClient.isHostClient)
        {
            NetworkClient.RegisterHandler<PlayerMessage>(OnPlayer);
            NetworkClient.RegisterHandler<StartGameMessage>(OnStartGame);
            // NetworkClient.Connect("localhost");
        }

        StartCoroutine(NetworkWait());
    }

    protected IEnumerator NetworkWait()
    {
        // Clear the text from the screen.
        m_MessageText.text = "Waiting For Second Player";

        // While there is not one tank left...
        while (!IsHaveEnoughPlayer() || !(m_numOfConnectedPlayers == m_Tanks.Length))
        {
            // ... return on the next frame.
            yield return null;

            if (m_client_isStartGame) break;
        }

        if (NetworkClient.isHostClient)
        {
            SendIdentify(m_listIdentifyPlayer);
            yield return new WaitForSeconds(2f);    //TODO: add synchronous
            SendStartGame(true);
        }

        // Create the delays so they only have to be made once.
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SetCameraTargets();

        // Once the tanks have been created and the camera is using them as targets, start the game.
        StartCoroutine(GameLoop());
    }

    protected bool IsHaveEnoughPlayer()
    {
        if (NetworkClient.isHostClient)
            return NetworkServer.connections.Count >= m_Tanks.Length;
        else return true;   // true for client
    }

    // [Server]
    // protected override void SpawnAllTanks()
    // {
    //     //TODO: make multi game mode
    //     // For all the tanks...
    //     for (int i = 0; i < m_Tanks.Length; i++)
    //     {
    //         // ... create them, set their player number and references needed for control.

    //         m_Tanks[i].m_Instance =
    //             Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
    //         Debug.Log(m_Tanks[i].m_Instance);

    //         m_Tanks[i].m_PlayerNumber = 1;
    //         m_Tanks[i].Setup();

    //         NetworkServer.Spawn(m_Tanks[i].m_Instance);
    //     }
    // }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log("Why");
        m_Tanks[m_numOfConnectedPlayers].m_Instance =
                Instantiate(m_TankPrefab, m_Tanks[m_numOfConnectedPlayers].m_SpawnPoint.position, m_Tanks[m_numOfConnectedPlayers].m_SpawnPoint.rotation) as GameObject;

        m_Tanks[m_numOfConnectedPlayers].m_PlayerNumber = m_numOfConnectedPlayers + 1;
        m_Tanks[m_numOfConnectedPlayers].Setup();

        NetworkServer.AddPlayerForConnection(conn, m_Tanks[m_numOfConnectedPlayers].m_Instance); // conn own this object

        // NetworkServer.Spawn(m_Tanks[i].m_Instance);

        m_numOfConnectedPlayers++;

        m_listIdentifyPlayer.Add(conn.identity);
    }

    public void SendIdentify(List<NetworkIdentity> n)
    {
        PlayerMessage msg = new PlayerMessage()
        {
            playerID = n
        };

        NetworkServer.SendToAll(msg);
    }

    public void OnPlayer(PlayerMessage msg)
    {
        Debug.Log("OnPlayerMessage " + msg.playerID[0].netId + " " + msg.playerID[0].gameObject + " " + msg.playerID[1].netId + " " + msg.playerID[1].gameObject);
        m_Tanks[0].m_Instance = NetworkClient.spawned[msg.playerID[0].netId].gameObject; // msg.playerID[0].gameObject: error because this is of the server
        m_Tanks[0].m_PlayerNumber = 1;
        m_Tanks[0].Setup();
        m_Tanks[1].m_Instance = NetworkClient.spawned[msg.playerID[1].netId].gameObject;
        m_Tanks[1].m_PlayerNumber = 2;
        m_Tanks[1].Setup();
    }

    public void SendStartGame(bool b)
    {
        StartGameMessage msg = new StartGameMessage()
        {
            isStart = b
        };

        NetworkServer.SendToAll(msg);
    }

    public void OnStartGame(StartGameMessage msg)
    {
        Debug.Log("OnStartGame " + msg.isStart);
        if (msg.isStart)
        {
            m_client_isStartGame = true;
        }
    }
}
