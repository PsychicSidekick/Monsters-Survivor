using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager instance;

    public NavMeshSurface centerPlatform;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateNavMesh()
    {
        centerPlatform.BuildNavMesh();
    }
}
