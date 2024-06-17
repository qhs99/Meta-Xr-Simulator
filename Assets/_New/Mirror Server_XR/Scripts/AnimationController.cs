using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class AnimationController : NetworkBehaviour
{
    [SerializeField] GameObject Cylinder;
    private Animator anim;

    [Header("Server Control Button")]
    public Button playBtn;
    public Button pauseBtn;
    public Button closeBtn;
    public Button restartBtn;
    public GameObject defaultScreen;


    // Start is called before the first frame update
    void Start()
    {
        anim = Cylinder.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playBtn.onClick.AddListener(RpcAnimPlay);
        pauseBtn.onClick.AddListener(RpcAnimPause);
        restartBtn.onClick.AddListener(RpcAnimRestart);
        closeBtn.onClick.AddListener(RpcClose);
    }

    [ClientRpc]
    public void RpcAnimPlay()
    {
        anim.speed = 1.0f;
    }

    [ClientRpc]
    public void RpcAnimPause()
    {
        anim.speed = 0.0f;
    }

    [ClientRpc]
    public void RpcAnimRestart()
    {
        anim.Rebind();
        anim.speed = 1.0f;
    }

    [ClientRpc]
    public void RpcClose()
    {
        defaultScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
