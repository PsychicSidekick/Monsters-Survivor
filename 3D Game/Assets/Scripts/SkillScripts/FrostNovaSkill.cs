using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FrostNovaSkill : Skill
{
    public GameObject novaColliderPrefab;
    public float maxNovaArea;
    public float expandTime;
    public float manaCost;

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
        if (!skillUser.CheckSkillCost(manaCost))
        {
            return;
        }

        skillUser.ReduceMana(manaCost);

        skillUser.StartCoroutine(ExpandSphereCollider());
    }

    IEnumerator ExpandSphereCollider()
    {
        SphereCollider sphereCollider = Instantiate(novaColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<SphereCollider>();
        sphereCollider.GetComponent<AreaEffect>().statusEffects.Add(new FreezeBuff(1, 100));

        for (float i = 0; i <= expandTime + 0.1; i += Time.deltaTime)
        {
            float size = Mathf.Lerp(0.5f, maxNovaArea, i / expandTime);
            sphereCollider.transform.localScale = new Vector3(size, size, size);
            if (size >= maxNovaArea)
            {
                Destroy(sphereCollider.gameObject);
                break;
            }
            yield return null;
        }
    }
}
