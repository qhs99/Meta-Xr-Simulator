using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;

public class NetworkManagerObserverTest : NetworkManager
{
    [Header("Observer / Player")]
    public GameObject localPlayerPrefab;
    public GameObject remotePlayerPrefab;

    [Header("Auto Sapwn Player")]
    public bool SpawnAsPlayer = true;

    private int randomPort;

    //������ �����̰ų� ��Ʈ���� �Ǵ� ������Ʈ�� �����ϰ� ����Ʈ Ŭ���̾�Ʈ�� ī�޶�θ� ����
    //�ϴ� �� �޼���δ� �� ��
    /*public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("Adding player for connection: " + conn.connectionId);
        Debug.Log("1");

        //�̰Ÿ� ��� �� ���� �ְڴ�
        //ó�� �κ���� ������ �÷��̾���� Ȱ���� �ϴ°� �ƴ�, ī�޶�� ��û�� �ϸ� �Ǵ� �뵵�ϱ�!
        
        Debug.Log("2");

        GameObject player = Instantiate(playerPref, Vector3.zero, Quaternion.identity);
        Debug.Log("3");

        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        Debug.Log("4");

        NetworkServer.AddPlayerForConnection(conn, player);
        Debug.Log("5");
    }*/

    public struct CreatePlayerMessage : NetworkMessage
    {
        public string playerName;
    }

    public override void OnStartServer()
    {
        Debug.Log("OnStartServer()");
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect()
    {
        Debug.Log("OnClientConnect()");
        base.OnClientConnect();

        if (SpawnAsPlayer)
        {
            CreatePlayerMessage playerMessage = new CreatePlayerMessage
            {
                playerName = DefaultVariable.playerName
            };

            NetworkClient.Send(playerMessage);
        }
    }

    void OnCreatePlayer(NetworkConnectionToClient conn, CreatePlayerMessage message)
    {
        Debug.Log("OnCreatePlayer()");

        if (message.playerName == "")
        {
            Debug.Log("Player connectionId: " + conn.connectionId);
            message.playerName = "Player" + conn.connectionId;
        }
        
        GameObject playerPref = conn.connectionId == 0 ? localPlayerPrefab : remotePlayerPrefab;
        GameObject player = Instantiate(playerPref, Vector3.zero, Quaternion.identity);
        player.name = message.playerName;

        /*PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        playerInfo.playerName = message.playerName;*/
        
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    //��Ʈ ����
    public void PlayerSceneChange()
    {
        if (NetworkClient.isConnected)
        {
            NetworkClient.Disconnect();
        }

        randomPort = Random.Range(7778, 8000);

        
    }
}