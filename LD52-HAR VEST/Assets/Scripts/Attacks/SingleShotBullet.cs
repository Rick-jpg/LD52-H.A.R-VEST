using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotBullet : Bullet
{

    public override void SetupDirection()
    {
        if (bulletDirection == 1)
        {
          movement = Vector3.right;
        }
        else
        {
          movement = Vector3.left;
        }
        
    }
}
