using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;

public class PlayerInfo : NetworkBehaviour
{
    public TMP_Text playerNameTxt;
    [SyncVar]
    public string playerName = "";
}
