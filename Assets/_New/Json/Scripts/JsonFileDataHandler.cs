using System;
using System.IO;
using UnityEngine;

public class JsonFileDataHandler
{
    /// <summary>
    /// 게임 데이터를 파일(Json형식)로 저장하고 로드하기 위한 기능 제공.
    /// </summary>

    // 저장경로
    private string dataDirPath = "";
    // 저장파일이름
    private string dataFileName = "";
    // 저장로컬파일이름
    private string localDataFileName = "";

    // 생성자 -> 데이터 디렉토리 경로와 파일 이름을 매개변수로 받아서 설정
    public JsonFileDataHandler(string dataDirPath, string dataFileName, string localDataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.localDataFileName = localDataFileName;
    }

    // 게임 데이터 개체 반환
    // 저장된 게임 데이터를 파일에서 읽어오는 메서드
    public JsonGameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        JsonGameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToload = "";
                // 파일 스트림을 열고 데이터를 읽음
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToload = reader.ReadToEnd();
                    }
                }
                // deserialize (Json 문자열을 JsonGameData 객체로 변환)
                loadedData = JsonUtility.FromJson<JsonGameData>(dataToload);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data to file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    // 게임 데이터 개체 가져오기
    // 게임 데이터를 파일에 저장하는 메서드
    public void Save(JsonGameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // 디렉토리가 존재하지 않으면 생성
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // serialize (Json 문자열로 변환)
            string dataToStore = JsonUtility.ToJson(data, true);
            // 파일을 읽거나 쓸 때 using 블록을 사용하면 읽기나 쓰기가 끝나면 해당 파일에 대한 연결이 닫히도록 보장.
            // 전제 경로를 전달하는 새 파일 스트림을 생성한 다음 파일 모드 지정. 파일 스트림을 열고 데이터 작성
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    #region
    /// <summary>
    /// LocalData
    /// </summary>
    
    public JsonLocalDataList LoadLocalData()
    {
        string fullPath = Path.Combine(dataDirPath, localDataFileName);
        JsonLocalDataList loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToload = "";
                // 파일 스트림을 열고 데이터를 읽음
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToload = reader.ReadToEnd();
                    }
                }
                // deserialize (Json 문자열을 JsonGameData 객체로 변환)
                loadedData = JsonUtility.FromJson<JsonLocalDataList>(dataToload);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data to file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void SaveLocalData(JsonLocalDataList localDataList, JsonGameData newData)
    {
        string fullPath = Path.Combine(dataDirPath, localDataFileName);
        JsonLocalGameData newLocalData  = new JsonLocalGameData(newData.col2, newData.col1);
        localDataList.jsonLocalDataList.Add(newLocalData);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(localDataList, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public void ResetLocalData(JsonLocalDataList localDataList)
    {
        localDataList.jsonLocalDataList.Clear();
        string fullPath = Path.Combine(dataDirPath, localDataFileName);
        try
        {
            string dataToStore = JsonUtility.ToJson(localDataList, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("Error occured when trying to reset data to file: " + fullPath + "\n" + e);
        }
    }
    #endregion
}
