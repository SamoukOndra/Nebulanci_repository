using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcNavigation : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private void OnEnable()
    {
        agent.enabled = true;
    }

    private void Update()
    {
        agent.destination = PlayerSpawner.playerSpawnerSingleton.GetClosestPlayerPos(gameObject.transform.position);
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }
}
