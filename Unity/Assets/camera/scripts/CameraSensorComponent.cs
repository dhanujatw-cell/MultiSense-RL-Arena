using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Sensors;

public class CameraSensorComponent : SensorComponent
{
    public Camera agentCamera;
    public int imageWidth = 84;
    public int imageHeight = 84;
    public bool grayscale = false;

    public string bboxSensorName = "AgentCamera_bbox";
    private BBoxSensor bboxSensor;

    public override ISensor[] CreateSensors()
    {
        bboxSensor = new BBoxSensor(bboxSensorName);
        return new ISensor[]
        {
            new CameraSensor(
                agentCamera,
                imageWidth,
                imageHeight,
                grayscale,
                gameObject.name + "_CameraSensor",
                SensorCompressionType.None,
                ObservationType.Default
            ),
            bboxSensor
        };
    }

    public void UpdateBBoxObservation(List<float> bboxList)
    {
        bboxSensor?.UpdateBBoxes(bboxList);
    }
}

