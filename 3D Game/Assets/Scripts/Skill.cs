using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public GameObject projPrefab;
    public float range;

    public void UseSkill(Vector3 startPos, Vector3 targetPos, float projSpeed)
    {
        Projectile proj = Instantiate(projPrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
        Vector3 direction = Vector3.Normalize(targetPos - startPos);

        proj.targetPos = startPos + direction * range;
        proj.projSpeed = projSpeed;
    }
}
