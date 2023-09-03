using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatHandler : MonoBehaviour
{
    private bool attackButtonPressed;

    //Cooldown blokuje zmenu zbrane, DULEZITE!!!! (destroy non-default weapon)
    private bool cooldownIsActive;
    private float cooldownDuration;
    private bool f_destroySelectedWeaponAfterCD = false;


    Transform weaponSlotTransform;

    [SerializeField] GameObject defaultWeapon;
    [SerializeField] float defaultWeaponReloadDuration = 3f;

    [SerializeField] GameObject meleeWeapon;
    [SerializeField] GameObject meleeTriger;

    AnimatorHandler animatorHandler;

    private Dictionary<int, GameObject> availableWeaponsDictionary = new();
    List<GameObject> availableWeaponsGO = new();
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
        meleeTriger.SetActive(false);

        animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
        weaponSlotTransform = animatorHandler.weaponSlotTransform;
        
        InstantiateWeapon(defaultWeapon); // prvni je default, bo index 0, dulezity pro reload misto zniceni
        if(meleeWeapon != null)
            InstantiateWeapon(meleeWeapon); // melee sou neznicitelny

        StartCoroutine(CorrectTransformCoroutine(0.1f)); // mozna refactor az pridam neco. zapotrebi i pri override animator controller
    }

    private void Attack()
    {
        int updatedAmmo = selectedWeaponScript.EvaluateAttackCondition();
        
        UpdateWeaponOnAmmo(updatedAmmo);

        if(updatedAmmo != -1) StartCoroutine(CooldownCoroutine(cooldownDuration, f_destroySelectedWeaponAfterCD));
        //update UI atd...
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
            f_destroySelectedWeaponAfterCD = true;
        }
    }

    private void DestroyWeapon(Weapons weapons)
    {
        DestroyWeaponOfID(weapons.WeaponID);
    }

    public void WeaponPickUp(GameObject weaponGO)
    {
        if (weaponGO.TryGetComponent(out Weapons weapons))
        {
            Debug.Log("weaponID: " + weapons.WeaponID); ////////////////////

            if (availableWeaponsDictionary.ContainsKey(weapons.WeaponID))
                ReloadWeaponOfID(weapons.WeaponID);
            
            else
            {
                AddNewWeapon(weaponGO, weapons);
                if(!cooldownIsActive) SelectLastAddedWeapon();
            }    
        }
    }

    private void AddNewWeapon(GameObject weaponGO, Weapons weapons)
    {
        GameObject newWeapon = Instantiate(weaponGO, weaponSlotTransform);
        Debug.Log("is new weapon active?: " + newWeapon.activeInHierarchy);////////
        //weapons.animatorHandler = animatorHandler;
        //weapons.animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
        //weapons.GetAnimatorHandler(gameObject);
        //weapons.GetAnimatorH();

        availableWeaponsDictionary.Add(weapons.WeaponID, newWeapon);
        availableWeaponsGO.Add(newWeapon);

        if(newWeapon.TryGetComponent(out Melee melee))
        {
            melee.meleeTriger = this.meleeTriger;
        }

        newWeapon.SetActive(false);
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
            availableWeaponsDictionary.Remove(weaponID, out GameObject weaponToDestroy);
            availableWeaponsGO.Remove(weaponToDestroy);
            Destroy(weaponToDestroy);
        }
    }

    private void ReloadWeaponOfID(int weaponID)
    {
        if(availableWeaponsDictionary.TryGetValue(weaponID, out GameObject weaponToReload))
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
        
        

        selectedWeaponIndex = weaponIndex;
        
        selectedWeaponScript = selectedWeaponGO.GetComponent<Weapons>();

        animatorHandler.SetAnimatorWeaponID(selectedWeaponScript.WeaponID);

        animatorHandler.SetAnimatorOverrideController(selectedWeaponScript.animatorOverrideController);

        SetCooldownDurationFromWeapon(selectedWeaponScript);

        //CorrectWeaponTransform();
        StartCoroutine(CorrectTransformCoroutine(0.1f));
    }

    private void SetCooldownDurationFromWeapon(Weapons weapons)
    {
        cooldownDuration = weapons.CooldownDuration;
    }

    private void CorrectWeaponTransform()
    {
        selectedWeaponGO.transform.forward = transform.forward;
        Debug.Log("aim corrected");///////////////////////
    }

    private void InstantiateWeapon(GameObject weaponGO)
    {
        GameObject _weapon = Instantiate(weaponGO);
        WeaponPickUp(_weapon);
        Destroy(_weapon);
        SelectWeapon(0);
    }

    //private void InstantiateMeleeWeapon()
    //{
    //    GameObject _meleeWeapon = Instantiate(meleeWeapon);
    //    WeaponPickUp(_meleeWeapon);
    //    Destroy(_meleeWeapon);
    //    SelectWeapon(0);
    //}


    #endregion METHODS


    #region INPUT ACTIONS
    public void OnFire(InputValue value)
    {
        attackButtonPressed = value.isPressed;
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

    IEnumerator CooldownCoroutine(float duration, bool destroySelectedWeaponAfterCD)
    {
        cooldownIsActive = true;
        Debug.Log("CD start");

        yield return new WaitForSeconds(duration);
        
        cooldownIsActive = false;
        
        if (destroySelectedWeaponAfterCD)
        {
            DestroyWeapon(selectedWeaponScript);
            SelectWeapon(0);
            f_destroySelectedWeaponAfterCD = false;
        } 
    }

    IEnumerator ReloadDefaultWeaponCortoutine(float duration, Weapons selectedWeaponScript)
    {
        yield return new WaitForSeconds(duration);
        selectedWeaponScript.Reload();
    }


    #endregion COROUTINES
}
