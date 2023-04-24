using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPos;
    public float projSpeed;
    public int pierce;
    public float damage;
    public DamageType dmgType;

    protected virtual void Update()
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
            pierce--;
            other.GetComponent<Enemy>().ReceiveDamage(new Damage(damage, other.GetComponent<Character>(), dmgType));
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
