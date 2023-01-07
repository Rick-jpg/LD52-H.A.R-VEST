using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShot : Attack
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    Transform[] bulletSpawnPoints;

    [SerializeField]
    PlayerController player;

    public override void DoAttack()
    {
        isBeingUsed = true;
        ReduceEnergy();
        StartCoroutine(AttackTime(startupTime, restoreTime));
    }

    IEnumerator AttackTime(float startupTime, float restoreTime)
    {
        player.SetCanMove(false);

        yield return new WaitForSeconds(startupTime);

        for (int i = 0; i < 2; i++)
        {
            Bullet newbullet = Instantiate(bulletPrefab, bulletSpawnPoints[i].position, transform.rotation);

            if (i == 0)
            {
                newbullet.Horizontal = false;
                newbullet.BulletDirection = attackDirection;
            }
            else if (i == 1)
            {
                newbullet.Horizontal = false;
                newbullet.BulletDirection = -attackDirection;
            }
        }

        yield return new WaitForSeconds(restoreTime);

        isBeingUsed = false;
        player.SetCanMove(true);
    }
}
