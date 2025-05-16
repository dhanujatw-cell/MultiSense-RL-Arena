using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;

public class BBoxReceiver : MonoBehaviour
{
    private CameraSensorComponent sensorComponent;

    void Start()
    {
        sensorComponent = GetComponent<CameraSensorComponent>();

        var perceptionCamera = GetComponent<PerceptionCamera>();
        if (perceptionCamera == null)
        {
            Debug.LogError("[BBoxReceiver] PerceptionCamera not found.");
            return;
        }

        var bboxLabeler = perceptionCamera.labelers
            .OfType<BoundingBox2DLabeler>()
            .FirstOrDefault();

        if (bboxLabeler == null)
        {
            Debug.LogError("[BBoxReceiver] No BoundingBox2DLabeler found.");
            return;
        }

        bboxLabeler.boundingBoxesCalculated += OnBoundingBoxesCalculated;
    }

    void OnBoundingBoxesCalculated(BoundingBox2DLabeler.BoundingBoxesCalculatedEventArgs args)
    {
        var flatList = new List<float>();
        int count = 0;

        foreach (var bbox in args.data)
        {
            if (count >= 5) break;

            flatList.Add(bbox.label_id);
            flatList.Add(bbox.x);
            flatList.Add(bbox.y);
            flatList.Add(bbox.width);
            flatList.Add(bbox.height);

            // Debug.Log($"[BBoxReceiver] BBox {count}: Label={bbox.label_id}, x={bbox.x}, y={bbox.y}, w={bbox.width}, h={bbox.height}");
            count++;
        }
        // Debug.Log($"[BBoxReceiver] Final flatList: {string.Join(", ", flatList)}");

        sensorComponent?.UpdateBBoxObservation(flatList);
    }
}

