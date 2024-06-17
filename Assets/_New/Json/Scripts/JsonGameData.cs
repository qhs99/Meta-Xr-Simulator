using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonGameData
{
    public string vcmIdx;
    public string mngId;
    public string vumIdx;

    public string col1;
    public string col2;

    public JsonGameData()
    {
        this.vcmIdx = "vcm24051002395yRP1x";
        this.mngId = "관리자승인아이디";
        this.vumIdx = "사용자승인아이디";

        this.col1 = "12";
        this.col2 = "Guest User";
    }
}


