using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueControllerUI : MonoBehaviour
{
    private List<DialogueLine> dialogues;
    private int currentIndex = 0;

    [Header("Speaker Positions")]
    public Transform playerPoint;
    public Transform shadowPoint;

    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public Vector3 offset = new Vector3(0, 2f, 0);

    private Transform currentTarget;

    /// <summary>
    /// Called by DialogueTrigger to start a dialogue sequence.
    /// </summary>
    public void StartDialogue(List<DialogueLine> lines)
    {
        if (lines == null || lines.Count == 0) return;

        dialogues = lines;
        currentIndex = 0;
        ShowDialogue();
    }

    private void Update()
    {
        // no active dialogues → nothing to do
        if (dialogues == null || dialogues.Count == 0) return;

        // advance dialogue on left-click
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }

        // make text bubble follow the current speaker
        if (dialogueText != null && currentTarget != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentTarget.position + offset);
            dialogueText.transform.position = screenPos;
        }
    }

    private void NextDialogue()
    {
        if (dialogues == null || dialogues.Count == 0) return;

        currentIndex++;
        if (currentIndex < dialogues.Count)
        {
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowDialogue()
    {
        if (dialogues == null || dialogues.Count == 0) return;

        DialogueLine line = dialogues[currentIndex];

        // decide whose bubble this line belongs to
        currentTarget = (line.speaker == Speaker.Player) ? playerPoint : shadowPoint;

        if (dialogueText != null)
        {
            dialogueText.text = line.text;
        }
    }

    private void EndDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }

        dialogues = null;
        currentIndex = 0;
        currentTarget = null;
    }
}