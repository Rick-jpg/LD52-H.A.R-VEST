using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Energybar : MonoBehaviour
{
    [SerializeField]
    private int maxEnergyCapacity;
    [SerializeField]
    private int currentEnergy;

    int energyExtraAdded = 2;

    [Header("UI")]
    [SerializeField]
    private TMP_Text energyNumbersText;

    private void OnEnable()
    {
        EnergyEndMachine.OnCompleteLevel += SetMaximumEnergy;
        Attack.OnEnergyUsed += DecreaseEnergy;
    }

    private void Start()
    {
        currentEnergy = maxEnergyCapacity;
        energyNumbersText = GetComponentInChildren<TMP_Text>();
        ChangeText();
    }

    void SetMaximumEnergy()
    {
        maxEnergyCapacity += energyExtraAdded;
        currentEnergy = maxEnergyCapacity;
        ChangeText();
    }

    void DecreaseEnergy(int amount)
    {
        int difference = currentEnergy - amount;
        if(difference < 0) difference = 0;
        currentEnergy = difference;
        ChangeText();
    }

    void ChangeText()
    {
        energyNumbersText.SetText($"{currentEnergy}/{maxEnergyCapacity}");
    }

    private void OnDisable()
    {
        EnergyEndMachine.OnCompleteLevel -= SetMaximumEnergy;
        Attack.OnEnergyUsed -= DecreaseEnergy;
    }
}
