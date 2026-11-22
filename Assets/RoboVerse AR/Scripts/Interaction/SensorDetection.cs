using UnityEngine;

public class SensorDetection : MonoBehaviour
{
    public GameObject sensorBeam;

    public float detectionDistance = 5f;
    public float sensorRadius = 0.35f;
    public float delayTime = 0.4f;

    private bool currentState = false;
    private bool targetState = false;
    private float stateTimer = 0f;

    void Update()
    {
        Vector3 origin = transform.position
                       + Vector3.up * 2f
                       + Vector3.forward * 0.5f;

        RaycastHit[] hits = Physics.SphereCastAll(
            origin,
            sensorRadius,
            Vector3.forward,
            detectionDistance
        );

        bool detected = false;

        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.IsChildOf(transform))
            {
                detected = true;
                break;
            }
        }

        if (detected != targetState)
        {
            targetState = detected;
            stateTimer = 0f;
        }

        if (currentState != targetState)
        {
            stateTimer += Time.deltaTime;

            if (stateTimer >= delayTime)
            {
                currentState = targetState;
                stateTimer = 0f;
            }
        }

        sensorBeam.SetActive(currentState);
    }
}