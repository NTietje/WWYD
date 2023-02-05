using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableNPCCount : MonoBehaviour
{
    [Header("Interactable Settings")]
    
    [SerializeField] private InteractionType interactionType;
    [SerializeField] private DialogueType dialogueType;
    [Header("Start Events after Interaction")]
    [SerializeField] private UnityEvent interactAction = null;
    [SerializeField] private bool startEventAfterDialogue = false;
    [SerializeField] private float eventDelay;

    private bool _inRange;
    private bool allowEventAfterDialogue;
    private KeyCode _interactKey = KeyCode.E;

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
                    Debug.Log("will start dialog");
                    DialogueManager.Instance.setDialogueType(dialogueType);
                    DialogueManager.Instance.StartNewDialogue();
                    if (startEventAfterDialogue)
                    {
                        allowEventAfterDialogue = true; 
                    }
                }
                if (!startEventAfterDialogue & interactAction != null)
                {
                    StartCoroutine(ShowEvent());
                }
            }
        }
        if (interactAction != null & startEventAfterDialogue &
            !DialogueManager.Instance.getIsDialogueActive() & allowEventAfterDialogue)
        {
            allowEventAfterDialogue = false;
            StartCoroutine(ShowEvent());
        }
    }

    private IEnumerator ShowEvent()
    {
        yield return new WaitForSeconds(eventDelay);
        interactAction.Invoke(); 
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is in range");
            _inRange = true;
            DialogueManager.Instance.ShowInteractPanel();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is out of range");
            _inRange = false;
            DialogueManager.Instance.HideInteractPanel();
        }
    }
}