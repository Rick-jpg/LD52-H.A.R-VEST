using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Energybar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_Text energyNumbersText;

    private void Start()
    {
        energyNumbersText = GetComponentInChildren<TMP_Text>();
    }

    public void ChangeText(float currentEnergy, float maxEnergy)
    {
        energyNumbersText.SetText($"{currentEnergy}/{maxEnergy}");
    }
}
