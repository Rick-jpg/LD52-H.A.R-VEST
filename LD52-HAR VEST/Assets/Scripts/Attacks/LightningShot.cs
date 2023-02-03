using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShot : Attack
{
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    Transform[] bulletSpawnPoints;

    [SerializeField]
    PlayerController player;

    public override void DoAttack()
    {
        isBeingUsed = true;
        AudioManager.Instance.PlaySound(1, 7);
        ReduceEnergy();
        StartCoroutine(AttackTime(startupTime, restoreTime));
    }

    IEnumerator AttackTime(float startupTime, float restoreTime)
    {
        player.SetCanMove(false);
        player.EnableGravity(false);

        yield return new WaitForSeconds(startupTime);

        AudioManager.Instance.PlaySound(1, 8);

        for (int i = 0; i < 2; i++)
        {
            Bullet newbullet = Instantiate(bulletPrefab, bulletSpawnPoints[i].position, transform.rotation);
            if (i == 0)
            {
                newbullet.BulletDirection = 1;
            }
            else if (i == 1)
            {
                newbullet.BulletDirection = -1;
            }

            newbullet.Speed = bulletSpeed;
        }

        yield return new WaitForSeconds(restoreTime);

        isBeingUsed = false;
        player.SetCanMove(true);
        player.EnableGravity(true);
    }
}
