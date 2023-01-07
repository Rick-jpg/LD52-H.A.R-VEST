using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    bool horizontal = true;
    private int bulletDirection;
    [SerializeField]
    private float spawnTime = 4f;
    private float spawnTimer;
    Vector3 movement;
    private void Start()
    {
        if (bulletDirection == 1)
        {
            if (horizontal)
            {
                movement = Vector3.right;
            }
            else
            {
                movement = Vector3.up;
            }
        }
        else
        {
            if (horizontal)
            {
                movement = Vector3.left;
            }
            else
            {
                movement = Vector3.down;
            }
        }
    }
    private void Update()
    {
        if(spawnTimer >= spawnTime)
        {
            spawnTimer = 0;
            Destroy(this.gameObject);
        }
        transform.Translate(movement * speed * Time.deltaTime);
        spawnTimer += Time.deltaTime;
    }

    public int BulletDirection { set { bulletDirection = value; } }
    public bool Horizontal { set { horizontal = value; } }
}
