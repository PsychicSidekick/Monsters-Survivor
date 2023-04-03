using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlastSkill : Skill
{
    public GameObject blastColliderPrefab;
    public float maxBlastArea;
    public float expandTime;
    public float blastManaCost;
    public float blastBaseDmg;

    public override void OnUse(Character _skillUser)
    {
        base.OnUse(_skillUser);
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FindGroundTarget();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("Blast");
    }

    public override void UseSkill()
    {
        if (!skillUser.CheckSkillCost(blastManaCost))
        {
            return;
        }

        skillUser.ReduceMana(blastManaCost);

        skillUser.StartCoroutine(ExpandSphereCollider());
    }

    IEnumerator ExpandSphereCollider()
    {
        SphereCollider sphereCollider = Instantiate(blastColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<SphereCollider>();
        sphereCollider.GetComponent<AreaDamage>().damage += blastBaseDmg + skillUser.GetComponent<StatsManager>().attackDmg.value;

        for (float i = 0; i <= expandTime + 0.1; i += Time.deltaTime)
        {
            float size = Mathf.Lerp(0.5f, maxBlastArea, i / expandTime);
            sphereCollider.transform.localScale = new Vector3(size, size, size);
            if (size >= maxBlastArea)
            {
                Destroy(sphereCollider.gameObject);
                break;
            }
            yield return null;
        }
    }
}
