using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZombieLevelSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI answerText;
    [SerializeField] Slider slider;

    private readonly string[] answers = new string[] {"No!", "Few.", "Heaps.", "Zombiecalypse!!!" };

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if((int)slider.value > answers.Length)
        {
            Debug.Log("ERROR: Invalid NpcLevel !!!!");
            return;
        }

        answerText.text = answers[(int)slider.value];
    }
}
