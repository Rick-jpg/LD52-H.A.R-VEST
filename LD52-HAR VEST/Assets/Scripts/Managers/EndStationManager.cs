using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndStationManager : MonoBehaviour
{
    private List<EnergyEndMachine> machines = new List<EnergyEndMachine>();

    private void OnEnable()
    {
        RespawnManager.onResetLevel += ResetMachines;
    }

    private void OnDisable()
    {
        RespawnManager.onResetLevel -= ResetMachines;
    }

    private void Start()
    {
        machines = GetComponentsInChildren<EnergyEndMachine>().ToList();
    }

    private void ResetMachines()
    {
        foreach (EnergyEndMachine machine in machines)
        {
            machine.ResetMachine();
        }
    }


}
