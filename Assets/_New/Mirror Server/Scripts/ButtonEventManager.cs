using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ButtonEventManager : NetworkBehaviour
{
    [SerializeField] CustomNetwork customNetwork;
    [SerializeField] GameObject spawnObj;

    [Header("Scene Setting")]
    public GameObject sceneManager;
    public GameObject cameraObj;

    public void SettingStartScene()
    {
        sceneManager.SetActive(true);
        cameraObj.SetActive(true);
    }

    public void SettingCloseScene()
    {
        sceneManager.SetActive(false);
        cameraObj.SetActive(false);
    }

    private void Start()
    {
        //서버 초기화 코드
        base.OnStartServer();
    }

    public void ChildrenActivateEvent()
    {
        customNetwork.ChildenActivate();
    }

    public void SomeServerEvent()
    {
        if (isServer)
        {
            customNetwork.UpdateGameState("NewRoundStarted");
        }
    }

    //remote player만 제어
    public void ColorChangeEvent()
    {
        if (isServer)
        {
            customNetwork.RandomColorCreate();
            foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
            {
                if (!conn.identity.isOwned)
                {
                    Debug.Log("플레이어 오브젝트 이름: " + conn.identity.gameObject.name);
                    customNetwork.ColorChange(conn.identity.gameObject);
                }    
            }
        }
    }

    public void SceneChangeEvent(int i)
    {
        if (isServer)
        {
            customNetwork.SceneChange(i);
        }
    }

    public void BoxCreateEvent()
    {
        if (isServer)
        {
            customNetwork.BoxCreate(spawnObj);
        }
    }


    /*public void SendMessageToAllClients(string message)
    {
        foreach (NetworkConnection conn in NetworkServer.connections.Values)
        {
            CustomNetwork player = conn.identity.GetComponent<CustomNetwork>();
            player.RpcReceiveMessage(message);
        }
    }*/
}
