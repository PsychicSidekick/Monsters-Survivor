using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    //[Serializable]
    //public struct MaterialLightColor
    //{
    //    public Material material;
    //    public Color color;
    //}

    //public static NavMeshManager instance;

    //public NavMeshSurface centerPlatform;
    //public Material surfaceMaterial;
    //public Light enviromentLight;
    //public List<MaterialLightColor> materialLightColors = new List<MaterialLightColor>();
    //public List<Vector2Int> listOfExistingPlatforms = new List<Vector2Int>();

    //private void Awake()
    //{
    //    instance = this;
    //    listOfExistingPlatforms = new List<Vector2Int>();
    //    listOfExistingPlatforms.Add(new Vector2Int(0, 0));
    //    centerPlatform = GetComponent<NavMeshSurface>();
    //    int index = UnityEngine.Random.Range(0, materialLightColors.Count);
    //    surfaceMaterial = materialLightColors[index].material;
    //    enviromentLight.color = materialLightColors[index].color;
    //    centerPlatform.GetComponent<Platform>().CreatePlatforms();
    //}

    //private void Start()
    //{

    //}

    //public void UpdateNavMesh()
    //{
    //    centerPlatform.BuildNavMesh();
    //}
}
