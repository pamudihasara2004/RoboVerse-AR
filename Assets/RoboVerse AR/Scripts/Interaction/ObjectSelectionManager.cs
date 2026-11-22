using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ObjectSelectionManager : MonoBehaviour
{
    public Camera arCamera;
    public TMP_Text infoText;

    private Transform selectedObject;
    private Vector3 originalScale;

    void Start()
    {
        Debug.Log("ObjectSelectionManager is working!");

        if (arCamera == null)
            Debug.LogError("PreviewCamera is NOT assigned!");

        if (infoText == null)
            Debug.LogError("MappingInfoText is NOT assigned!");
    }

    void Update()
    {
        Vector2 position;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            position = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            position = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        if (arCamera == null || infoText == null)
            return;

        Ray ray = arCamera.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ObjectInfo info = hit.collider.GetComponent<ObjectInfo>();

            if (info != null)
            {
                if (selectedObject != null)
                    selectedObject.localScale = originalScale;

                selectedObject = info.transform;
                originalScale = selectedObject.localScale;

                selectedObject.localScale = originalScale * 1.15f;

                infoText.text = info.objectName +
                                "\n\n" + info.description;

                Debug.Log("Selected: " + info.objectName);
            }
        }
    }

    public void ClearSelection()
    {
        Debug.Log("RESET BUTTON CLICKED!");

        if (selectedObject != null)
        {
            selectedObject.localScale = originalScale;
            selectedObject = null;
        }

        if (infoText != null)
        {
            infoText.text =
                "ROBOT MAPPING LAB\n\nSelect a marker to explore.";
        }
        else
        {
            Debug.LogError("MappingInfoText is not assigned!");
        }
    }
}