using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Net;
using System.Net.Sockets;

public class XRNetworkManager : NetworkManager
{
    Dictionary<NetworkConnection, string> clientScenes = new Dictionary<NetworkConnection, string>();
    private int randomPort;

    public override void Start()
    {
       /* randomPort = Random.Range(7778, 8000);
        Debug.Log(">>>>>>>> RandomPort : " + randomPort); //일단 각 플레이어마다 다른 값이 저장된다는 가정하에..*/

        string ipAddress = networkAddress;
        Debug.Log("========1) IP Address: " + ipAddress);
    }

    public struct CreateCharacterMessage: NetworkMessage
    {
        
    }

    public override void OnStartServer()
    {
        Debug.Log("OnStartServer()");
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreateCharacterMessage>(CreateCharacter);
    }

    public override void OnClientConnect()
    {
        Debug.Log("OnClientConnect()");

        string ipAddress = networkAddress;
        Debug.Log("========2) IP Address: " + ipAddress);

        var host = Dns.GetHostEntry(Dns.GetHostName());

        //--
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Debug.Log(ip.ToString());
            }
        }
        //--

        base.OnClientConnect();

        CreateCharacterMessage createCharacterMsg = new CreateCharacterMessage
        {

        };

        NetworkClient.Send(createCharacterMsg);
    }

    void CreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
    {
        Debug.Log("CreateCharacter()");
    }


    #region
    /// ==========씬 변경 각 서버 생성==========

    //클라이너트가 플레이어 추가를 요청할 때 호출됨
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("OnServerAddPlayer");
        base.OnServerAddPlayer(conn);

        // Assign a default scene for the client, if needed
        clientScenes[conn] = "DefaultScene";

        
    }

    //클라이언트 연결이 끊어지면 서버에 호출 됨.
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        // Remove client from dictionary when they disconnect
        clientScenes.Remove(conn);
    }

    public void ChangeClient(NetworkConnection conn, string newScene)
    {
        if (clientScenes.ContainsKey(conn))
        {
            clientScenes[conn] = newScene;
            //NetworkServer.SendToClient(conn.connectionId, new SceneMessage { sceneName = newScene, sceneOperation = SceneOperation.LoadAdditive });
            
        }
    }

    public void CreateRoomForClient(NetworkConnection conn, string sceneName)
    {
        //
    }
    

    private void ReStartAndPort()
    {
        //NetworkManager.singleton.networkAddress
    }
    #endregion
}
