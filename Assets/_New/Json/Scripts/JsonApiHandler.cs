using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using System;

public class JsonApiHandler : MonoBehaviour
{
    /// <summary>
    /// DB�� ������ ����
    /// </summary>

    //���� api url
    public string dataApiUrl = "";

    private string jsonData = "";

    public void DBSave(string filePath)
    {
        DataLoad(filePath);
    }

    public void DataLoad(string filePath)
    {
        string dataToload = null;
        if (File.Exists(filePath))
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //������ ��� ������ ���ڿ��� ����
                        dataToload = reader.ReadToEnd();
                    }
                }         
                jsonData = dataToload;
                Debug.Log("===1    " + jsonData);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data to file: " + filePath + "\n" + e);
            }
            StartCoroutine(DataSave());
        }
    }

    IEnumerator DataSave()
    {
        using (UnityWebRequest www = new UnityWebRequest(dataApiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // 1)���⼭ �����͸� ������
            yield return www.SendWebRequest();

            // 2)java���� �˻� ���� �� ����� ���� �Ѿ���� �˻�
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
                        Debug.Log("Status: " + responseDict["status"]); // ������强�� ok ���� no
                    }
                }
            }
        }
    }
}
