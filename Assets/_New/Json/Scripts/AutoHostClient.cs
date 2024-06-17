using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoHostClient : MonoBehaviour
{
    [SerializeField] NetworkManager networkManager;

    private void Start()
    {
        //호스팅된 후 실행한 게임의 경우 바로 호스팅되어 있는 곳으로 이동됨
/*
        if (!Application.isBatchMode)
        {
            Debug.Log("=== Client Connected ===");
            networkManager.StartClient();
        }
        else
        {
            Debug.Log("=== Serer Starting ===");
        }
*/
    }

    public void JoinLocal()
    {
        networkManager.networkAddress = "localhost";
        networkManager.StartClient();
    }
}
