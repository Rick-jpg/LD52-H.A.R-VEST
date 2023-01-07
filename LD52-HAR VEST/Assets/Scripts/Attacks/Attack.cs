using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField]
    protected float energyCost;
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
    protected abstract void ReduceEnergy();

    public bool IsBeingUsed { get; private set; }
}
