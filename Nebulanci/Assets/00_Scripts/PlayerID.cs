using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerID : MonoBehaviour
{
    public int PlayerIDnumber { get; private set; }

    public void SetPlayerID(int id)
    {
        PlayerIDnumber = id;
    }
}
