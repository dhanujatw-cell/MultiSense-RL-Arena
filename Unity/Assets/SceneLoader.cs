using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public Light directionalLight;

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = FindObjectOfType<Light>();
            if (directionalLight == null || directionalLight.type != LightType.Directional)
            {
                Debug.LogError("❌ No directional light found in the scene.");
                return;
            }
        }

        directionalLight.intensity = GameSettings.LightingIntensity;
        Debug.Log($"Found light: {directionalLight.name}, Type: {directionalLight.type}, Intensity: {directionalLight.intensity}, Enabled: {directionalLight.enabled}");

        string envName = GameSettings.SelectedEnvironment;
        string envPath = "Environment/" + envName;
        GameObject prefab = Resources.Load<GameObject>(envPath);

        if (prefab != null)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
            Debug.Log($"✅ Spawned {envName} in ActiveScene.");
        }
        else
        {
            Debug.LogError("❌ Could not load prefab: " + envPath);
        }
    }
}
