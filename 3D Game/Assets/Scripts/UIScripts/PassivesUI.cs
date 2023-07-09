using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivesUI : MonoBehaviour
{
    public GameObject skillPassiveTree;
    public GameObject playerPassiveTree;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            skillPassiveTree.SetActive(!skillPassiveTree.activeInHierarchy);
            playerPassiveTree.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerPassiveTree.SetActive(!playerPassiveTree.activeInHierarchy);
            skillPassiveTree.SetActive(false);
        }
    }
}
