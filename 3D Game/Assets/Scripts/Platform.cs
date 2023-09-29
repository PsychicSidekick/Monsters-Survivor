using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Platform : MonoBehaviour
{
    public static List<Vector2Int> listOfExistingPlatforms = new List<Vector2Int>();

    public Vector2Int pos;

    public GameObject platformPrefab;

    private void Start()
    {
        listOfExistingPlatforms.Add(pos);
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
        if (other.GetComponent<PlayerControl>())
        {
            CreatePlatforms();
            NavMeshManager.instance.UpdateNavMesh();
        }
    }
}
