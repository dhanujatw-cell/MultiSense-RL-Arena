using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;

public class AgentBridge : MonoBehaviour
{
    public static AgentBridge Instance;

    public List<BoundingBoxValue> currentBoundingBoxes = new();

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Called by the PerceptionCamera callback to update current frame's bounding boxes.
    /// </summary>
    /// <param name="infos">Rendered object infos from the labelers</param>
    public void SetBoundingBoxes(NativeArray<RenderedObjectInfo> infos)
    {
        currentBoundingBoxes.Clear();
        Debug.Log($"[Dhanuja] ID: {infos}");
        foreach (var info in infos)
        {
            var box = new BoundingBoxValue
            {
                x = info.boundingBox.x,
                y = info.boundingBox.y,
                width = info.boundingBox.width,
                height = info.boundingBox.height,
                label_id = (int)info.instanceId
            };

            currentBoundingBoxes.Add(box);

            // ✅ Log each bounding box to the Unity Console
            Debug.Log($"[BBox] ID: {box.label_id}, x: {box.x}, y: {box.y}, w: {box.width}, h: {box.height}");
        }
    }
}

