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

    private void Update()
    {
        agent.destination = PlayerSpawner.playerSpawnerSingleton.GetClosestPlayerPos(gameObject.transform.position);
    }
}
