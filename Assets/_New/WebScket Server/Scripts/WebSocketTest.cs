using UnityEngine;
using WebSocketSharp;

public class WebSocketTest : MonoBehaviour
{
    public string url;
    string DataReceived;
    WebSocket ws;
    
    void Start()
    {
        ws = new WebSocket(url);
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            DataReceived = e.Data;
            Debug.Log(DataReceived);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.LogError("WebSocket connection error: " + e.Message);
        };
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ws.Send("Hello");
        }
    }
}
