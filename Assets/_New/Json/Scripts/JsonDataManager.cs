using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class JsonDataManager : MonoBehaviour
{
    /// <summary>
    /// �ǽð� ������ ������ ���� �Ŵ��� ��ũ��Ʈ
    /// </summary>

    [Header("File Storage Config")]
    [SerializeField] private string fileName;       //JsonGameData ���������̸�
    [SerializeField] private string localFileName;  //JdonLocalGameData ���������̸�

    [Header("API Handler")]
    [SerializeField] private GameObject jsonApiHandler;

    private JsonGameData jsonGameData;
    private JsonLocalDataList localDataList;

    private List<DBDataPersitence> DbDataPersistenceObjects;
    private List<IDataPersistence<JsonLocalDataList>> localDataPersistenceObjects = new List<IDataPersistence<JsonLocalDataList>>();

    private JsonFileDataHandler dataHandler;
    private JsonApiHandler apiHandler;

    public static JsonDataManager instance { get; private set; }

    private void Awake()
    {
        //singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.dataHandler = new JsonFileDataHandler(Application.persistentDataPath, fileName, localFileName);

        apiHandler = jsonApiHandler.GetComponent<JsonApiHandler>();

        this.localDataPersistenceObjects = FindAllLocalDataPersistenceObjects();
        this.DbDataPersistenceObjects = FindAllDbDataPersistenceObjects();

        LoadData();
        LoadLocalData();
    }

    #region
    /// <summary>
    /// ���ÿ��� �����ϴ� ������ ex)��ŷ �ý���
    /// </summary>

    public void NewData()
    {
        this.jsonGameData = new JsonGameData();
    }

    public void LoadData()
    {
        this.jsonGameData = dataHandler.Load();

        if (this.jsonGameData == null)
        {
            Debug.Log("Data X");
            NewData();
        }
        
        foreach(DBDataPersitence dataPersistenceObj in DbDataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(jsonGameData);
        }
        Debug.Log("mngId: " + jsonGameData.mngId);
        Debug.Log("col1: " + jsonGameData.col1);
        Debug.Log("col2: " + jsonGameData.col2);
    }

    //Local Btn -> On Click()
    public void SaveData()
    {
        foreach (DBDataPersitence dataPersistenceObj in DbDataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref jsonGameData);
        }
        dataHandler.Save(jsonGameData);
    }

    public void SaveDBData(Action onDataSaved)
    {
        foreach (DBDataPersitence dataPersistenceObj in DbDataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref jsonGameData);
        }

        dataHandler.Save(jsonGameData);

        //������ ���� �� �ݹ� ����
        onDataSaved?.Invoke();
    }

    //Server Btn -> On Click()
    public void SaveDB()
    {
        SaveDBData(() =>
        {
            Debug.Log("SaveDB ����");
            
            apiHandler.DBSave(Path.Combine(Application.persistentDataPath, fileName));
            dataHandler.SaveLocalData(localDataList, jsonGameData);
        });
    }

    private List<DBDataPersitence> FindAllDbDataPersistenceObjects()
    {
        //system.linq �� ����ϰ� �ֱ� ������ i ������ ���Ӽ� �������̽��� �����ϴ� ��� ��ũ��Ʈ�� ã�� �� �ִ�.
        IEnumerable<DBDataPersitence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<DBDataPersitence>();

        //�� ��� ��ȯ�ϰ� �ش� ȣ���� ��� ����
        return new List<DBDataPersitence>(dataPersistenceObjects);
    }

    #endregion



    #region
    /// <summary>
    /// ���ÿ��� �����ϴ� ������ ex)��ŷ �ý���
    /// </summary>

    public void NewLocalData()
    {
        localDataList = new JsonLocalDataList();
    }

    public void LoadLocalData()
    {
        this.localDataList = dataHandler.LoadLocalData();

        if (localDataList == null)
        {
            Debug.Log("Local Data X");
            NewLocalData();
        }

        //�� �ڵ尡 ���ԵǾ� ���� ��, localDataPersistenceObjects ����Ʈ�� �ִ� ��� ��ü�鿡 ���� LoadData(localDataList) �޼��尡 ȣ��
        foreach (var dataPersistenceObj in localDataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(localDataList);
        }

        Debug.Log("localDataLis Count: "+localDataList.jsonLocalDataList.Count);
    }

    //Local Btn -> On Click()
    public void SaveLocalData()
    {
        foreach (var dataPersistenceObj in localDataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref localDataList);
        }

        dataHandler.SaveLocalData(localDataList, jsonGameData);
    }

    //Local Reset Data -> On Click()
    public void ResetLocalData()
    {
        foreach (var dataPersistenceObj in localDataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref localDataList);
        }

        dataHandler.ResetLocalData(localDataList);
    }

    private List<IDataPersistence<JsonLocalDataList>> FindAllLocalDataPersistenceObjects()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence<JsonLocalDataList>>().ToList();
    }

    #endregion
}
