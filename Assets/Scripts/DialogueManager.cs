using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueUI;
    public string[] dialogues;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(string[] dialogueLines)
    {
        dialogues = dialogueLines;
        currentDialogueIndex = 0;
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        dialogueUI.SetActive(true);
        dialogueText.text = dialogues[currentDialogueIndex];
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisplayNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogueUI.activeSelf)
        {
            DisplayNextDialogue();
        }
    }
}