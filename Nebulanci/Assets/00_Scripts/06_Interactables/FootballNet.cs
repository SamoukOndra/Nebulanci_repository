using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballNet : MonoBehaviour
{
    [SerializeField] bool redTeamTarget;
    [SerializeField] HandleCinemachineTargetGroup hctg;

    BoxCollider trigger;
    public float camTargetWeight = 0.5f;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
        trigger.isTrigger = true;

        hctg.AddStaticTarget(gameObject.transform, camTargetWeight);
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
