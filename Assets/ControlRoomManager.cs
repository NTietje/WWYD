using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoomManager : MonoBehaviour
{
    void Awake()
    {
        GameStoryManager.Instance.allowReset = true;
        GameStoryManager.Instance.SubtractFromClockTime(1, 30, 0);
    }

    void Start()
    {
        if (!GameStoryManager.Instance.visitedControlRoom)
        {
            StartCoroutine(DisplayDialogue(DialogueType.ControlRoomIntro));
        } else
        {
            StartCoroutine(DisplayDialogue(DialogueType.AgainControlRoom));
        }
    }
    
    private IEnumerator DisplayDialogue(DialogueType type)
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.setDialogueType(type);
        DialogueManager.Instance.StartNewDialogue();
    }
}
