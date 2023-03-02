using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPos;
    public float projSpeed;

    public float damage;
    public DamageType dmgType;

    private void Update()
    {
        if(transform.position == targetPos)
        {
            Destroy(gameObject);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed/100);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().ReceiveDamage(new Damage(damage, other.GetComponent<Character>(), dmgType));
            Destroy(gameObject);
        }
    }
}
