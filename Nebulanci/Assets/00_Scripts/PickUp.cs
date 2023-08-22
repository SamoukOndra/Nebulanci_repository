using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    // tondle musi bejt uz instanciovanej objekt, jinac zustane weaponID na defaultni hodnote nula z abstract class
    public GameObject pickUpItem;

    Collider collider;

    private bool isWeapon;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;

        isWeapon = pickUpItem.TryGetComponent<Weapons>(out Weapons weapons);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWeapon)
            {
                CombatHandler combatHandler = other.GetComponent<CombatHandler>();
                combatHandler.AddWeapon(pickUpItem);
            }

            gameObject.SetActive(false);
        }
    }

}
