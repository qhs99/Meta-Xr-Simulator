using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Discovery;
using System;
using System.Net;

[DisallowMultipleComponent] //컴포넌트 중복 방지
public class XRNetworkDiscovery : NetworkDiscoveryBase<ServerRequest, ServerResponse>
{
    [SerializeField] XRCanvasHUD xRCanvasHUD;

    protected override ServerResponse ProcessRequest(ServerRequest request, IPEndPoint endpoint)
    {
        try
        {
            return new ServerResponse
            {
                serverId = ServerId,
                uri = transport.ServerUri()
            };
        }
        catch (NotImplementedException)
        {
            Debug.LogError("");
            throw;
        }

    }

    protected override void ProcessResponse(ServerResponse response, IPEndPoint endpoint)
    {
        throw new System.NotImplementedException();
    }
}
