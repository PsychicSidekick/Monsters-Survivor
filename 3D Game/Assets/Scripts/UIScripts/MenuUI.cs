using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuPanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(!menuPanel.activeInHierarchy);
        }
    }

    public void ReturnOnClick()
    {
        menuPanel.SetActive(false);
    }

    public void ExitOnClick()
    {
        Debug.Log("Exit Game");
    }
}
