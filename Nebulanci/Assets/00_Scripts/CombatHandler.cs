using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatHandler : MonoBehaviour
{
    private bool attackButtonPressed;

    private bool cooldownIsActive;
    private float cooldownDuration;


    Transform weaponSlotTransform;

    [SerializeField] GameObject defaultWeapon;
    [SerializeField] float defaultWeaponReloadDuration = 3f;

    AnimatorHandler animatorHandler;

    private Dictionary<int, GameObject> availableWeaponsDictionary = new Dictionary<int, GameObject>();
    List<GameObject> availableWeaponsGO = new List<GameObject>();
    GameObject selectedWeaponGO;
    private int selectedWeaponIndex = 0;
    private Weapons selectedWeaponScript;


    private void Update()
    {
        //action type Pass Through, no Initial State Check !!!!
        //NEBO v Start spustit CooldownCoroutine by asi taky slo

        if (attackButtonPressed && !cooldownIsActive) Attack();

    }



    #region METHODS
    public void Initialize()
    {
        animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
        weaponSlotTransform = animatorHandler.weaponSlotTransform;
        WeaponPickUp(defaultWeapon);
        SelectWeapon(0);
        StartCoroutine(CorrectTransformCoroutine(0.1f)); // mozna refactor az pridam neco
    }

    private void Attack()
    {
        int updatedAmmo = selectedWeaponScript.Attack();
        UpdateWeaponOnAmmo(updatedAmmo);
    }

    private void UpdateWeaponOnAmmo(int currentAmmo)
    {
        //UpdateUIWeapon(currentAmmo) zatim neexistuje
        if (currentAmmo > 0 || currentAmmo == -1) return;
        else if (currentAmmo == 0 && selectedWeaponIndex == 0)
        {
            StartCoroutine(ReloadDefaultWeaponCortoutine(defaultWeaponReloadDuration, selectedWeaponScript));
            selectedWeaponScript.currentAmmo = -1;
        }
        else
        {
            DestroyWeapon(selectedWeaponScript);
            SelectWeapon(0);
        }
    }

    private void DestroyWeapon(Weapons weapons)
    {
        DestroyWeaponOfID(weapons.weaponID);
    }

    public void WeaponPickUp(GameObject weaponGO)
    {
        Weapons weapons;
        if (weaponGO.TryGetComponent<Weapons>(out weapons))
        {
            Debug.Log("weaponID: " + weapons.weaponID); ////////////////////

            if (availableWeaponsDictionary.ContainsKey(weapons.weaponID))
                ReloadWeaponOfID(weapons.weaponID);
            
            else
            {
                AddNewWeapon(weaponGO, weapons);
                SelectLastAddedWeapon();
            }    
        }
    }

    private void AddNewWeapon(GameObject weaponGO, Weapons weapons)
    {
        GameObject newWeapon = Instantiate(weaponGO, weaponSlotTransform);

        //weapons.animatorHandler = animatorHandler;
        weapons.animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);

        availableWeaponsDictionary.Add(weapons.weaponID, newWeapon);
        availableWeaponsGO.Add(newWeapon);
    }

    private void SelectLastAddedWeapon()
    {
        selectedWeaponIndex = availableWeaponsGO.Count - 1;
        SelectWeapon(selectedWeaponIndex);
    }

    private void DestroyWeaponOfID(int weaponID)
    {
        if (availableWeaponsDictionary.ContainsKey(weaponID))
        {
            GameObject weaponToDestroy;
            availableWeaponsDictionary.Remove(weaponID, out weaponToDestroy);
            availableWeaponsGO.Remove(weaponToDestroy);
            Destroy(weaponToDestroy);
        }
    }

    private void ReloadWeaponOfID(int weaponID)
    {
        GameObject weaponToReload;
        if(availableWeaponsDictionary.TryGetValue(weaponID, out weaponToReload))
        {
            Weapons w = weaponToReload.GetComponent<Weapons>();
            w.Reload();
        }
    }

    private void SelectWeapon(int weaponIndex)
    {
        if (cooldownIsActive) return;

        if(selectedWeaponGO != null) 
            selectedWeaponGO.SetActive(false);
        
        selectedWeaponGO = availableWeaponsGO[weaponIndex];
        selectedWeaponGO.SetActive(true);
        
        CorrectWeaponTransform();

        selectedWeaponIndex = weaponIndex;
        
        selectedWeaponScript = selectedWeaponGO.GetComponent<Weapons>();

        SetCooldownDurationFromWeapon(selectedWeaponScript);
    }

    private void SetCooldownDurationFromWeapon(Weapons weapons)
    {
        cooldownDuration = weapons.cooldownDuration;
    }

    private void CorrectWeaponTransform()
    {
        selectedWeaponGO.transform.forward = transform.forward;
        Debug.Log("aim corrected");///////////////////////
    }
    #endregion METHODS

    #region INPUT ACTIONS
    // INPUT ACTIONS
    public void OnFire(InputValue value)
    {
        attackButtonPressed = value.isPressed;
        Debug.Log("OnFire call");
        //samotnej attack v Update(), z activeWeapon
    }

    public void OnChangeWeapon()
    {
        if (cooldownIsActive) return;

        int weaponsAvailable = availableWeaponsGO.Count;
        if (weaponsAvailable == 1) return;
        else
        {
            selectedWeaponIndex++;
            if (selectedWeaponIndex >= weaponsAvailable)
                selectedWeaponIndex = 0;

            SelectWeapon(selectedWeaponIndex);
        }
    }
    #endregion INPUT ACTIONS

    #region COROUTINES
    IEnumerator CorrectTransformCoroutine(float delay)
    {
        //without delay wrong results on Initialize();
        yield return new WaitForSeconds(delay);
        CorrectWeaponTransform();
    }

    IEnumerator CooldownCoroutine(float duration)
    {
        cooldownIsActive = true;
        yield return new WaitForSeconds(duration);
        cooldownIsActive = false;
    }

    IEnumerator ReloadDefaultWeaponCortoutine(float duration, Weapons selectedWeaponScript)
    {
        yield return new WaitForSeconds(duration);
        selectedWeaponScript.Reload();
    }

    #endregion COROUTINES
}
