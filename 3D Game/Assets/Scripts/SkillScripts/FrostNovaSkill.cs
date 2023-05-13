using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FrostNovaSkill : Skill
{
    public GameObject novaColliderPrefab;
    public float maxNovaArea;
    public float expandTime;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.FindGroundTarget();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("Blast");
    }

    public override void UseSkill(Character skillUser)
    {
        skillUser.StartCoroutine(ExpandSphereCollider(skillUser));
    }

    IEnumerator ExpandSphereCollider(Character skillUser)
    {
        EffectCollider novaCollider = Instantiate(novaColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<EffectCollider>();
        novaCollider.SetEffects(0, DamageType.Cold, false, skillUser, null, new FreezeBuff(skillUser, 1, 100));
            
        for (float i = 0; i <= expandTime + 0.1; i += Time.deltaTime)
        {
            float size = Mathf.Lerp(0.5f, maxNovaArea, i / expandTime);
            novaCollider.transform.localScale = new Vector3(size, size, size);
            if (size >= maxNovaArea)
            {
                Destroy(novaCollider.gameObject);
                break;
            }
            yield return null;
        }
    }
}
