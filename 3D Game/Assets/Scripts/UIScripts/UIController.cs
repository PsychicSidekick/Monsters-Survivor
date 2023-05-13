using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject skillPassiveTree;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            skillPassiveTree.SetActive(!skillPassiveTree.activeInHierarchy);
        }
    }
}
