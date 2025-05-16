using UnityEngine;
using Unity.Collections;
using UnityEngine.Perception.GroundTruth;

public class PerceptionEventHook : MonoBehaviour
{
    private PerceptionCamera perceptionCamera;

    void Awake()
    {
        perceptionCamera = GetComponent<PerceptionCamera>();
        if (perceptionCamera == null)
        {
            Debug.LogError("PerceptionCamera component not found on this GameObject.");
        }
    }

    void OnEnable()
    {
        if (perceptionCamera != null)
        {
            perceptionCamera.RenderedObjectInfosCalculated += OnRenderedObjectInfosCalculated;
        }
    }

    void OnDisable()
    {
        if (perceptionCamera != null)
        {
            perceptionCamera.RenderedObjectInfosCalculated -= OnRenderedObjectInfosCalculated;
        }
    }

    void OnRenderedObjectInfosCalculated(int frameCount, NativeArray<RenderedObjectInfo> renderedObjectInfos)
    {
        // ✅ Call the AgentBridge to store bounding boxes
        AgentBridge.Instance?.SetBoundingBoxes(renderedObjectInfos);
    }
}

