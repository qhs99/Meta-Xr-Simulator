using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustomNetwork : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetColor))]
    private Color newColor;

    [SerializeField] GameObject cylinder;

    [ClientRpc]
    void RpcUpdateGameState(string newState)
    {
        Debug.Log("Game state updated to: " + newState);
    }

    public void UpdateGameState(string newState)
    {
        RpcUpdateGameState(newState);
    }

    // Color Event 1)�������� ���ο� ���� ����
    public void RandomColorCreate()
    {
        newColor = Random.ColorHSV();
    }

    // Color Event 2)����� �� 
    [ClientRpc] //�̰Ÿ� �� ���� localPlayer�� ���� ���� remotePlayer�� �� �� �ʰ� ����ȭ ��
    void SetColor(Color oldColor, Color newColor)
    {
        this.newColor = newColor;
    }

    // Color Event 3)
    [ClientRpc]
    public void ColorChange(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = newColor;
    }

    [ClientRpc]
    public void SceneChange(int i)
    {
        SceneManager.LoadScene("Main Scene" + i);
    }

    public void BoxCreate(GameObject obj)
    {
        NetworkServer.Spawn(Instantiate(obj, new Vector3(-1, 1, -1), Quaternion.identity));
    }

    //===============================
    [ClientRpc]
    public void ChildenActivate()
    {
        cylinder.transform.GetChild(0).gameObject.SetActive(!cylinder.transform.GetChild(0).gameObject.activeSelf);
    }

}
