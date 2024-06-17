using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.UI;

public class SceneController : NetworkBehaviour
{
    [SerializeField] Button sceneBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] GameObject defaultScreen;

    private void Start()
    {
        sceneBtn.onClick.AddListener(SceneChangeButton);
        closeBtn.onClick.AddListener(RpcClose);
    }

    public void SceneChangeButton()
    {

        if(NetworkClient.active && !NetworkServer.active!)
        {
            CmdRequestSceneChange();
            
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdRequestSceneChange(NetworkConnectionToClient sender = null)
    {
        Debug.Log("CmdRequestSceneChange");
        TargetChangeScene(sender.identity.connectionToClient, "VR_Client Scene");
    }

    [TargetRpc] //targetRPC는 networkmanager를 상속받으데는 쓸 수 없음..
    private void TargetChangeScene(NetworkConnection target, string sceneName)
    {
        Debug.Log("TargetChangeScene");
        ClientChangeScene(sceneName);
    }

    private void ClientChangeScene(string sceneName)
    {
        Debug.Log("ClientChangeScene");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    

    [ClientRpc]
    public void RpcClose()
    {
        defaultScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
