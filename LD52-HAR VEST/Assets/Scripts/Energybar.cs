using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_Text energyNumbersText;
    [SerializeField]
    Image energyBarFill;

    private void Start()
    {
        energyNumbersText = GetComponentInChildren<TMP_Text>();
    }

    public void ChangeText(float currentEnergy, float maxEnergy)
    {
        energyNumbersText.SetText($"{currentEnergy}/{maxEnergy}");
    }

    public void UpdateEnergyBarFill(float value)
    {
        energyBarFill.fillAmount = value;
    }
}
