using System;
using System.IO;
using UnityEngine;

public class JsonFileDataHandler
{
    /// <summary>
    /// ���� �����͸� ����(Json����)�� �����ϰ� �ε��ϱ� ���� ��� ����.
    /// </summary>

    // ������
    private string dataDirPath = "";
    // ���������̸�
    private string dataFileName = "";
    // ������������̸�
    private string localDataFileName = "";

    // ������ -> ������ ���丮 ��ο� ���� �̸��� �Ű������� �޾Ƽ� ����
    public JsonFileDataHandler(string dataDirPath, string dataFileName, string localDataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.localDataFileName = localDataFileName;
    }

    // ���� ������ ��ü ��ȯ
    // ����� ���� �����͸� ���Ͽ��� �о���� �޼���
    public JsonGameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        JsonGameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToload = "";
                // ���� ��Ʈ���� ���� �����͸� ����
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToload = reader.ReadToEnd();
                    }
                }
                // deserialize (Json ���ڿ��� JsonGameData ��ü�� ��ȯ)
                loadedData = JsonUtility.FromJson<JsonGameData>(dataToload);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data to file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    // ���� ������ ��ü ��������
    // ���� �����͸� ���Ͽ� �����ϴ� �޼���
    public void Save(JsonGameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // ���丮�� �������� ������ ����
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // serialize (Json ���ڿ��� ��ȯ)
            string dataToStore = JsonUtility.ToJson(data, true);
            // ������ �аų� �� �� using ����� ����ϸ� �б⳪ ���Ⱑ ������ �ش� ���Ͽ� ���� ������ �������� ����.
            // ���� ��θ� �����ϴ� �� ���� ��Ʈ���� ������ ���� ���� ��� ����. ���� ��Ʈ���� ���� ������ �ۼ�
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
                // ���� ��Ʈ���� ���� �����͸� ����
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToload = reader.ReadToEnd();
                    }
                }
                // deserialize (Json ���ڿ��� JsonGameData ��ü�� ��ȯ)
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
