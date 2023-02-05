
using System;
using System.Collections;
using UnityEngine;

public class AutoStartDialogue : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private DialogueType dialogueType;
    [SerializeField] private SceneCall sceneCall = null;

    private bool dialogueCalled = false;

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
        yield return new WaitForSeconds(delay);
        DialogueManager.Instance.setDialogueType(dialogueType);
        DialogueManager.Instance.StartNewDialogue();
        dialogueCalled = true;
    }

    private void Update()
    {
        if (dialogueCalled & !DialogueManager.Instance.getIsDialogueActive() & sceneCall != null)
        {
            dialogueCalled = false;
            Debug.Log("AutoDialogue will start animation and new scene");
            sceneCall.StartAnimatorAndScene();
        }
    }
}
