using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoHostClient : MonoBehaviour
{
    [SerializeField] NetworkManager networkManager;

    private void Start()
    {
        //ȣ���õ� �� ������ ������ ��� �ٷ� ȣ���õǾ� �ִ� ������ �̵���
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
