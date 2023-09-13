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

    [Header("Grenades")]
    [SerializeField] Transform grenadeThrowTransform;
    //[SerializeField] float forceGrowth = 0.35f; hardcoded v throwGrenadeCoroutine
    //[SerializeField] float maxForceRation = 1;

    [Header("Default weapon")]
    [SerializeField] GameObject defaultWeapon;
    [SerializeField] float defaultWeaponReloadDuration = 3f;

    [Header("Melee")]
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

        //uz tam mam initial state check

        if (attackButtonPressed && !cooldownIsActive) Attack();

    }

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += BlockAttack;
        EventManager.OnPlayerDeath += ResetWeapons;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= BlockAttack;
        EventManager.OnPlayerDeath -= ResetWeapons;
    }


    #region METHODS
    public void Initialize()
    {
        meleeTriger.SetActive(false);

        animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
        weaponSlotTransform = animatorHandler.weaponSlotTransform;

        InstantiateWeapon(defaultWeapon); // prvni je default, bo index 0, dulezity pro reload misto zniceni
        if (meleeWeapon != null)
            InstantiateWeapon(meleeWeapon); // melee sou neznicitelny

        StartCoroutine(CorrectTransformCoroutine(0.1f)); // mozna refactor az pridam neco. zapotrebi i pri override animator controller
        //cooldownIsActive = false;
    }

    private void Attack()
    {
        int updatedAmmo = selectedWeaponScript.EvaluateAttackCondition();

        if(updatedAmmo >= 0 && selectedWeaponGO.TryGetComponent(out Grenade grenade))
        {
            StartCoroutine(ThrowGrenadeCoroutine(updatedAmmo, grenade));
            return;
        }

        ManageWeaponOnAmmo(updatedAmmo);
    }

    public void BlockAttack(GameObject player)
    {
        if (player == gameObject)
            attackButtonPressed = false;
    }

    private void ManageWeaponOnAmmo(int updatedAmmo)
    {
        UpdateWeaponOnAmmo(updatedAmmo);

        if (updatedAmmo != -1) StartCoroutine(CooldownCoroutine(cooldownDuration, f_destroySelectedWeaponAfterCD));
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


    public void WeaponPickUp(GameObject weaponGO)
    {
        if (weaponGO.TryGetComponent(out Weapons weapons))
        {
            Debug.Log("weaponID: " + weapons.WeaponID); ////////////////////
            int id = weapons.WeaponID;

            if (availableWeaponsDictionary.ContainsKey(id))
                ReloadWeaponOfID(id);

            else
            {
                AddNewWeapon(weaponGO);
                if (!cooldownIsActive) SelectLastAddedWeapon();
            }
        }
    }

    private void AddNewWeapon(GameObject sourceWeapon)
    {
        GameObject newWeapon = Instantiate(sourceWeapon, weaponSlotTransform);
        newWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        int id = sourceWeapon.GetComponent<Weapons>().WeaponID;

        availableWeaponsDictionary.Add(id, newWeapon);
        availableWeaponsGO.Add(newWeapon);

        newWeapon.GetComponent<Weapons>().shootingPlayer = gameObject;

        if(newWeapon.TryGetComponent(out Melee melee))
        {
            melee.meleeTriger = this.meleeTriger;
        }

        if(newWeapon.TryGetComponent(out Grenade grenade))
        {
            grenade.SetProjectileSpawnPoint(grenadeThrowTransform);
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
        if (availableWeaponsDictionary.TryGetValue(weaponID, out GameObject weaponToReload))
        {
            weaponToReload.GetComponent<Weapons>().Reload();
        }
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (cooldownIsActive) return;

        if (selectedWeaponGO != null)
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
    }

    private void InstantiateWeapon(GameObject weaponGO)
    {
        GameObject _weapon = Instantiate(weaponGO);
        WeaponPickUp(_weapon);
        Destroy(_weapon);
        SelectWeapon(0);
    }

    public void ResetWeapons(GameObject player)
    {
        if (player != gameObject) return;

        int i = 1;
        if (meleeWeapon != null) i = 2;

        while (i < availableWeaponsGO.Count)
        {
            DestroyWeaponOfID(availableWeaponsGO[i].GetComponent<Weapons>().WeaponID);
            i++;
        }
    }

    #endregion METHODS


    #region INPUT ACTIONS
    public void OnFire(InputValue value)
    {
        attackButtonPressed = value.isPressed;
        selectedWeaponScript.isAttacking = value.isPressed;
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

        yield return new WaitForSeconds(duration);

        cooldownIsActive = false;

        if (destroySelectedWeaponAfterCD)
        {
            DestroyWeaponOfID(selectedWeaponScript.WeaponID);
            SelectWeapon(0);
            f_destroySelectedWeaponAfterCD = false;
        }
    }

    IEnumerator ReloadDefaultWeaponCortoutine(float duration, Weapons selectedWeaponScript)
    {
        yield return new WaitForSeconds(duration);
        selectedWeaponScript.Reload();
    }

    IEnumerator ThrowGrenadeCoroutine(int updatedAmmo, Grenade grenade)
    {
        cooldownIsActive = true;

        float throwForceRation = 0;
        float rationGrowth = 0.35f;
        float minRation = 0.1f;

        while (attackButtonPressed)
        {
            if (throwForceRation < 1)
                throwForceRation += rationGrowth * Time.deltaTime;

            Debug.Log("throw forece: " + throwForceRation);

            yield return null;
        }

        if (throwForceRation < minRation)
            throwForceRation = minRation;

        grenade.GrenadeThrow(throwForceRation);

        ManageWeaponOnAmmo(updatedAmmo);
    }

    #endregion COROUTINES
}
