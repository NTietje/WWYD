
using System.Collections;
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
        yield return new WaitForSeconds(delay);
        DialogueManager.Instance.setDialogueType(dialogueType);
        DialogueManager.Instance.StartNewDialogue();
    }
}
