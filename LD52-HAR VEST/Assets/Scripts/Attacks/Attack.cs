using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public delegate void EnergyUsed(int amount);
    public static EnergyUsed OnEnergyUsed;
    [SerializeField]
    protected int energyCost;
    [SerializeField]
    protected float windupTime;
    [SerializeField]
    protected float restoreTime;
    protected bool isBeingUsed;
    protected int attackDirection;

    public void SetDirection(int dir)
    {
        attackDirection = dir;
    }

    public abstract void DoAttack();
    protected void ReduceEnergy()
    {
        OnEnergyUsed?.Invoke(energyCost);
    }

    public bool IsBeingUsed { get; private set; }
    public int EnergyCost { get { return energyCost; } }
}
