using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private int bulletDirection;
    Vector3 movement;
    private void Update()
    {
        if(this.bulletDirection == 1)
        {
            movement = Vector3.right;
        }
        else
        {
            movement = Vector3.left;
        }
        transform.Translate(movement * speed * Time.deltaTime);
    }

    public int BulletDirection{ get; set; }
}
