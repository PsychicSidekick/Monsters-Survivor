using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    public Character targetCharacter;

    protected override void Update()
    {
        if (targetCharacter != null)
        {
            targetPos = GameManager.instance.RefinedPos(targetCharacter.transform.position);
        }
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
        base.Update();
    }
}
