using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebSocketTest2 : MonoBehaviour
{
    WebSocket ws;

    void Start()
    {
        //林家 持绢拎具 凳!!!
        ws = new WebSocket("");

        ws.Connect();
        //ws.OnMessage += Call;
    }

    void Update()
    {
        if(ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("abcd");
        }
    }

    void Call(Object sender, MessageEventArgs e)
    {
        //Debug.Log("林家: " + ((WebSocket)sender).Url + ", 单捞磐: " + e.Data);
    }
}
