using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiplayerSetup : MonoBehaviour
{
    public delegate void PlayerAdded();
    public static event PlayerAdded OnPlayerAdded;
}
