using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void Update()
    {
        transform.position = Player.instance.transform.position;
    }
}
