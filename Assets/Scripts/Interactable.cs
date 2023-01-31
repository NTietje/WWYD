
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    Interact,
    PickUp,
    Talk
}

public class Interactable : MonoBehaviour
{
    public InteractionType interactionType;
    public bool inRange;
    public UnityEvent interactAction;
    public KeyCode interactKey = KeyCode.E;
    
    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                Debug.Log("Interaction was called");
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is in range");
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is not anymore in range");
            inRange = false;
        }
    }
}
