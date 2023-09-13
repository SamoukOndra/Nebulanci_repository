using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static float pickUpDuration = 1;

    [SerializeField] Vector3 itemOffsetPosition = Vector3.up;

    // tondle musi bejt uz instanciovanej objekt, jinac zustane weaponID na defaultni hodnote nula z abstract class
    public GameObject pickUpItem;

    Collider collider;

    private bool isWeapon;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWeapon)
            {
                CombatHandler combatHandler = other.GetComponent<CombatHandler>();
                combatHandler.WeaponPickUp(pickUpItem);
            }

            DisableSelf();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DisableSelfCoroutine());

        if(pickUpItem != null)
        {
            isWeapon = pickUpItem.TryGetComponent<Weapons>(out Weapons weapons);

            pickUpItem.SetActive(true);
            pickUpItem.transform.position = gameObject.transform.position + itemOffsetPosition;
        }
    }

    public bool DecideIfWeapon()
    {
        isWeapon = pickUpItem.TryGetComponent<Weapons>(out Weapons weapons);
        return isWeapon;
    }

    private void DisableSelf()
    {
        pickUpItem.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator DisableSelfCoroutine()
    {
        yield return new WaitForSeconds(pickUpDuration);
        DisableSelf();
    }
}
