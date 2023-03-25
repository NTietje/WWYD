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
    
    public static HouseAnimationManager Instance { get; private set; }
   
    private bool allowEventAfterDialogue = false;
    private bool runWasStart = false;

    void Update()
    {
        if (choicesManager.GetAllowRun() & !runWasStart)
        {
            runWasStart = true;
            Debug.Log("is allowed to run");
            RunForestRunOutOfHouse();
        }
        if (allowEventAfterDialogue & !DialogueManager.Instance.getIsDialogueActive() &
            (DialogueManager.Instance.getDialogueType() == DialogueType.ConfrontBadGuyRun))
        {
            Debug.Log("will start end scene load after run");
            allowEventAfterDialogue = false;
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

    private void RunForestRunOutOfHouse()
    {
        // if (choicesManager.GetCurrentChoiceType() == ChoiceType.ConfrontBadGuy)
        // {
        Debug.Log("run forest run out of house");
        StartCoroutine(StartAnimationProcess2());
        // }
    }

    public void StartEndSceneAfterConfrontation()
    {
        Debug.Log("load end level in HouseAnimationManager");
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
        Debug.Log("start StartAnimationProcess2");
        if (npcMovable != null & destinationOutOfHouse != null)
        {
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
