using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlastSkill : Skill
{
    public GameObject blastColliderPrefab;
    public float maxBlastArea;
    public float expandTime;
    public float blastBaseDmg;

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
        EffectCollider blastCollider = Instantiate(blastColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<EffectCollider>();
        blastCollider.SetEffects(blastBaseDmg, DamageType.Fire, false, skillUser, null);

        for (float i = 0; i <= expandTime + 0.1; i += Time.deltaTime)
        {
            float size = Mathf.Lerp(0.5f, maxBlastArea, i / expandTime);
            blastCollider.transform.localScale = new Vector3(size, size, size);
            if (size >= maxBlastArea)
            {
                Destroy(blastCollider.gameObject);
                break;
            }
            yield return null;
        }
    }
}
