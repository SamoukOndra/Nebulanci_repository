using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_InGame : MonoBehaviour
{
    public delegate void Attack(GameObject player);
    public static event Attack OnAttack;
}
