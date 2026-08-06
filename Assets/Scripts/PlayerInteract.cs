using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Camera mainCam;
    public float interactionDistance = 10f;
    public LayerMask ignoreLayers; // Adds the layer dropdown to the Inspector

    [Header("UI Settings")]
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    private void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        if (mainCam == null) return;

        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

        bool hitSomething = false;

        // ~ignoreLayers ignores whatever layer is selected in the Inspector dropdown
        if (Physics.Raycast(ray, out hit, interactionDistance, ~ignoreLayers))
        {
            NPCInteractable interactable = hit.collider.GetComponentInParent<NPCInteractable>();

            if (interactable != null)
            {
                Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green);
                hitSomething = true;

                if (interactionText != null)
                {
                    interactionText.text = interactable.GetDescription();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }

        if (interactionUI != null)
        {
            interactionUI.SetActive(hitSomething);
        }
    }
}