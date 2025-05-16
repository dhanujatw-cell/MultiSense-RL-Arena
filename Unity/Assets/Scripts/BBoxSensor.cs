using System.Collections.Generic;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BBoxSensor : ISensor
{
    private const int maxBoxes = 5;
    private const int valuesPerBox = 5;
    private const int totalObservationSize = maxBoxes * valuesPerBox;

    private string sensorName;
    private float[] observationBuffer = new float[totalObservationSize];

    public BBoxSensor(string name)
    {
        sensorName = name;
    }

    public void UpdateBBoxes(List<float> bboxData)
    {
        for (int i = 0; i < totalObservationSize; i++)
            observationBuffer[i] = 0f;

        int count = Mathf.Min(bboxData.Count, totalObservationSize);
        for (int i = 0; i < count; i++)
            observationBuffer[i] = bboxData[i];
    }

    public string GetName() => sensorName;

    public ObservationSpec GetObservationSpec()
        => ObservationSpec.Vector(totalObservationSize);

    public int Write(ObservationWriter writer)
    {
        for (int i = 0; i < totalObservationSize; i++)
            writer[i] = observationBuffer[i];
        return totalObservationSize;
    }

    public byte[] GetCompressedObservation() => null;
    public void Reset() { }
    public CompressionSpec GetCompressionSpec() => CompressionSpec.Default();
    public SensorCompressionType GetCompressionType() => SensorCompressionType.None;
    public void Update() { }
}

