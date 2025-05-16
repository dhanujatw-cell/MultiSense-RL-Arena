using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LauncherUI : MonoBehaviour
{
    [Header("UI Inputs")]
    public TMP_Dropdown envDropdown;
    public TMP_InputField humanCountInput;
    public Slider lightingSlider;
    public TMP_Dropdown numCameras;

    public void LaunchGame()
    {
        // Environment selection
        string selectedEnvName = envDropdown.options[envDropdown.value].text;
        GameSettings.SelectedEnvironment = selectedEnvName;

        int selectedIndex = numCameras.value;
        string label = numCameras.options[selectedIndex].text;
        if (!int.TryParse(label, out int cameraCount))
        {
            cameraCount = 1; // Default fallback
        }
        GameSettings.NumberOfCameras = cameraCount;

        // Number of humans
        int numHumans = 0;
        if (!int.TryParse(humanCountInput.text, out numHumans))
        {
            numHumans = 1; // Default if input is invalid
        }
        GameSettings.NumberOfHumans = Mathf.Max(0, numHumans);  // Prevent negatives
        Debug.Log($"humans in the scene {GameSettings.NumberOfHumans}");

        // Lighting level
        GameSettings.LightingIntensity = lightingSlider.value;

        // Debug.Log($"🚀 Launching ActiveScene with settings:\n" +
        //           $"- Environment: {selectedEnvName}\n" +
        //           $"- Humans: {GameSettings.NumberOfHumans}\n" +
        //           $"- Lighting: {GameSettings.LightingIntensity}");

        SceneManager.LoadScene("ActiveScene");
    }
}
