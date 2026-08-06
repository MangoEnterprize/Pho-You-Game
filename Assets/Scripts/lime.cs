using UnityEngine;

public class lime : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float bounceSpeed = 2f;    // How fast it goes up and down
    [SerializeField] private float bounceHeight = 0.3f; // How far up and down it travels

    // Keeps track of where the lime started before it began floating
    private Vector3 startPosition;

    void Start()
    {
        // Save the starting position of the lime
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Handle the Spinning around the WORLD's Z-axis (forward)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // 2. Handle the Bouncing (Smooth Sine Wave)
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that walked into us has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        Destroy(gameObject);
    }
}