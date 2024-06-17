using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonSceneManager : MonoBehaviour
{
    public GameObject player;

    public void SceneChange(string num)
    {
        SceneManager.LoadScene("Scene" + num);
    }

    public void SceneChange_Data()
    {
        SceneManager.LoadScene("Data View");
    }
}
