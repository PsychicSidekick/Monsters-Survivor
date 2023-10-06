using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCenter : MonoBehaviour
{
    public Transform movingTarget;
    public float rotationSpeed;
    public float lifeTime;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0 || !movingTarget.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
        transform.position = GameManager.instance.RefinedPos(movingTarget.position);
        gameObject.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.World);
    }
}
