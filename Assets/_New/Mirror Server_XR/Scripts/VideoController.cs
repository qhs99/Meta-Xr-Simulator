using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Mirror;

public class VideoController : NetworkBehaviour
{
    [SerializeField] Slider videoSlider;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioSource videoAudioSource;
    [SerializeField] Button playBtn;
    [SerializeField] Button stopBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] GameObject defaultScreen;

    private void Start()
    {
        playBtn.onClick.AddListener(RpcVideoPlay);
        stopBtn.onClick.AddListener(RpcVideoStop);
        closeBtn.onClick.AddListener(RpcClose);
    }

    [ClientRpc]
    public void RpcVideoPlay()
    {
        videoPlayer.Pause();
        playBtn.gameObject.SetActive(false);
        stopBtn.gameObject.SetActive(true);
    }

    [ClientRpc]
    public void RpcVideoStop()
    {
        videoPlayer.Prepare();
        videoPlayer.Play();
        stopBtn.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(true);
    }

    [ClientRpc]
    public void RpcClose()
    {
        defaultScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
