using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Attack
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    Transform bulletSpawn;

    public override void DoAttack()
    {
       isBeingUsed = true;
       Bullet newbullet = Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
       newbullet.BulletDirection = attackDirection;
    }

}
