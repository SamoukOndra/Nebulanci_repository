using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMeleeTriggerHandler : MonoBehaviour
{
    [SerializeField] float dmg = 10;

    List<GameObject> targetedPlayers = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetedPlayers.Add(other.gameObject);
        }

        //if (targetPlayer != null)
        //{
        //    PlayerHealth health = targetPlayer.GetComponent<PlayerHealth>();
        //    if (health.DamageAndReturnValidKill(dmg))
        //    {
        //        EventManager.InvokeOnPlayerKill(gameObject);
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetedPlayers.Contains(other.gameObject))
        {
            targetedPlayers.Remove(other.gameObject);
        }
    }

    public void HitPlayer() // on animtion event
    {
        if (targetedPlayers == null) return;
        foreach (GameObject player in targetedPlayers)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            ph.DamageAndReturnValidKill(dmg);

            CollisionMaterials cm = player.GetComponent<CollisionMaterials>();
            Vector3 hitPoint = gameObject.transform.position;
            Vector3 hitDirection = (player.transform.position - (hitPoint + Vector3.down)).normalized;
            Quaternion rotation = Quaternion.LookRotation(hitDirection);
            cm.Interact(hitPoint, rotation);
        }
    }

    private void OnDisable()
    {
        targetedPlayers = new();
    }
}
