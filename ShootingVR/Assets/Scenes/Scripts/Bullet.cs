using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float bulletSpeed = 20f;
    [SerializeField]
    private float deleteIntervalMax = 1f;
    private float deleteInterval;
    // Use this for initialization
    void Start()
    {
        deleteInterval = deleteIntervalMax;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
        DeleteBullet();
    }

    void BulletMove()
    {
        var velocity = transform.rotation * new Vector3(0, 0, bulletSpeed);
        transform.position += velocity * Time.deltaTime;
    }

    void DeleteBullet()
    {
        deleteInterval -= Time.deltaTime;
        if (deleteInterval < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
