using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject ballPrefab;
    public float ballRange;
    public float ballSpeed;
    public float shootBallManaCost;
    public float ballBaseDmg;

    public void ShootBall()
    {
        if (!PlayerControl.instance.CheckSkillCost(shootBallManaCost))
        {
            return;
        }

        PlayerControl.instance.ReduceMana(shootBallManaCost);

        Vector3 startPos = PlayerControl.instance.RefinedPos(transform.position);

        Projectile proj = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
        proj.damage += ballBaseDmg + GetComponent<StatsManager>().attackDmg.value;
        Vector3 direction = Vector3.Normalize(GetComponent<SkillHandler>().skillTarget - startPos);

        proj.targetPos = startPos + direction * ballRange;
        proj.projSpeed = ballSpeed;
    }

    public GameObject blastColliderPrefab;
    public float maxBlastArea;
    public float expandTime;
    public float blastManaCost;
    public float blastBaseDmg;

    public void Blast()
    {
        if (!PlayerControl.instance.CheckSkillCost(blastManaCost))
        {
            return;
        }

        PlayerControl.instance.ReduceMana(blastManaCost);

        StartCoroutine(ExpandSphereCollider());
    }

    IEnumerator ExpandSphereCollider()
    {
        SphereCollider sphereCollider = Instantiate(blastColliderPrefab, PlayerControl.instance.RefinedPos(transform.position), Quaternion.identity).GetComponent<SphereCollider>();
        sphereCollider.GetComponent<AreaDamage>().damage += blastBaseDmg + GetComponent<StatsManager>().attackDmg.value;

        for (float i = 0; i <= expandTime + 0.1; i+=Time.deltaTime)
        {
            //sphereCollider.radius = Mathf.Lerp(0.5f, maxBlastArea, i / expandTime);
            //if (sphereCollider.radius >= maxBlastArea)
            //{
            //    Destroy(sphereCollider.gameObject);
            //    break;
            //}
            //yield return null;

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
