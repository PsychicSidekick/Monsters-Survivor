using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Platform : MonoBehaviour
{
    [Serializable]
    public struct MaterialLightColor
    {
        public Material material;
        public Color color;
    }

    public static List<Vector2Int> listOfExistingPlatforms = new List<Vector2Int>();
    public static Platform centerPlatform;
    public static Material surfaceMaterial;

    public Light enviromentLight;

    public List<MaterialLightColor> materialLightColors = new List<MaterialLightColor>();

    public Vector2Int pos;

    public GameObject platformPrefab;

    private void Start()
    {
        listOfExistingPlatforms.Add(pos);
        if (pos == new Vector2Int(0, 0))
        {
            listOfExistingPlatforms = new List<Vector2Int>();
            listOfExistingPlatforms.Add(pos);
            centerPlatform = this;
            int index = UnityEngine.Random.Range(0, materialLightColors.Count);
            surfaceMaterial = materialLightColors[index].material;
            enviromentLight.color = materialLightColors[index].color;
            CreatePlatforms();
            centerPlatform.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        GetComponent<MeshRenderer>().material = surfaceMaterial;
    }

    private void CreatePlatforms()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                Vector2Int newPos = pos + new Vector2Int(i, j);

                if (listOfExistingPlatforms.Contains(newPos))
                {
                    continue;
                }

                listOfExistingPlatforms.Add(newPos);
                Platform newPlatform = Instantiate(platformPrefab, transform.position + new Vector3(i * 10 * platformPrefab.transform.localScale.x, 0, j * 10 * platformPrefab.transform.localScale.z), Quaternion.identity).GetComponent<Platform>();

                newPlatform.pos = newPos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            CreatePlatforms();
            centerPlatform.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("HI");
    //}
}
