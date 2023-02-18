using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject ballPrefab;
    public float ballRange;
    public float ballSpeed;

    public void ShootBall()
    {
        Vector3 startPos = PlayerControl.instance.RefinedPos(transform.position);

        Projectile proj = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
        Vector3 direction = Vector3.Normalize(GetComponent<SkillHandler>().skillTarget - startPos);

        proj.targetPos = startPos + direction * ballRange;
        proj.projSpeed = ballSpeed;
    }

    public GameObject blastColliderPrefab;
    public float maxBlastArea = 2f;
    public float expandTime = 1;

    public void Blast()
    {
        StartCoroutine(ExpandSphereCollider());
    }

    IEnumerator ExpandSphereCollider()
    {
        SphereCollider sphereCollider = Instantiate(blastColliderPrefab, PlayerControl.instance.RefinedPos(transform.position), Quaternion.identity).GetComponent<SphereCollider>();

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


    //float currentTime = 0f;
    //float timeToExpand = 10f;

    //private void Update()
    //{
    //    SphereCollider sphereCollider = GetComponent<SphereCollider>();

    //    if (startBlastExpansion)
    //    {
    //        if (currentTime <= timeToExpand)
    //        {
    //            currentTime += Time.deltaTime;
    //            sphereCollider.radius += Mathf.Lerp(0.5f, maxBlastArea, currentTime/timeToExpand);
    //        }   

    //        if (sphereCollider.radius >= maxBlastArea)
    //        {
    //            startBlastExpansion = false;
    //            sphereCollider.radius = 0.5f;
    //        }
    //    }
    //}
}
