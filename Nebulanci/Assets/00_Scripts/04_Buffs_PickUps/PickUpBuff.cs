using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpBuff : MonoBehaviour
{
    public string buffName;
    public abstract void Interact(GameObject player);
}
