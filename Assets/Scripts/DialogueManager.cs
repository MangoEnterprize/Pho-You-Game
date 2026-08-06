using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private string[] currentLines;
    private int currentLineIndex;
    private bool isDialogueActive;

    private GameObject currentBubbleCanvas;
    private TextMeshProUGUI currentBubbleText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(GameObject bubbleCanvas, TextMeshProUGUI bubbleText, string[] lines)
    {
        currentBubbleCanvas = bubbleCanvas;
        currentBubbleText = bubbleText;
        currentLines = lines;
        currentLineIndex = 0;
        isDialogueActive = true;

        currentBubbleCanvas.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < currentLines.Length)
        {
            currentBubbleText.text = currentLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        if (currentBubbleCanvas != null)
        {
            currentBubbleCanvas.SetActive(false);
        }
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}