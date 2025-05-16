// Assets/Scripts/PerceptionHandler.cs
using UnityEngine;
using Unity.Collections;
using UnityEngine.Perception.GroundTruth;

public class PerceptionHandler : MonoBehaviour
{
    public PerceptionCamera perceptionCamera;

    void OnEnable()
    {
        perceptionCamera.RenderedObjectInfosCalculated += OnRenderedObjectInfosCalculated;
    }

    void OnDisable()
    {
        perceptionCamera.RenderedObjectInfosCalculated -= OnRenderedObjectInfosCalculated;
    }

    void OnRenderedObjectInfosCalculated(int frameCount, NativeArray<RenderedObjectInfo> infos)
    {
        if (AgentBridge.Instance != null)
            AgentBridge.Instance.SetBoundingBoxes(infos);
    }
}

