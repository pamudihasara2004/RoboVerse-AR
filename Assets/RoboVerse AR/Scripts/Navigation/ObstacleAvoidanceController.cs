using UnityEngine;
using TMPro;

public class ObstacleAvoidanceController : MonoBehaviour
{
    [Header("Robot")]
    public Transform robot;

    [Header("Navigation")]
    public Transform[] waypoints;
    public Transform destination;

    [Header("Sensor")]
    public float detectionDistance = 3f;
    public LineRenderer sensorRay;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float waypointDistance = 0.15f;

    [Header("UI")]
    public TMP_Text statusText;

    private int currentWaypoint = 0;

    private bool simulationRunning = false;
    private bool destinationReached = false;

    void Start()
    {
        ResetSimulation();
    }

    void Update()
    {
        if (!simulationRunning || destinationReached)
            return;

        DetectObstacle();

        MoveRobot();
    }

    void DetectObstacle()
    {
        Vector3 origin = robot.position;

        Vector3 direction = robot.forward;

        RaycastHit hit;

        bool obstacleDetected = Physics.Raycast(
            origin,
            direction,
            out hit,
            detectionDistance
        );

        if (obstacleDetected &&
            hit.collider.CompareTag("Obstacle"))
        {
            sensorRay.startColor = Color.red;
            sensorRay.endColor = Color.red;

            statusText.text =
                "OBSTACLE DETECTED!\n" +
                "Robot is following a safe path.";

            Debug.DrawRay(
                origin,
                direction * hit.distance,
                Color.red
            );
        }
        else
        {
            sensorRay.startColor = Color.cyan;
            sensorRay.endColor = Color.cyan;

            statusText.text =
                "PATH CLEAR\n" +
                "Robot is moving safely.";

            Debug.DrawRay(
                origin,
                direction * detectionDistance,
                Color.cyan
            );
        }

        sensorRay.SetPosition(0, origin);

        if (obstacleDetected &&
            hit.collider.CompareTag("Obstacle"))
        {
            sensorRay.SetPosition(1, hit.point);
        }
        else
        {
            sensorRay.SetPosition(
                1,
                origin + direction * detectionDistance
            );
        }
    }

    void MoveRobot()
    {
        if (currentWaypoint >= waypoints.Length)
        {
            destinationReached = true;

            statusText.text = "DESTINATION REACHED!";

            return;
        }

        Vector3 targetPosition =
            waypoints[currentWaypoint].position;

        Vector3 direction =
            targetPosition - robot.position;

        direction.y = 0;

        if (direction.magnitude <= waypointDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                destinationReached = true;

                statusText.text =
                    "DESTINATION REACHED!";

                return;
            }

            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        robot.rotation = Quaternion.Slerp(
            robot.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        robot.position = Vector3.MoveTowards(
            robot.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    public void StartSimulation()
    {
        simulationRunning = true;

        statusText.text = "ROBOT NAVIGATION STARTED!";
    }

    public void PauseSimulation()
    {
        simulationRunning = false;

        statusText.text = "SIMULATION PAUSED.";
    }

    public void ResetSimulation()
    {
        simulationRunning = false;

        destinationReached = false;

        currentWaypoint = 0;

        robot.position = new Vector3(0, 0.25f, -3);

        robot.rotation = Quaternion.identity;

        sensorRay.enabled = true;

        sensorRay.startColor = Color.cyan;
        sensorRay.endColor = Color.cyan;

        statusText.text =
            "READY TO NAVIGATE\n\n" +
            "Press START to begin.";
    }
}
