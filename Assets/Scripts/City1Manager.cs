using System.Collections;
using UnityEngine;

public class City1Manager : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("is in Awake of City1Manager");
        GameStoryManager.Instance.allowReset = true;
        GameStoryManager.Instance.SubtractFromClockTime(1, 30, 0);
    }

    void Start()
    {
        Debug.Log("is in Start of City1Manager");
        if (!GameStoryManager.Instance.visitedCity1)
        {
            StartCoroutine(DisplayDialogue(DialogueType.CityIntro));
        } else if (GameStoryManager.Instance.wasDead)
        {
            StartCoroutine(DisplayDialogue(DialogueType.AgainStartAfterDied));
        }
    }
    
    private IEnumerator DisplayDialogue(DialogueType type)
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.setDialogueType(type);
        DialogueManager.Instance.StartNewDialogue();
    }

}
