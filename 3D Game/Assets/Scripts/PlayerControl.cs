using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 moveTarget = new Vector3(0, 1, 0);

    public float speed;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.transform.gameObject.tag == "Ground")
                {
                    moveTarget = new Vector3(hit.point.x, 1, hit.point.z);
                }
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed/100);
    }
}
