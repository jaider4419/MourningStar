using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    private bool dialogueShown = false; 
    private DialogueManager dialogueManager;

    [SerializeField] private string[] dialogues; 


    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); 
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not in scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Opening dialogue!");
        if (!dialogueShown)
        {
            ShowDialogue();
            dialogueShown = true;
        }
    }

    private void ShowDialogue()
    {
        if (dialogueManager != null)
        {
            if (dialogues != null && dialogues.Length > 0)
            {
                dialogueManager.StartDialogue(dialogues); 
            }
            else
            {
                Debug.LogError("No dialogue lines set!");
            }
        }
        else
        {
            Debug.LogError("DialogueManager not found!");
        }
    }
}