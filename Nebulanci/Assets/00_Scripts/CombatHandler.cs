using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatHandler : MonoBehaviour
{
    private bool attackButtonPressed;

    private bool cooldownIsActive;
    private float cooldownDuration;


    Transform weaponTransform;

    [SerializeField] GameObject defaultWeapon;

    AnimatorHandler animatorHandler;

    private Dictionary<int, GameObject> availableWeaponsDictionary = new Dictionary<int, GameObject>();
    List<GameObject> availableWeaponsGO = new List<GameObject>();
    GameObject selectedWeaponGO;
    private int selectedWeaponIndex = 0;
    private Weapons selectedWeaponScript;

    public void Initialize()
    {
        animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
        weaponTransform = animatorHandler.weaponTransform;
        AddWeapon(defaultWeapon);
        SelectWeapon(0);
        StartCoroutine(CorrectTransformCoroutine(0.1f));
    }

    public void AddWeapon(GameObject weapon)
    {
        Weapons w;
        if (weapon.TryGetComponent<Weapons>(out w))
        {
            Debug.Log("AddWeapon valid item");
            Debug.Log(w.weaponID);

            if (availableWeaponsDictionary.ContainsKey(w.weaponID))
                ReloadWeaponOfID(w.weaponID);
            else
            {
                Debug.Log("AddWeapon: new weapon");
                GameObject newWeapon = Instantiate(weapon, weaponTransform);
                w.animatorHandler = animatorHandler;
                availableWeaponsDictionary.Add(w.weaponID, newWeapon);
                availableWeaponsGO.Add(newWeapon);

                selectedWeaponIndex = availableWeaponsGO.Count - 1;
                SelectWeapon(selectedWeaponIndex);
            }
        }
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

    //private void RemoveWeapon(GameObject weapon)
    //{
    //    availableWeapons.Remove(weapon);
    //    Destroy(weapon);
    //}

    //private void HandleDictionary(int key, GameObject weapon)
    //{
    //    if (availableWeaponsDictionary.ContainsKey(key))
    //    {
    //        GameObject weaponToDestroy;
    //        availableWeaponsDictionary.Remove(key, out weaponToDestroy);
    //        RemoveWeapon(weaponToDestroy);
    //    }
    //}

    private void SelectWeapon(int weaponIndex)
    {
        if(selectedWeaponGO != null) 
            selectedWeaponGO.SetActive(false);
        selectedWeaponGO = availableWeaponsGO[weaponIndex];
        selectedWeaponGO.SetActive(true);
        CorrectWeaponTransform();
        selectedWeaponScript = selectedWeaponGO.GetComponent<Weapons>();
    }

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
    
    IEnumerator CorrectTransformCoroutine(float delay)
    {
        //without delay wrong results on Initialize();
        yield return new WaitForSeconds(delay);
        CorrectWeaponTransform();
    }

    private void CorrectWeaponTransform()
    {
        selectedWeaponGO.transform.forward = transform.forward;
        Debug.Log("aim corrected");
    }

    ///TEST
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CorrectWeaponTransform();
        }
    }
}
