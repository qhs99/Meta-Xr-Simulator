using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebSocketTest2 : MonoBehaviour
{
    WebSocket ws;

    void Start()
    {
        //�ּ� �־���� ��!!!
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
        //Debug.Log("�ּ�: " + ((WebSocket)sender).Url + ", ������: " + e.Data);
    }
}
