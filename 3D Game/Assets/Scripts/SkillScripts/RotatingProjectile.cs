using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProjectile : Projectile
{
    public float lifeTime;
    public Transform center;
    public float range;
    public float rotationSpeed;

    protected override void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        if (center != null)
        {
            transform.position = center.position + (transform.position - center.position).normalized * range;
            transform.RotateAround(center.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
