using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAttack : Attack
{
    [SerializeField]
    LayerMask solids;

    [SerializeField]
    GameObject teleportPoint;
    [SerializeField]
    GameObject player;

    public override void DoAttack()
    {
        isBeingUsed = true;
        StartCoroutine(AttackTime(startupTime, restoreTime));
    }

    IEnumerator AttackTime(float startupTime, float restoreTime)
    {
        PlayerController p = player.GetComponent<PlayerController>();
        p.SetCanMove(false);
        p.EnableGravity(false);

        yield return new WaitForSeconds(startupTime);

        // check if teleportPoint is colliding with a solid
        Collider box = teleportPoint.GetComponent<BoxCollider>();
        bool canTeleport = Physics.OverlapBox(box.bounds.center, box.bounds.extents, box.transform.rotation, solids).Length == 0;

        Debug.Log(canTeleport);

        // if not, teleport and reduce energy
        if (canTeleport)
        {
            ReduceEnergy();
            Vector3 newPosition = teleportPoint.transform.position;
            player.transform.position = newPosition;
        }
        // if it is colliding, don't teleport and display feedback
        else
        {
            Debug.Log("Cannot teleport");
        }

        yield return new WaitForSeconds(restoreTime);

        isBeingUsed = false;
        p.SetCanMove(true);
        p.EnableGravity(true);
    }
}
