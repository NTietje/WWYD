using System.Collections;
using UnityEngine;

public class GameStoryManager : MonoBehaviour
{
    public static GameStoryManager Instance;
    
    private int peopleTalkedCount = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one GameStoryManager");
        }
        else
        {
            Instance = this;
        }
    }

    public void CountUpPeopleTalkedTo()
    {
        peopleTalkedCount += 1;
        Debug.Log("talked to count: " + peopleTalkedCount);
        if (peopleTalkedCount == 4)
        {
            Debug.Log("talked to 4 People");
            peopleTalkedCount = 0;
            StartDialogueWithDelay(2, DialogueType.CityChoices);
        }
    }
    
    public void StartDialogueWithDelay(int delay, DialogueType dialogueType)
    {
        StartCoroutine(DisplayDialogue(delay, dialogueType));
    }
    
    private IEnumerator DisplayDialogue(int delay, DialogueType dialogueType)
    {
        yield return new WaitForSeconds(delay);
        DialogueManager.Instance.setDialogueType(dialogueType);
        DialogueManager.Instance.StartNewDialogue();
    }

}
