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

    private Dictionary<GameObject, GameObject> playerDeathPosPairs = new();
    private void Awake()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void OnEnable()
    {
        EventManager.OnPlayerAdded += AddTarget;
        EventManager.OnPlayerDeath += SmoothenRespawnCamera;
        EventManager.OnPlayerDeath += PlayDeathEffects;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= AddTarget;
        EventManager.OnPlayerDeath -= SmoothenRespawnCamera;
        EventManager.OnPlayerDeath -= PlayDeathEffects;
    }

    public void AddTarget(GameObject targetPlayer, PlayerBlueprint notUsedHere)
    {
        cinemachineTargetGroup.AddMember(targetPlayer.transform, 1, 0);
        GameObject newDeathPosition = Instantiate(deathPosition);
        cinemachineTargetGroup.AddMember(newDeathPosition.transform, 0, 0);
        playerDeathPosPairs.Add(targetPlayer, newDeathPosition);
    }

    public void AddStaticTarget(Transform targetTransform, float weight)
    {
        cinemachineTargetGroup.AddMember(targetTransform, weight, 0);
    }

    public void SmoothenRespawnCamera(GameObject deathPlayer)
    {
        StartCoroutine(SmoothRespawnCameraCoroutine(deathPlayer));
    }

    public void PlayDeathEffects(GameObject deathPlayer)
    {
        if (playerDeathPosPairs.TryGetValue(deathPlayer, out GameObject _deathPosition))
        {
            DeathPositionVFXHandler deathPositionVFXHandler = _deathPosition.GetComponent<DeathPositionVFXHandler>();
            deathPositionVFXHandler.PlayDeathVFX();
        }
    }

    IEnumerator SmoothRespawnCameraCoroutine(GameObject deathPlayer)
    {
        float timer = 0;

        int index1 = cinemachineTargetGroup.FindMember(deathPlayer.transform);
        cinemachineTargetGroup.m_Targets[index1].weight =0;



        playerDeathPosPairs.TryGetValue(deathPlayer, out GameObject newTarget);
        newTarget.transform.position = deathPlayer.transform.position;

        int index2 = cinemachineTargetGroup.FindMember(newTarget.transform);

        cinemachineTargetGroup.m_Targets[index2].weight = 1;

        yield return new WaitForSeconds(PlayerSpawner.respawnPlayerWaitTime + 0.1f);

        timer = 0f;
        while (timer < respawnCameraBlend)
        {
            timer += Time.deltaTime;

            cinemachineTargetGroup.m_Targets[index1].weight = (timer / respawnCameraBlend);
            cinemachineTargetGroup.m_Targets[index2].weight = 1 - (timer / respawnCameraBlend);
            

            yield return null;
        }

        cinemachineTargetGroup.m_Targets[index2].weight = 0f;
        cinemachineTargetGroup.m_Targets[index1].weight = 1f;

        //MOZNY BUGY??? pokud zemre hned znova behem dobihani tyhle coroutine??
    }
}
