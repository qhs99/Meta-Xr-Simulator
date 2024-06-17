using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    [Range(1, 10)]
    public int speed = 1;

    private Transform cameraObj;
    public Transform cameraPos;
    private ButtonEventManager buttonEventMgr;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        if(cameraObj != null)
        {
            cameraObj.position = cameraPos.position;
            cameraObj.rotation = cameraPos.rotation;
        }

    }

    public override void OnStartAuthority()
    {
        buttonEventMgr = GameObject.FindAnyObjectByType<ButtonEventManager>();

        cameraObj = buttonEventMgr.cameraObj.transform;
    }
}   
