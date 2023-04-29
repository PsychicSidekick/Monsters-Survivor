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

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.FindGroundTarget();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("Blast");
    }

    public override void UseSkill(Character skillUser)
    {
        if (!skillUser.CheckSkillCost(blastManaCost))
        {
            return;
        }

        skillUser.ReduceMana(blastManaCost);

        skillUser.StartCoroutine(ExpandSphereCollider(skillUser));
    }

    IEnumerator ExpandSphereCollider(Character skillUser)
    {
        SphereCollider sphereCollider = Instantiate(blastColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<SphereCollider>();
        sphereCollider.GetComponent<AreaEffect>().damage += blastBaseDmg + skillUser.GetComponent<StatsManager>().attackDmg.value;
        sphereCollider.GetComponent<AreaEffect>().statusEffects.Add(new FreezeBuff(1, 100));

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
