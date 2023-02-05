using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStartDialogue : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private DialogueType dialogueType;

    private void Start()
    {
        Debug.Log("Display dialogue on start");
        StartCoroutine(DisplayDialogue());
    }

    public void StartFlow()
    {
        StartCoroutine(DisplayDialogue());
    }
    
    private IEnumerator DisplayDialogue()
    {
        Debug.Log("will wait till dialogue");
        yield return new WaitForSeconds(delay);
        Debug.Log("will start dialogue");
        DialogueManager.Instance.setDialogueType(dialogueType);
        DialogueManager.Instance.StartNewDialogue();
    }
}
