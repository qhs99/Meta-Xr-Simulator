using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Transports;
using kcp2k;

public class CustomNetworkManager : NetworkManager
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("Server started on port: " + networkAddress);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        Debug.Log("New player connected: " + conn.identity);
    }

    public void JoinSpecificRoom(string roomName)
    {
        networkAddress = "localhost";
        StartClient();

        NetworkClient.ConnectHost(); //서버에 연결
        
    }
}
