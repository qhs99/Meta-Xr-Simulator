using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence<T>
{
    /// <summary>
    /// 게임 데이터를 로드하고 저장하는 두 가지 메서드를 정의.
    /// 이를 통해 게임 데이터의 일관된 접근 방식을 제공하고, 여러 클래스에서 게임 데이터를 로드하거나 저장할 수 있도록 함.
    /// </summary>
    /// <param name="data"></param>

    //게임 데이터 개체를 가져옴
    void LoadData(T data);

    //게임 데이터 개체에 대한 참조를 가져옴
    void SaveData(ref T data);
}
