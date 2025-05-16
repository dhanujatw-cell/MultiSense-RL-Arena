// Assets/Scripts/BBoxSensorComponent.cs
using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;

[AddComponentMenu("ML Agents/Custom BBox Sensor")]
public class BBoxSensorComponent : SensorComponent
{
    public string sensorName = "AgentCamera";

    public override ISensor[] CreateSensors()
    {
        return new ISensor[] { new BBoxSensor(sensorName) };
    }


}

