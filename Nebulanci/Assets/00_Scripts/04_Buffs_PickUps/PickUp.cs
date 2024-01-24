using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static float pickUpDuration = 1;

    [SerializeField] Transform itemHolder;
    [SerializeField] Vector3 itemOffsetPosition = Vector3.up;

    // tondle musi bejt uz instanciovanej objekt, jinac zustane weaponID na defaultni hodnote nula z abstract class
    [HideInInspector]
    public GameObject pickUpItem;

    Collider _collider;
    AudioClip pickUpSound;
    AudioClip gunGrabSound;
    AudioClip buffGrabSound;

    private bool isWeapon;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;

        pickUpSound = AudioManager.audioList.pop;
        gunGrabSound = AudioManager.audioList.gunGrab;
        buffGrabSound = AudioManager.audioList.tss;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWeapon)
            {
                CombatHandler combatHandler = other.GetComponent<CombatHandler>();
                combatHandler.WeaponPickUp(pickUpItem);

                PlayClip(gunGrabSound);

            }

            else
            {
                pickUpItem.GetComponent<PickUpBuff>().Interact(other.gameObject);

                PlayClip(buffGrabSound);
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

            pickUpItem.transform.SetParent(itemHolder);
            pickUpItem.SetActive(true);
            pickUpItem.transform.position = gameObject.transform.position + itemOffsetPosition;

            PlayClip(pickUpSound);
        }
    }

    public bool DecideIfWeapon()
    {
        isWeapon = pickUpItem.TryGetComponent<Weapons>(out Weapons weapons);
        return isWeapon;
    }

    private void DisableSelf()
    {
        pickUpItem.transform.SetParent(null);
        pickUpItem.transform.localScale = Vector3.one;
        pickUpItem.SetActive(false);
        gameObject.SetActive(false);
    }

    private void PlayClip(AudioClip clip)
    {
        GameObject _asGo = AudioSourcePool.audioSourcePoolSingleton.GetPooledAS_pickup();
        _asGo.transform.position = itemHolder.position;
        AudioSource _as = _asGo.GetComponent<AudioSource>();
        _asGo.SetActive(true);
        Util.RandomizePitch(_as);
        _as.PlayOneShot(clip);
    }

    IEnumerator DisableSelfCoroutine()
    {
        yield return new WaitForSeconds(pickUpDuration);
        DisableSelf();
    }
}
