using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float sensitivity = 10f; // Mouse sensitivity
    public Vector3 offset = new Vector3(0, 2, -5); // Offset from the player position

    private float pitch = 0f; // Vertical rotation angle
    private float yaw = 0f; // Horizontal rotation angle

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity; // Get horizontal mouse movement
        pitch -= Input.GetAxis("Mouse Y") * sensitivity; // Get vertical mouse movement
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Clamp vertical angle

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = player.position + rotation * offset; // Calculate the new position based on rotation and offset

        transform.LookAt(player); // Make sure the camera always looks at the player
    }
}
