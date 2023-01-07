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
       ReduceEnergy();
        StartCoroutine(AttackTime(startupTime, restoreTime));
    }

    IEnumerator AttackTime(float startupTime, float restoreTime)
    {
        yield return new WaitForSeconds(startupTime);

        Bullet newbullet = Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        newbullet.BulletDirection = attackDirection;

        yield return new WaitForSeconds(restoreTime);

        isBeingUsed = false;
    }
}
