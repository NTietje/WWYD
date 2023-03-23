using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class HouseAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animatorTür1;
    [SerializeField] private Animator animatorTür2;
    [SerializeField] private NpcMovable npcMovable = null;
    [SerializeField] private GameObject destinationInHouse = null;
    [SerializeField] private GameObject destinationOutOfHouse = null;
    [SerializeField] private Interactable firstDestinationInteractable;
    [SerializeField] private ChoicesManager choicesManager;
    [SerializeField] private GameObject badGuy;
   
    private bool allowEventAfterDialogue = false;
    // private void Start()
    // {
    //     if (npcMovable != null & destinationInHouse != null)
    //     {
    //         Debug.Log("start walk to");
    //         npcMovable.WalkTo(destinationInHouse);
    //     }
    // }
    
    void Update()
    {
        if (!DialogueManager.Instance.getIsDialogueActive() & allowEventAfterDialogue)
        {
            StartEndSceneAfterConfrontation();
        }
    }

    public void CheckEvidence()
    {
        if (GameStoryManager.Instance.CheckAllEvidenceFound())
        {
            StartCoroutine(StartAnimationProcess1());
        }
    }

    public void RunForestRunOutOfHouse()
    {
        Debug.Log("run forest run");
        if (choicesManager.GetCurrentChoiceType() == ChoiceType.ConfrontBadGuy)
        {
            StartCoroutine(StartAnimationProcess2());
        }
    }

    public void StartEndSceneAfterConfrontation()
    {
        Debug.Log("load end level");
        LevelManager.Instance.LoadScene("09_Ende_1");
    }

    private IEnumerator StartAnimationProcess1() // walk into living room
    {
        if (npcMovable != null & destinationInHouse != null)
        {
            npcMovable.WalkTo(destinationInHouse);
        }
        if (animatorTür1 != null)
        {
            yield return new WaitForSeconds(1);
            animatorTür1.SetBool("enable", true);
        }

    }

    private IEnumerator StartAnimationProcess2() // run out of the house
    {
        firstDestinationInteractable.clearActions();
        firstDestinationInteractable.StartDialogue(DialogueType.ConfrontBadGuyRun);

        yield return new WaitForSeconds(1);
        Debug.Log("start animation");
        if (npcMovable != null & destinationOutOfHouse != null)
        {
            Debug.Log("start run to");
            npcMovable.RunTo(destinationOutOfHouse);
        }
        
        if (animatorTür2 != null)
        {
            animatorTür2.SetBool("enable", true);
        }

        allowEventAfterDialogue = true;
        yield return new WaitForSeconds(4);
        badGuy.SetActive(false);
    }

}
