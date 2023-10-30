using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>())
        {
            if (GetComponent<EffectCollider>().owner.GetType() != other.GetComponent<EffectCollider>().owner.GetType())
            {
                Destroy(other.gameObject);
            }
        }
    }
}
