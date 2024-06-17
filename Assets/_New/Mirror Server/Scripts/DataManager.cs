using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


public class GameData
{
    public int time = 55;
    public int mistakeCount = 1234;

    public string grabObj = "Spanner";
    public string gribObj = "Rag";
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameData gameData = new GameData();
    string defaultUrl = "https://www.thinkingseed.co.kr/saveData.do";

    private void Awake()
    {
        //싱글톤
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PostRequest1()
    {
        StartCoroutine(LoadData(defaultUrl));
    }

    IEnumerator LoadData(string url)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("vcmIdx", "vcm24051330331kOEwH");
        values.Add("mngId", "관리자승인아이디");
        values.Add("vumIdx", "사용자승인아이디");
/*
        values.Add("col1", gameData.mistakeCount.ToString());
        values.Add("col2", gameData.mistakeCount.ToString());
*/
        values.Add("col1", gameData.grabObj.ToString());
        values.Add("col2", gameData.grabObj.ToString());
        //values.Add("col3", "col3값");

        string jsonPayload = JsonConvert.SerializeObject(values);   //json데이터를 넘기면 됩니다.
                                                                    //데이터예시
        Debug.Log(jsonPayload);
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResponse = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    Dictionary<string, string> responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

                    if (responseDict.ContainsKey("status"))
                    {
                        Debug.Log("Status: " + responseDict["status"]); // 디비저장성공 ok 실패 no
                    }
                }
            }
        }
    }
}
