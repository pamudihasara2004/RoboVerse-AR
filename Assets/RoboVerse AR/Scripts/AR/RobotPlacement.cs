using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RobotPlacement : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject robotPrefab;

    private GameObject placedRobot;

    private List<ARRaycastHit> hits =
        new List<ARRaycastHit>();

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                touch.position.ReadValue();

            if (raycastManager.Raycast(
                touchPosition,
                hits,
                TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (placedRobot == null)
                {
                    placedRobot = Instantiate(
                        robotPrefab,
                        hitPose.position,
                        hitPose.rotation
                    );
                }
                else
                {
                    placedRobot.transform.SetPositionAndRotation(
                        hitPose.position,
                        hitPose.rotation
                    );
                }
            }
        }
    }
}