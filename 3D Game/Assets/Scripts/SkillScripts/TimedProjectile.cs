using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedProjectile : MonoBehaviour
{
    public Vector3 travelDirection;
    public float lifeTime;
    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        lifeTime -= Time.deltaTime;
        projectile.targetPos = transform.position + travelDirection;
    }
}
