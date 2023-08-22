using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static AnimatorHandler GetAnimatorHandlerInChildren(GameObject parent)
    {
        AnimatorHandler animatorHandler = parent.GetComponentInChildren<AnimatorHandler>();
        return animatorHandler;
    }
}
