using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence<T>
{
    /// <summary>
    /// ���� �����͸� �ε��ϰ� �����ϴ� �� ���� �޼��带 ����.
    /// �̸� ���� ���� �������� �ϰ��� ���� ����� �����ϰ�, ���� Ŭ�������� ���� �����͸� �ε��ϰų� ������ �� �ֵ��� ��.
    /// </summary>
    /// <param name="data"></param>

    //���� ������ ��ü�� ������
    void LoadData(T data);

    //���� ������ ��ü�� ���� ������ ������
    void SaveData(ref T data);
}
