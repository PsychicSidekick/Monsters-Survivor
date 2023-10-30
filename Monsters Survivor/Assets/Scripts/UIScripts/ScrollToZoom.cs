using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollToZoom : MonoBehaviour
{
    public float maxSize;
    public float minSize;

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                if (gameObject.transform.localScale.x < maxSize)
                {
                    gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(0.1f, 0.1f, 0);
                }
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                if (gameObject.transform.localScale.x > minSize)
                {
                    gameObject.transform.localScale = gameObject.transform.localScale - new Vector3(0.1f, 0.1f, 0);
                }
            }
        }
    }
}
