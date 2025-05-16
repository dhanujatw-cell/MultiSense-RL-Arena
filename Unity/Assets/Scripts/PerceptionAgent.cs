// Assets/Scripts/PerceptionAgent.cs

using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PerceptionAgent : Agent
{
    public override void Initialize()
    {
        // Optional: initialization logic
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Optional: add vector observations if needed
    }



    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Avoid crash: send dummy values
        var discreteActions = actionsOut.DiscreteActions;
        if (discreteActions.Length > 0)
            discreteActions[0] = 0;
    }

    void Start()
    {
        Debug.Log("[PerceptionAgent] Start() called. Agent initialized successfully.");
    }
    public override void OnActionReceived(ActionBuffers actions)
{
    int action = actions.DiscreteActions[0];
    Debug.Log($"[Agent] Received action: {action}");

    // Apply action logic, e.g.:
    // if (action == 0) DoNothing();
    // if (action == 1) MoveLeft();
    // if (action == 2) MoveRight();
}
}

