using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Character targetCharacter;
    private Projectile projectile;

    private void Start()
    {
        targetCharacter = GetComponent<EffectCollider>().target;
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
        if (targetCharacter != null && targetCharacter.gameObject.activeInHierarchy)
        {
            projectile.targetPos = GameManager.instance.RefinedPos(targetCharacter.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChainsTo(Character newTargetCharacter)
    {
        targetCharacter = newTargetCharacter;
    }
}
