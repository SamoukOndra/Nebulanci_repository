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

    AnimatorHandler animatorHandler;

    private Dictionary<int, GameObject> availableWeaponsDictionary = new Dictionary<int, GameObject>();
    List<GameObject> availableWeaponsGO = new List<GameObject>();
    GameObject selectedWeaponGO;
    private int selectedWeaponIndex = 0;
    private Weapons selectedWeaponScript;

    #region METHODS
    public void Initialize()
    {
        animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
        weaponSlotTransform = animatorHandler.weaponSlotTransform;
        WeaponPickUp(defaultWeapon);
        SelectWeapon(0);
        StartCoroutine(CorrectTransformCoroutine(0.1f)); // mozna refactor az pridam neco
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
        
        weapons.animatorHandler = animatorHandler;
        
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
        if(selectedWeaponGO != null) 
            selectedWeaponGO.SetActive(false);
        
        selectedWeaponGO = availableWeaponsGO[weaponIndex];
        selectedWeaponGO.SetActive(true);
        
        CorrectWeaponTransform();
        
        selectedWeaponScript = selectedWeaponGO.GetComponent<Weapons>();
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
    }

    public void OnChangeWeapon()
    {
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
    // COROUTINES
    IEnumerator CorrectTransformCoroutine(float delay)
    {
        //without delay wrong results on Initialize();
        yield return new WaitForSeconds(delay);
        CorrectWeaponTransform();
    }
    #endregion COROUTINES


    ///TEST
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CorrectWeaponTransform();
        }
    }
}
