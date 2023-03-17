using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float BulletRange;

    Vector2 InitialPosition;

    public void Start()
    {
        InitialPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);

        DestroyBullet();
    }

    public void DestroyBullet()
    {
        if (Vector2.Distance(InitialPosition, transform.position) > BulletRange)
        {
            GameObject.Destroy(gameObject);
        }

    }
}