using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballNet : MonoBehaviour
{
    [SerializeField] bool redTeamTarget;

    BoxCollider trigger;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Ball ball))
        {
            FootballManager.singleton.InvokeOnTeamScore(redTeamTarget);
            //tady nejaky rachejtle a sfx
            ball.Respawn();
        }
    }
}
