using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(-4.5f, 12, -4.5f);
        transform.LookAt(player.transform);
    }
}
