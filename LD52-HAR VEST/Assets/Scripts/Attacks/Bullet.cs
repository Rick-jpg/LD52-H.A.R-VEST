using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    bool horizontal = true;
    [SerializeField]
    bool wallPhase = false;
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

    private void OnTriggerEnter(Collider col)
    {
        //No work
        Debug.Log("1");
        //if (!wallPhase)
        //{
        //    Debug.Log("2");
        //    if (col.gameObject.layer == 2)
        //    {
        //        Debug.Log("3");
        //        Destroy(this.gameObject);
        //    }
        //    else if (col.gameObject.layer == 7)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    public float Speed { set { speed = value; } }
    public bool WallPhase { set { wallPhase = value; } }
    public int BulletDirection { set { bulletDirection = value; } }
    public bool Horizontal { set { horizontal = value; } }
}
