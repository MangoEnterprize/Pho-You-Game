using UnityEngine;
using TMPro;

public class NPCInteractable : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject speechBubbleCanvas;
    [SerializeField] private TextMeshProUGUI speechBubbleText;

    [Header("NPC Data")]
    [SerializeField] private string promptText = "Press E to talk";

    [Header("Dialogue Lines")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;

    public string GetDescription()
    {
        return promptText;
    }

    public void Interact()
    {
        if (DialogueManager.Instance.IsDialogueActive())
        {
            DialogueManager.Instance.DisplayNextLine();
        }
        else
        {
            DialogueManager.Instance.StartDialogue(speechBubbleCanvas, speechBubbleText, dialogueLines);
        }
    }
}