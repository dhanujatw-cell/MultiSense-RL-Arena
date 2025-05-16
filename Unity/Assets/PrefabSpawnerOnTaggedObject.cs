using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PrefabEntry
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float spawnProbability = 1f;
}

public class PrefabSpawnerOnTaggedObject : MonoBehaviour
{
    [Header("Prefabs to Spawn (with Probabilities)")]
    public List<PrefabEntry> prefabEntries;

    [Header("Target Tag")]
    public string targetTag = "SpawnPoint";

    [Header("Spawn Settings")]
    public float spawnDelay = 2f;
    public int numberToSpawn = GameSettings.NumberOfHumans;

    [Header("Spawn Height Offset")]
    public float spawnHeightOffset = 1.0f;

    void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length == 0)
        {
            Debug.LogError($"❌ No GameObjects with tag '{targetTag}' found.");
            yield break;
        }

        if (prefabEntries.Count == 0)
        {
            Debug.LogError("❌ No prefabs assigned.");
            yield break;
        }

        // Shuffle targets
        List<GameObject> shuffled = new List<GameObject>(targets);
        Shuffle(shuffled);

        int spawnCount = Mathf.Min(numberToSpawn, shuffled.Count);
        Debug.Log($"number from the setting : {numberToSpawn}    spawned humans : {spawnCount}");
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject target = shuffled[i];

            GameObject selectedPrefab = ChooseRandomPrefab();
            if (selectedPrefab == null) continue;

            Vector3 basePosition = target.transform.position;
            basePosition.y += spawnHeightOffset;

            GameObject spawned = Instantiate(selectedPrefab, basePosition, Quaternion.identity);
            spawned.name = selectedPrefab.name + $"_Spawned_{i + 1}";
        }
    }

    GameObject ChooseRandomPrefab()
    {
        float totalWeight = 0f;
        foreach (var entry in prefabEntries)
        {
            totalWeight += entry.spawnProbability;
        }

        float randomValue = Random.value * totalWeight;
        float cumulative = 0f;

        foreach (var entry in prefabEntries)
        {
            cumulative += entry.spawnProbability;
            if (randomValue <= cumulative)
                return entry.prefab;
        }

        return null; // fallback
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
