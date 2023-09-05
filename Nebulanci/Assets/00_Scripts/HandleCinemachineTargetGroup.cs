using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandleCinemachineTargetGroup : MonoBehaviour
{
    [SerializeField] GameObject deathPosition; // budou slouzit aj k provedeni gore VFX atd
    List<GameObject> deathPositions = new();

    CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] float respawnCameraBlend = 1f;

    private void Awake()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void OnEnable()
    {
        EventManager.OnPlayerAdded += AddTarget;
        EventManager.OnPlayerDeath += SmoothenRespawnCamera;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= AddTarget;
        EventManager.OnPlayerDeath -= SmoothenRespawnCamera;
    }

    public void AddTarget(GameObject target)
    {
        cinemachineTargetGroup.AddMember(target.transform, 1, 0);
        GameObject newDeathPosition = Instantiate(deathPosition);
        deathPositions.Add(newDeathPosition);
        newDeathPosition.SetActive(false);
    }

    public void SmoothenRespawnCamera(GameObject deathPlayer)
    {
        //int index = cinemachineTargetGroup.FindMember(deathPlayer.transform);
        StartCoroutine(SmoothRespawnCameraCoroutine(deathPlayer));
    }

    IEnumerator SmoothRespawnCameraCoroutine(GameObject deathPlayer)
    {
        float timer = 0;

        int index1 = cinemachineTargetGroup.FindMember(deathPlayer.transform);
        cinemachineTargetGroup.m_Targets[index1].weight =0;
        
        GameObject newTarget = deathPositions[index1];
        newTarget.transform.position = deathPlayer.transform.position;
        newTarget.SetActive(true);

        cinemachineTargetGroup.AddMember(newTarget.transform, 1, 0);

        int index2 = cinemachineTargetGroup.FindMember(newTarget.transform);

        //while(timer < respawnCameraBlend)
        //{
        //    timer += Time.deltaTime;
        //    
        //    cinemachineTargetGroup.m_Targets[index2].weight = 1 - (timer / respawnCameraBlend);
        //
        //    yield return null;
        //}
        //
        //cinemachineTargetGroup.RemoveMember(newTarget.transform); //nikde sem ho ale zatim nedeaktivoval, bude poolnutej s vlastním scriptem??

        yield return new WaitForSeconds(PlayerSpawner.respawnPlayerWaitTime/* - respawnCameraBlend*/);

        timer = 0f;
        while (timer < respawnCameraBlend)
        {
            timer += Time.deltaTime;

            cinemachineTargetGroup.m_Targets[index1].weight = (timer / respawnCameraBlend);
            cinemachineTargetGroup.m_Targets[index2].weight = 1 - (timer / respawnCameraBlend);
            

            yield return null;
        }

        cinemachineTargetGroup.RemoveMember(newTarget.transform); //nikde sem ho ale zatim nedeaktivoval, bude poolnutej s vlastním scriptem??
        cinemachineTargetGroup.m_Targets[index1].weight = 1f;
    }
}
