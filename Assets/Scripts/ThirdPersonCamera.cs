using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target & Positioning")]
    public Transform target; // Drag your Player object here in the Inspector
    public Vector3 offset = new Vector3(0, 2f, -5f); // Distance behind/above the player

    [Header("Sensitivity & Limits")]
    public float sensitivity = 3.0f;
    public float pitchMin = -20f; // Limit looking down
    public float pitchMax = 80f;  // Limit looking up

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        currentYaw = angles.y;
        currentPitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1 = Right Mouse Button
        if (Input.GetMouseButton(1))
        {
            // Lock and hide the cursor while dragging
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get mouse input movement
            currentYaw += Input.GetAxis("Mouse X") * sensitivity;
            currentPitch -= Input.GetAxis("Mouse Y") * sensitivity;
            currentPitch = Mathf.Clamp(currentPitch, pitchMin, pitchMax);
        }
        else
        {
            // Unlock cursor when right click is released
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Calculate rotation and position based on target
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        transform.position = target.position + rotation * offset;
        
        // Point camera towards the player
        transform.LookAt(target.position + Vector3.up * 1.2f);
    }
}
