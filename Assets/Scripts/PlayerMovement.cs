using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5.0f;
    public float rotationSpeed = 720.0f; // Higher value = faster, sharper turns
    public Transform cameraTransform;

    [Header("Rotation Behavior")]
    [Tooltip("If true, player turns to face camera direction ONLY while holding Right Click. If false, player always faces camera direction.")]
    public bool rotateWithCameraOnlyOnRightClick = true;

    void Start()
    {
        // Find main camera if not assigned
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // 1. Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 5, 0);
        }

        // 2. Flatten camera vectors onto the XZ plane (ignore looking up/down)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 3. Movement relative to camera orientation
        Vector3 movement = (forward * moveZ + right * moveX);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // 4. Player Rotation Logic
        bool isRightClicking = Input.GetMouseButton(1);

        if (isRightClicking || !rotateWithCameraOnlyOnRightClick)
        {
            // Rotate player to match the CAMERA's horizontal forward direction
            if (forward != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (movement != Vector3.zero)
        {
            // Fallback: Rotate player toward direction of MOVEMENT when not right-clicking
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}