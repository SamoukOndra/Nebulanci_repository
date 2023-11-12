using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuGameSettings : MonoBehaviour
{
    [SerializeField] GameObject buffList_Prefab;
    [SerializeField] GameObject buffQuantity_Prefab;
    [SerializeField] GameObject menuContent;
    [SerializeField] TextMeshProUGUI defaultWeaponDescriptionText;
    

    private List<GameObject> allBuffs;
    private List<BuffSetting> allBuffSettings = new();

    //private List<string> allNames;

    private List<int> weaponsIndexes = new();

    private int defWeaponIndex;

    private int meleeWeaponIndex;

    private int selectedDefWeaponIndex = 0;

    private void Start()
    {
        Initialize();
    }

    //private void Start()
    //{
    //    allBuffs = buffList_Prefab.GetComponent<BuffList>().allBuffs;
    //
    //    for(int i = 0; i < allBuffs.Count; i++)
    //    {
    //        GameObject buff = Instantiate(buffQuantity_Prefab, menuContent.transform);
    //        BuffSetting buffSetting = buff.GetComponent<BuffSetting>();
    //        allBuffSettings.Add(buffSetting);
    //
    //        string name = "Name missing";
    //
    //        if(allBuffs[i].TryGetComponent(out Weapons weapon))
    //        {
    //            weaponsIndexes.Add(i);
    //            name = weapon.Name;
    //
    //            if(weapon is Melee)
    //            {
    //                meleeWeaponIndex = i;
    //            }
    //        }
    //
    //        else if(allBuffs[i].TryGetComponent(out PickUpBuff other))
    //        {
    //            name = other.Name;
    //        }
    //
    //        buffSetting.SetName(name);
    //    }
    //}

    public void NextDefWeapon(bool forward)
    {
        int add = (forward ? 1 : -1);

        selectedDefWeaponIndex += add;

        if (forward && selectedDefWeaponIndex == weaponsIndexes.Count)
            selectedDefWeaponIndex = 0;

        else if (!forward && selectedDefWeaponIndex < 0)
            selectedDefWeaponIndex = (weaponsIndexes.Count -1);

        defWeaponIndex = weaponsIndexes[selectedDefWeaponIndex];

        defaultWeaponDescriptionText.text = allBuffSettings[defWeaponIndex].GetName();
    }

    public void ConfirmSettings()
    {
        SetUp.ResetPickUpDictionary();

        for(int i = 0; i < allBuffSettings.Count; i++)
        {
            SetUp.AddPickUpPair(i, allBuffSettings[i].GetQuantity());
        }

        SetUp.SetStartWeapons(meleeWeaponIndex, defWeaponIndex);
    }

    public void GetSettings()
    {
        for (int i = 0; i < allBuffSettings.Count; i++)
        {
            allBuffSettings[i].SetQuantity(SetUp.GetQuantityFromPickUpPair(i));
        }

        defWeaponIndex = SetUp.defaultWeaponInex;
        defaultWeaponDescriptionText.text = allBuffSettings[defWeaponIndex].GetName();
    }




    //private int FindMeleeIndex()
    //{
    //    for (int i = 0; i < allBuffs.Count; i++)
    //    {
    //        if (allBuffs[i].TryGetComponent(out Melee melee))
    //        {
    //            return i;
    //        }
    //    }
    //
    //    return -1;
    //}
    //
    //private void GetWeaponsIndexes()
    //{
    //    for (int i = 0; i < allBuffs.Count; i++)
    //    {
    //        if (allBuffs[i].TryGetComponent(out Weapons weapon))
    //        {
    //            weaponsIndexes.Add(i);
    //        }
    //    }
    //}

    //[ContextMenu("Initialize PickUps options")]
    private void Initialize()
    {
        allBuffs = buffList_Prefab.GetComponent<BuffList>().allBuffs;

        for (int i = 0; i < allBuffs.Count; i++)
        {
            GameObject buff = Instantiate(buffQuantity_Prefab, menuContent.transform);
            BuffSetting buffSetting = buff.GetComponent<BuffSetting>();
            allBuffSettings.Add(buffSetting);

            string name = "Name missing";

            if (allBuffs[i].TryGetComponent(out Weapons weapon))
            {
                weaponsIndexes.Add(i);
                name = weapon.weaponName;

                if (weapon is Melee)
                {
                    meleeWeaponIndex = i;
                }
            }

            else if (allBuffs[i].TryGetComponent(out PickUpBuff other))
            {
                name = other.buffName;
            }

            buffSetting.SetName(name);
            //buffSetting.SetName("weap " +i);
        }
    }
}
