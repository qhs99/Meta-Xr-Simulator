using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardInput : MonoBehaviour
{
    public Button[] btn;
    public Button deleteBtn;
    private TMP_InputField field;
    private string btnName;

    private void Start()
    {
        field = GetComponent<TMP_InputField>();

        /*for (int i = 0; i < btn.Length; i++)
        {
            btnName = btn[i].name;
            btn[i].onClick.AddListener(()=> InputValue(btnName));

            Debug.Log(btn[i]);
        }*/

        deleteBtn.onClick.AddListener(DeleteButton);
    }

    public void InputValue(string name)
    {
        field.text += name;
    }

    public void DeleteButton()
    {
        field.text = "";
    }

    public void BackwardButton()
    {
        field.text = field.text.Substring(0, field.text.Length - 1);
    }
}
