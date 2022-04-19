using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cam;
    private float pitch = 0f;

    [Range(0.0f,5f)]
    public float sensitivity;

    public bool canLook = true;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canLook)
        {
            return;
        }

        Vector2 mouseMove = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        UpdateMouseY(mouseMove);
        UpdateMouseX(mouseMove);
    }

    // Horizontal mouse movement
    void UpdateMouseX(Vector2 mousemove)
    {
        transform.Rotate(Vector3.up * mousemove.x * sensitivity);
    }

    // Vertical mouse movement
    void UpdateMouseY(Vector2 mouseMove)
    {
        pitch -= mouseMove.y;

        if (pitch > 90) // limit angle looking down and up
            pitch = 90;
        if (pitch < -90)
            pitch = -90;

        cam.localEulerAngles = Vector3.right * pitch * sensitivity;
    }
}
