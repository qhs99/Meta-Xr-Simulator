using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror.Discovery;
using Mirror;
using System.Net;
using System.Net.Sockets;

public class XRCanvasHUD : MonoBehaviour
{
    public XRNetworkManager netManager;
    public GameObject cameraObject;

    [Header("UI Canvas")]
    public GameObject serverCanvas;
    public GameObject clientCanvas;

    [Header("UI")]
    [SerializeField] Button serverBtn;
    [SerializeField] Button clientBtn;
    [SerializeField] TMP_InputField inputAddress;
    [SerializeField] TMP_Text textAddress;
    

    private void Start()
    {
        serverBtn.onClick.AddListener(HostButton);
        clientBtn.onClick.AddListener(ClientButton);

        inputAddress.text = NetworkManager.singleton.networkAddress;
        inputAddress.onValueChanged.AddListener(delegate { OnValueChangedAddress(); });

        var host = Dns.GetHostEntry(Dns.GetHostName());

        //--
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                textAddress.text = ip.ToString();
            }
        }
        //--


    }


    public void HostButton()
    {
        Debug.Log("HostButton()");
        netManager.StartHost();
        serverCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ServerButton()
    {
        Debug.Log("ServerButton()");
        netManager.StartServer();
        gameObject.SetActive(false);
    }

    public void ClientButton()
    {
        Debug.Log("ClientButton()");
        netManager.StartClient();
        serverCanvas.SetActive(false);
        gameObject.SetActive(false);
    }


    public void OnValueChangedAddress()
    {
        NetworkManager.singleton.networkAddress = inputAddress.text;
    }
}
