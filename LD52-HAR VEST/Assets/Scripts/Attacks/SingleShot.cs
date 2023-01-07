using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Attack
{
    [SerializeField]
    private Bullet bulletPrefab;

    public override void DoAttack()
    {
        isBeingUsed = true;
       Bullet newbullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
       newbullet.BulletDirection = attackDirection;
    }

    protected override void ReduceEnergy()
    {
        throw new System.NotImplementedException();
    }
}
