using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public enum InteractionType
{
    StartDialogueOnTrigger,
    StartDialogue
}

public enum PersonTag
{
    Player,
    BadGuy
}

static class PersonTagMethods
{
    public static String GetTagString(this PersonTag tag)
    {
        switch (tag)
        {
            case PersonTag.Player:
                return "Player";
            case PersonTag.BadGuy:
                return "BadGuy";
            default:
                return "";
        }
    }
}

public class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    [SerializeField] private InteractionType interactionType;
    [SerializeField] private DialogueType dialogueType;
    [SerializeField] private PersonTag interactPerson = PersonTag.Player;
    [Header("Start Events after Interaction")]
    [SerializeField] private UnityEvent interactAction = null;
    [SerializeField] private bool startEventAfterDialogue = false;
    [SerializeField] private float eventDelay;
    [Header("Observables")]
    [SerializeField] private bool countTalk = false;

    private bool allowCounting = false;
    private bool _inRange;
    private bool allowEventAfterDialogue;
    private KeyCode _interactKey = KeyCode.E;
    private bool triggerDialogueWasFired = false;

    public void clearActions()
    {
        interactAction = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inRange)
        {
            if (Input.GetKeyDown(_interactKey))
            {
                Debug.Log("Interaction was called");
                if (interactionType == InteractionType.StartDialogue)
                {
                    StartDialogue(dialogueType);
                    allowCounting = true;
                }
                if (!startEventAfterDialogue & interactAction != null)
                {
                    StartCoroutine(ShowEvent());
                }
            }
        }

        if (allowCounting & !DialogueManager.Instance.getIsDialogueActive() & countTalk)
        {
            int objectID = gameObject.GetInstanceID();
            allowCounting = false;
            GameStoryManager.Instance.CountUpPeopleTalkedTo(objectID.ToString());
        }
        
        if (interactAction != null & startEventAfterDialogue &
            !DialogueManager.Instance.getIsDialogueActive() & allowEventAfterDialogue)
        {
            Debug.Log("dialog ended");
            allowEventAfterDialogue = false;
            StartCoroutine(ShowEvent());
        }
    }

    public void StartDialogue(DialogueType type)
    {
        Debug.Log("will start dialog: " + type.ToString());
        DialogueManager.Instance.setDialogueType(type);
        DialogueManager.Instance.StartNewDialogue();
        if (startEventAfterDialogue)
        {
            allowEventAfterDialogue = true; 
        }
    }
    
    public void StartDialogue()
    {
        DialogueManager.Instance.setDialogueType(dialogueType);
        DialogueManager.Instance.StartNewDialogue();
        if (startEventAfterDialogue)
        {
            allowEventAfterDialogue = true; 
        }
    }

    private IEnumerator ShowEvent()
    {
        yield return new WaitForSeconds(eventDelay);
        interactAction.Invoke(); 
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag(interactPerson.GetTagString()))
        {
            Debug.Log(interactPerson.GetTagString() + "is in range");
            if (interactionType == InteractionType.StartDialogueOnTrigger & !triggerDialogueWasFired)
            {
                StartDialogue(dialogueType);
                triggerDialogueWasFired = true;
            }
            else
            {
                _inRange = true;
                DialogueManager.Instance.ShowInteractPanel(); 
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(interactPerson.GetTagString()))
        {
            Debug.Log(interactPerson.GetTagString() + "is out of range");
            _inRange = false;
            DialogueManager.Instance.HideInteractPanel();
        }
    }
}
