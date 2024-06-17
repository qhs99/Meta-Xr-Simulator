using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

using TMPro;
using System.Net;
using System.Net.Sockets;

public class XROnlineCanvasCon :NetworkBehaviour
{
    private bool activeState = false;

    [Header("Canvas")]
    [SerializeField] GameObject offlineCanvas;
    [SerializeField] GameObject serverCanvas;
    [SerializeField] GameObject clientCanvas;

    [Header ("Button")] //-->리스트로 받아와도 될 듯?
    [SerializeField] Button pictureBtn;
    [SerializeField] Button videoBtn;
    [SerializeField] Button animBtn;
    [SerializeField] Button sceneBtn;

    List<GameObject> clientChild;
  
    //Client Child
    /// <summary>
    /// 0. default Screen
    /// 1. picture
    /// 2. video
    /// 3. animation
    /// 4. scene
    /// </summary>

    private void Awake()
    {
        clientChild = new List<GameObject>();

        Transform clientTransform = clientCanvas.transform;

        for (int i = 0; i < clientTransform.childCount; i++)
        {
            Transform childTransform = clientTransform.GetChild(i);
            clientChild.Add(childTransform.gameObject);
        }
    }

    private void Start()
    {
        pictureBtn.onClick.AddListener(PictureButton);
        videoBtn.onClick.AddListener(RpcVideoButton);
        animBtn.onClick.AddListener(AnimButton);
        sceneBtn.onClick.AddListener(SceneButton);

        //test

        var host = Dns.GetHostEntry(Dns.GetHostName());

        //현재 연결된 ip 주소
        this.transform.GetChild(3).GetComponent<TMP_Text>().text = NetworkManager.singleton.networkAddress;

    }


    /*public void DefaultScreenButton()
    {

    }*/

    [ClientRpc]
    public void PictureButton()
    {
        clientChild[0].SetActive(false);
        clientChild[1].SetActive(true);
    }

    [ClientRpc]
    public void RpcVideoButton()
    {
        clientChild[0].SetActive(false);
        clientChild[2].SetActive(true);
    }

    [ClientRpc]
    public void AnimButton()
    {
        clientChild[0].SetActive(false);
        clientChild[3].SetActive(true);
    }

    [ClientRpc]
    public void SceneButton()
    {
        clientChild[0].SetActive(false);
        clientChild[4].SetActive(true);
    }

    [ClientRpc]
    public void CmdDeactivate()
    {
        for (int i = 0; i < clientChild.Count; i++)
        {
            clientChild[i].SetActive(false);
        }
    }

    public void CmdButtonNonClick()
    {
        for (int i = 0; i < clientChild.Count; i++)
        {
            clientChild[i].GetComponent<Button>().interactable = false;
        }
    }
}
