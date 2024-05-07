using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Camera mainCamera;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get the camera's forward and right vectors, ignoring the vertical component
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed;
        transform.Translate(movement * Time.deltaTime, Space.World);
    }
}
