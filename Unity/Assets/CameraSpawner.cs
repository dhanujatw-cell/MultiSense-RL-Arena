using UnityEngine;
using System.Collections.Generic;

public class CameraSpawner : MonoBehaviour
{
    [Header("Prefab to Spawn at Camera Points")]
    public GameObject prefabToSpawn;

    void Start()
    {
        Transform[] spawnPoints = SceneSettings.CameraTransforms;

        if (prefabToSpawn == null)
        {
            Debug.LogError("❌ No prefab assigned.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("❌ SceneSettings.CameraTransforms is empty or not set.");
            return;
        }

        int numberToSpawn = Mathf.Clamp(GameSettings.NumberOfCameras, 0, spawnPoints.Length);

        // Convert to list and shuffle
        List<Transform> shuffled = new List<Transform>(spawnPoints);
        Shuffle(shuffled);

        for (int i = 0; i < numberToSpawn; i++)
        {
            Transform point = shuffled[i];

            GameObject spawned = Instantiate(prefabToSpawn, point.position, point.rotation);
            spawned.transform.rotation = point.rotation; 
            spawned.name = $"Camera_{i + 1}";
        }

        Debug.Log($"✅ Spawned {numberToSpawn} cameras randomly from {spawnPoints.Length} available points.");
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
