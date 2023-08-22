using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapons : MonoBehaviour
{
    public AnimatorHandler animatorHandler;
    public int weaponID { get; protected set; }
    public float cooldown { get; protected set; }


    //public virtual void Initialize()
    //{
    //    animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
    //    
    //}

    public abstract void Attack();
    public abstract void Reload(); // v pripade pusek doplni maximum, u granatu +1 do maxima 5?
}
