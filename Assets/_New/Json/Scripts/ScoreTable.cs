using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using System.Linq;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;

    [SerializeField] private string localfileName;

    private List<Transform> RanktransformList;

    string fullPath;
    int listCount = 0;

    private void Awake()
    {
        fullPath = Path.Combine(Application.persistentDataPath, localfileName);
        Debug.Log(fullPath);
        RanktransformList = new List<Transform>();

        LoadDataSort();
    }

    public JsonLocalDataList FileLoad()
    {
        JsonLocalDataList loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<JsonLocalDataList>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data to file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void LoadDataSort()
    {
        JsonLocalDataList loadedData = FileLoad();
        var sortedDataList = new List<JsonLocalGameData>();

        //if (loadedData != null && loadedData.jsonLocalDataList != null)
        if (loadedData != null && loadedData.jsonLocalDataList.Count != 0)
        {
            sortedDataList = loadedData.jsonLocalDataList.OrderByDescending(data => int.Parse(data.time)).ToList();
            loadedData.jsonLocalDataList = sortedDataList;
        }
        else
        {
            for(int i = 0; i < entryTemplate.childCount; i++)
            {
                entryTemplate.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = "...";
            }
            return;
        }

        for (int i = 0; i < loadedData.jsonLocalDataList.Count; i++)
        {
            Debug.Log(loadedData.jsonLocalDataList[i]);
            CreateRankingEntry(loadedData.jsonLocalDataList[i], entryContainer, RanktransformList);
            listCount++;
        }

        entryTemplate.gameObject.SetActive(false);
    }

    public void CreateRankingEntry(JsonLocalGameData data, Transform container, List<Transform> transformList)
    {
        var height = 50;
        var min = 0;
        var sec = 0;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        if(listCount < 5)
        {
            entryRectTransform.anchoredPosition = new Vector2(0, -height * listCount);
        }
        else
        {
            entryRectTransform.anchoredPosition = new Vector2(450, -height * (listCount - 5));
        }

        //rank update
        int rank = listCount + 1;
        entryTransform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = rank.ToString();
        //name update
        string name = data.id;
        entryTransform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = name;
        //time update
        int time = int.Parse(data.time);
        min = time / 60;
        sec = time % 60;
        if (sec < 10)
        {
            entryTransform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = min.ToString() + " : 0" + sec.ToString();
        }
        else
        {
            entryTransform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = min.ToString() + " : " + sec.ToString();
        }
    }

}

