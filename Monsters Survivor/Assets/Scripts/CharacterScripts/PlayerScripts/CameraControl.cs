using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = Player.instance.gameObject;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        transform.position = player.transform.position + new Vector3(-2.75f, 8, -2.75f);
        transform.LookAt(player.transform);
    }
}
