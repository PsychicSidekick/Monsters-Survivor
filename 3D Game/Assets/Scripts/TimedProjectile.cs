using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedProjectile : Projectile
{
    public Vector3 travelDirection;
    public float lifeTime;

    protected override void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        lifeTime -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + travelDirection, projSpeed / 100);
    }
}
