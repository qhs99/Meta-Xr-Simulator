using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonLocalGameData
{
    public string id;
    public string time;

    public JsonLocalGameData(string id, string time)
    {
        this.id = id;
        this.time = time;
    }
}

[System.Serializable]
public class JsonLocalDataList
{
    public List<JsonLocalGameData> jsonLocalDataList = new List<JsonLocalGameData>();
}
