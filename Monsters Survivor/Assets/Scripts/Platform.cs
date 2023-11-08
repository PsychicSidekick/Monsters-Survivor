using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Platform : MonoBehaviour
{
    [Serializable]
    public struct MapPreset
    {
        public Material material;
        public Color color;
        public AudioClip bgm;
        public float bgmVolume;
    }

    public static List<Vector2Int> listOfExistingPlatforms = new List<Vector2Int>();
    public static Platform centerPlatform;
    public static Material surfaceMaterial;

    public Light enviromentLight;

    public List<MapPreset> mapPresets = new List<MapPreset>();

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

            // Choose and set random map preset
            int index = UnityEngine.Random.Range(0, mapPresets.Count);
            surfaceMaterial = mapPresets[index].material;
            enviromentLight.color = mapPresets[index].color;
            Camera.main.GetComponent<AudioSource>().clip = mapPresets[index].bgm;
            Camera.main.GetComponent<AudioSource>().volume = mapPresets[index].bgmVolume;
            Camera.main.GetComponent<AudioSource>().Play();

            CreateSurroundingPlatforms();
            // Build nav mesh
            centerPlatform.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        GetComponent<MeshRenderer>().material = surfaceMaterial;
    }

    private void CreateSurroundingPlatforms()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Skip centre
                if (i == 0 && j == 0)
                {
                    continue;
                }

                Vector2Int newPos = pos + new Vector2Int(i, j);

                // Skip if already exists
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
        // Create surrounding platforms if player enters
        if (other.GetComponent<Player>())
        {
            CreateSurroundingPlatforms();
            centerPlatform.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }
}
