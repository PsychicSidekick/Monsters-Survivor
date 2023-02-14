using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject ballPrefab;
    public float ballRange;
    public float ballSpeed;

    public void ShootBall()
    {
        PlayerControl.instance.StopMoving();
        Vector3 startPos = PlayerControl.instance.RefinedPos(transform.position);

        Projectile proj = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
        Vector3 direction = Vector3.Normalize(PlayerControl.instance.skillTarget - startPos);

        proj.targetPos = startPos + direction * ballRange;
        proj.projSpeed = ballSpeed;
    }

    public void Blast()
    {
        Collider sphereCollider = GetComponent<SphereCollider>();
    }
}
