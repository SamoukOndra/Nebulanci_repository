using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuffSetting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText; //pridat do weapons a buffs
    [SerializeField] Slider quantitySlider;

    public void SetName(string name)
    {
        nameText.text = name;
    }
    public string GetName()
    {
        return nameText.text;
    }

    public void SetQuantity(int quantity)
    {
        Mathf.Clamp(quantity, quantitySlider.minValue, quantitySlider.maxValue);
        quantitySlider.value = quantity;
    }

    public int GetQuantity()
    {
        return ((int)quantitySlider.value);
    }
}
