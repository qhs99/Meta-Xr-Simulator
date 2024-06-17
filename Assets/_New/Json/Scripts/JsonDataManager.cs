using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class JsonDataManager : MonoBehaviour
{
    /// <summary>
    /// 실시간 데이터 저장을 위한 매니저 스크립트
    /// </summary>

    [Header("File Storage Config")]
    [SerializeField] private string fileName;       //JsonGameData 저장파일이름
    [SerializeField] private string localFileName;  //JdonLocalGameData 저장파일이름

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
    /// 로컬에서 관리하는 데이터 ex)랭킹 시스템
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

        //데이터 저장 후 콜백 실행
        onDataSaved?.Invoke();
    }

    //Server Btn -> On Click()
    public void SaveDB()
    {
        SaveDBData(() =>
        {
            Debug.Log("SaveDB 실행");
            
            apiHandler.DBSave(Path.Combine(Application.persistentDataPath, fileName));
            dataHandler.SaveLocalData(localDataList, jsonGameData);
        });
    }

    private List<DBDataPersitence> FindAllDbDataPersistenceObjects()
    {
        //system.linq 를 사용하고 있기 때문에 i 데이터 지속성 인터페이스를 구현하는 모든 스크립트를 찾을 수 있다.
        IEnumerable<DBDataPersitence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<DBDataPersitence>();

        //새 목록 반환하고 해당 호출의 결과 전달
        return new List<DBDataPersitence>(dataPersistenceObjects);
    }

    #endregion



    #region
    /// <summary>
    /// 로컬에서 관리하는 데이터 ex)랭킹 시스템
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

        //이 코드가 포함되어 있을 때, localDataPersistenceObjects 리스트에 있는 모든 객체들에 대해 LoadData(localDataList) 메서드가 호출
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
