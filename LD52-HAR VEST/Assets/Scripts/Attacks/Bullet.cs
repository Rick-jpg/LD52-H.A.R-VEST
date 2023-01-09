using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    protected int bulletDirection;
    [SerializeField]
    private float spawnTime = 4f;
    private float spawnTimer;
    protected Vector3 movement;

    [SerializeField]
    [Tooltip("Insert the number of the layer that the bullet should collide with")]
    int[] collidedLayers = new int[0];

    public abstract void SetupDirection();
    protected void Start()
    {
        SetupDirection();
    }

    protected void Update()
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
        if (collidedLayers.Length == 0) return;
        foreach (int i in collidedLayers)
        {
            if (col.gameObject.layer == i)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public float Speed { set { speed = value; } }
    public int BulletDirection { set { bulletDirection = value; } }
}
