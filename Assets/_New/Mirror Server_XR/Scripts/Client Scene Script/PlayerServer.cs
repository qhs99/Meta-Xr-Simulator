using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerServer : NetworkBehaviour
{
    XRNetworkManager netManager;

    private void Awake()
    {
        netManager = GameObject.Find("XR Network Manager").GetComponent<XRNetworkManager>();
        Debug.Log(netManager.name);
    }
}
