using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBDataPersitence : MonoBehaviour, IDataPersistence<JsonGameData>
{
    [SerializeField] private TMP_InputField countTxt;
    [SerializeField] private TMP_InputField nameTxt;
    private string _count;
    private string _name;

    private JsonGameData currentData;

    public void LoadData(JsonGameData data)
    {
        Debug.Log(">>>>>>>> DBDataPersistence -> LoadData ½ÇÇà <<<<<<<<<");
    }

    public void SaveData(ref JsonGameData data)
    {
        data.col1 = _count;
        data.col2 = _name;
    }

    public void SetData()
    {
        _count = countTxt.text;
        _name = nameTxt.text;
    }
}
