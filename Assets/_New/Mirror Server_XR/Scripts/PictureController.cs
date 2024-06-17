using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PictureController : NetworkBehaviour
{
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] GameObject defaultScreen;

    [SerializeField]
    private ScrollRect scrollRect;
    //private Vector2 startPos = new Vector2(0, 0);
    private float pageSize;
    private Vector2 startPos;
    private float swipeTime = 0.1f;
    [SerializeField]
    private bool pageState = false;

    private int totalPages;
    private int currentPage = 0;

    //private int pageState = 0; //0.stop 1.left 2.right



    private void Start()
    {
        pageSize = scrollRect.GetComponent<RectTransform>().rect.width;
        //startPos = new Vector2(0, 0);
        
        totalPages = scrollRect.content.childCount; // Assuming each child is a page
        Debug.Log("totalPages: " +totalPages);
        leftBtn.onClick.AddListener(RpcLeftButton);
        rightBtn.onClick.AddListener(RpcRightButton);
        closeBtn.onClick.AddListener(RpcClose);
    }

    // Update is called once per frame
    void Update()
    {
/*
        switch (pageState)
        {
            *//*case 0:
                {
                    return;
                }
                break;*//*
            case false: //left
                {
                    if(scrollRect.content.anchoredPosition.x <= startPos.x - pageSize)
                    scrollRect.content.transform.Translate(Vector3.left * swipeTime);
                }
                break;
            case true: //right
                {
                    if(scrollRect.content.anchoredPosition.x >= startPos.x - pageSize)
                    scrollRect.content.transform.Translate(Vector3.right * swipeTime);
                }
                break;
        }
*/
    }

    [ClientRpc]
    public void RpcLeftButton()
    {
        /*pageSize -= 100;
        pageState = false;*/

        if (currentPage > 0)
        {
            Vector2 targetPos = scrollRect.content.anchoredPosition + new Vector2(pageSize, 0);
            StartCoroutine(SmoothScroll(targetPos, -1));
        }
    }

    [ClientRpc]
    public void RpcRightButton()
    {
        if (currentPage < totalPages - 1)
        {
            Vector2 targetPos = scrollRect.content.anchoredPosition + new Vector2(-pageSize, 0);
            StartCoroutine(SmoothScroll(targetPos, 1));
        }

            
    }

    [ClientRpc]
    public void RpcClose()
    {
        defaultScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator SmoothScroll(Vector2 targetPos, int direction)
    {
        Vector2 startPos = scrollRect.content.anchoredPosition;
        float elapsedTime = 0;

        while (elapsedTime < swipeTime)
        {
            scrollRect.content.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsedTime / swipeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scrollRect.content.anchoredPosition = targetPos;

        // Update current page
        currentPage += direction;
        Debug.Log(">>>>>>>>> currentPage:"+currentPage);
        //currentPage = Mathf.Clamp(currentPage, 0, totalPages - 1); // Ensure currentPage stays within valid range

        /*if (direction > 0) // Right
        {
            if (scrollRect.content.anchoredPosition.x >= pageSize * (totalPages - 1))
            {
                scrollRect.content.anchoredPosition = new Vector2(0, scrollRect.content.anchoredPosition.y);
            }
        }
        else if (direction < 0) // Left
        {
            if (scrollRect.content.anchoredPosition.x <= -pageSize * (totalPages - 1))
            {
                scrollRect.content.anchoredPosition = new Vector2(0, scrollRect.content.anchoredPosition.y);
            }
        }*/
    }
}
