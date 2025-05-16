using Unity.MLAgents.Actuators;
using UnityEngine;

public class CameraActuatorComponent : ActuatorComponent
{
    public CameraAgentController controller;

    public override ActionSpec ActionSpec => ActionSpec.MakeDiscrete(3);

    public override IActuator[] CreateActuators()
    {
        return new IActuator[] { new CameraActuator(controller) };
    }
}

public class CameraActuator : IActuator
{
    private CameraAgentController controller;
    private ActionSpec spec = ActionSpec.MakeDiscrete(3);

    public CameraActuator(CameraAgentController ctrl)
    {
        controller = ctrl;
    }

    public string Name => "CameraActuator";
    public ActionSpec ActionSpec => spec;

    public void OnActionReceived(ActionBuffers actionBuffers)
    {
        int move = actionBuffers.DiscreteActions[0];
        int direction = 0;

        if (move == 1) direction = -1;
        else if (move == 2) direction = 1;

        controller.RotateAgent(direction);
    }

    public void Heuristic(in ActionBuffers actionBuffersOut)
    {
        var discreteActions = actionBuffersOut.DiscreteActions;
        float input = Input.GetAxis("Horizontal");
        if (Mathf.Approximately(input, 0f))
            discreteActions[0] = 0;
        else
            discreteActions[0] = input < 0 ? 1 : 2;
    }

    public void WriteDiscreteActionMask(IDiscreteActionMask actionMask) { }

    public void ResetData() { }
}

