using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AStarPathfinder : MonoBehaviour
{
    public Transform robotStart;
    public Transform destination;
    public Transform[] obstacles;

    public LineRenderer pathLine;
    public TMP_Text statusText;

    private List<Vector3> finalPath = new List<Vector3>();

    private int currentStep = 0;

    private const int GRID_MIN = -4;
    private const int GRID_MAX = 4;

    private class Node
    {
        public Vector2Int position;
        public int gCost;
        public int hCost;
        public Node parent;

        public int fCost => gCost + hCost;

        public Node(Vector2Int pos)
        {
            position = pos;
        }
    }

    void Start()
    {
        pathLine.positionCount = 0;

        statusText.text =
            "Press FIND PATH to calculate the shortest path.";
    }

    int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) +
               Mathf.Abs(a.y - b.y);
    }

    bool IsBlocked(Vector2Int position)
    {
        foreach (Transform obstacle in obstacles)
        {
            Vector2Int obstaclePosition =
                new Vector2Int(
                    Mathf.RoundToInt(obstacle.position.x),
                    Mathf.RoundToInt(obstacle.position.z)
                );

            if (obstaclePosition == position)
                return true;
        }

        return false;
    }

    public void GeneratePath()
    {
        finalPath.Clear();

        pathLine.positionCount = 0;

        currentStep = 0;

        Vector2Int start = new Vector2Int(
            Mathf.RoundToInt(robotStart.position.x),
            Mathf.RoundToInt(robotStart.position.z)
        );

        Vector2Int goal = new Vector2Int(
            Mathf.RoundToInt(destination.position.x),
            Mathf.RoundToInt(destination.position.z)
        );

        List<Node> openList = new List<Node>();
        HashSet<Vector2Int> closedList =
            new HashSet<Vector2Int>();

        Node startNode = new Node(start);

        startNode.gCost = 0;
        startNode.hCost = Heuristic(start, goal);

        openList.Add(startNode);

        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        while (openList.Count > 0)
        {
            Node current = openList[0];

            foreach (Node node in openList)
            {
                if (node.fCost < current.fCost)
                    current = node;
            }

            if (current.position == goal)
            {
                ReconstructPath(current);

                statusText.text =
                    "PATH FOUND! Total Steps: " +
                    (finalPath.Count - 1);

                DrawPath();

                return;
            }

            openList.Remove(current);
            closedList.Add(current.position);

            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbour =
                    current.position + direction;

                if (neighbour.x < GRID_MIN ||
                    neighbour.x > GRID_MAX ||
                    neighbour.y < GRID_MIN ||
                    neighbour.y > GRID_MAX)
                    continue;

                if (IsBlocked(neighbour) ||
                    closedList.Contains(neighbour))
                    continue;

                int newCost = current.gCost + 1;

                Node existing = openList.Find(
                    n => n.position == neighbour
                );

                if (existing == null)
                {
                    Node newNode = new Node(neighbour);

                    newNode.gCost = newCost;
                    newNode.hCost = Heuristic(neighbour, goal);
                    newNode.parent = current;

                    openList.Add(newNode);
                }
                else if (newCost < existing.gCost)
                {
                    existing.gCost = newCost;
                    existing.parent = current;
                }
            }
        }

        statusText.text =
            "NO PATH FOUND! Try changing the obstacles.";
    }

    void ReconstructPath(Node node)
    {
        while (node != null)
        {
            finalPath.Add(
                new Vector3(
                    node.position.x,
                    0.18f,
                    node.position.y
                )
            );

            node = node.parent;
        }

        finalPath.Reverse();
    }

    void DrawPath()
    {
        pathLine.positionCount = finalPath.Count;

        for (int i = 0; i < finalPath.Count; i++)
        {
            pathLine.SetPosition(i, finalPath[i]);
        }
    }

    public void NextStep()
    {
        if (finalPath.Count == 0)
            return;

        if (currentStep < finalPath.Count - 1)
        {
            currentStep++;

            robotStart.position = finalPath[currentStep];

            statusText.text =
                "Robot Navigation Step: " +
                currentStep + " / " +
                (finalPath.Count - 1);
        }
        else
        {
            statusText.text =
                "DESTINATION REACHED!";
        }
    }

    public void PreviousStep()
    {
        if (finalPath.Count == 0)
            return;

        if (currentStep > 0)
        {
            currentStep--;

            robotStart.position = finalPath[currentStep];

            statusText.text =
                "Robot Navigation Step: " +
                currentStep;
        }
    }

    public void ResetPath()
    {
        currentStep = 0;

        robotStart.position =
            new Vector3(-3, 0.25f, -3);

        pathLine.positionCount = 0;

        finalPath.Clear();

        statusText.text =
            "Press FIND PATH to calculate the shortest path.";
    }
}
