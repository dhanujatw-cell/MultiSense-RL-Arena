using UnityEngine;
using Unity.MLAgents;
using UnityEngine.SceneManagement;

public class CameraAgentController : MonoBehaviour
{
    public float rotationAngle = 10f;
    public Camera agentCamera;
    public float timeBetweenDecisionsAtInference = 0.1f;

    private float m_TimeSinceDecision;
    private Agent m_Agent;

    void OnEnable()
    {
        m_Agent = GetComponent<Agent>();
        transform.rotation = Quaternion.identity;
    }

    public void RotateAgent(int direction)
    {
        // direction: -1 = left, 0 = stay, 1 = right
        transform.Rotate(0f, direction * rotationAngle, 0f);

        // Add small penalty for each action to encourage minimal movement
        m_Agent.AddReward(-0.01f);

        // Sample reward condition (customize as needed)
        float viewTargetScore = CheckViewTarget();
        m_Agent.AddReward(viewTargetScore);

        // Optionally end episode
        if (Mathf.Abs(viewTargetScore - 1.0f) < 0.01f)
        {
            m_Agent.EndEpisode();
        }
    }

    float CheckViewTarget()
    {
        // Add your own logic to reward the agent for rotating to face an object
        // E.g., raycast or color match in the center of camera view
        return 0.0f;
    }

    void FixedUpdate()
    {
        WaitTimeInference();
    }

    void WaitTimeInference()
    {
        if (m_Agent == null) return;

        if (Academy.Instance.IsCommunicatorOn)
        {
            m_Agent.RequestDecision();
        }
        else
        {
            if (m_TimeSinceDecision >= timeBetweenDecisionsAtInference)
            {
                m_TimeSinceDecision = 0f;
                m_Agent.RequestDecision();
            }
            else
            {
                m_TimeSinceDecision += Time.fixedDeltaTime;
            }
        }
    }

    public void ResetAgent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

