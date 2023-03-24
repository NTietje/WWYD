using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAnimationManager : MonoBehaviour
{
    [SerializeField] private NpcMovable npcMovable = null;
    [SerializeField] private GameObject destination = null;
    [SerializeField] private GameObject badGuy;
    
    private bool allowEventAfterDialogue = false;
    
    void Update()
    {
        if (!DialogueManager.Instance.getIsDialogueActive() & allowEventAfterDialogue)
        {
            allowEventAfterDialogue = false;
            Debug.Log("will load house scene");
            StartHouseScene();
        }
    }
    
    public void RunForestRun()
    {
        // Debug.Log("run forest run");
        // if (choicesManager.GetCurrentChoiceType() == ChoiceType.ConfrontBadGuy)
        // {
            StartCoroutine(StartAnimationProcess());
        // }
    }
    
    private IEnumerator StartAnimationProcess() // run out of the house
    {
        if (npcMovable != null & destination != null)
        {
            Debug.Log("start run to");
            npcMovable.RunTo(destination);
        }
        
        yield return new WaitForSeconds(2);
        
        DialogueManager.Instance.setDialogueType(DialogueType.City2BadGuyAfterRun);
        DialogueManager.Instance.StartNewDialogue();
        
        yield return new WaitForSeconds(1);

        allowEventAfterDialogue = true;
        yield return new WaitForSeconds(2);
        badGuy.SetActive(false);
    }

    private void StartHouseScene()
    {
        LevelManager.Instance.LoadScene("08_Haus");
    }
}
