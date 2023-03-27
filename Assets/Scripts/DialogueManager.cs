using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Text dialogueSubtext;
    [SerializeField] private Text dialogueSpeaker;
    // [Header("Choices UI")]
    // [SerializeField] private GameObject choicesPanel;
    // [Header("Dialogue Settings")]
  //  [SerializeField] float typingDelay;
    [Header("Interact Press UI")]
    [SerializeField] private GameObject interactPanel;
    [Header("Player Object")]
    [SerializeField] private AnimationAndMovementController playerController = null;

    public static DialogueManager Instance { get; private set; }
    
    private readonly KeyCode _continueKey = KeyCode.Return;
    private DialogueType _dialogueType;
    private List<Dialogue> _dialogues;
    private bool _dialogueIsTyping;
    private bool _dialogueFinished;
    private bool _dialogueIsActive;
    private bool _allowDialogInteraction = false;
    private int _dialogueIndex;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager");
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        canvas.gameObject.SetActive(true);
        _dialogueIsTyping = false;
        _dialogueIsActive = false;
        _dialogueFinished = false;
        dialoguePanel.SetActive(false);
        interactPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(_continueKey) & _dialogueIsActive & !_dialogueIsTyping && _allowDialogInteraction)
        {
            if (!_dialogueFinished)
            {
                ContinueDialogue();
            }
        }
    }

    public void ShowInteractPanel()
    {
        canvas.gameObject.SetActive(true);
        interactPanel.SetActive(true);
    }
    
    public void HideInteractPanel()
    {
        interactPanel.SetActive(false);
    }
    
    public DialogueType getDialogueType()
    {
        return _dialogueType;
    }

    public bool getIsDialogueActive()
    {
        return _dialogueIsActive;
    }

    public void setDialogueType(DialogueType type)
    {
        _dialogueType = type;
    }
    
    public void StartNewDialogue()
    {
        if (playerController != null)
        {
            playerController.ActivatePlayerMovement(false);
        }
        canvas.gameObject.SetActive(true);
        _dialogueIsTyping = false;
        _dialogueIndex = 0;
        _dialogues = DialogueParser.GetDialoguesForType(_dialogueType);
        _dialogueIsActive = true;
        _dialogueFinished = false;
        dialoguePanel.SetActive(true);
        interactPanel.SetActive(false);

        ContinueDialogue();
        _allowDialogInteraction = true;
    }

    public void ContinueDialogue()
    {
        dialogueText.text = "";
        dialogueSubtext.text = "";

        if (_dialogues.Count > 0 & _dialogueIndex < _dialogues.Count & !_dialogueIsTyping)
        {
            dialogueSpeaker.text = _dialogues[_dialogueIndex].Speaker;
            StartCoroutine(DisplayLine(_dialogues[_dialogueIndex].Text));
            dialogueText.gameObject.SetActive(true);
            
            if (_dialogues[_dialogueIndex].Subtext != null)
            {
                dialogueSubtext.text = _dialogues[_dialogueIndex].Subtext;
            }
            if (_dialogues[_dialogueIndex].Choices != null)
            {
                ChoicesManager.Instance.StartChoices(_dialogues[_dialogueIndex].Choices);
            }
            else
            {
                Debug.Log("no choices");
            }
            _dialogueIndex++;
        }
        else
        {
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        _dialogueIsActive = false;
        _allowDialogInteraction = false;
        _dialogueFinished = true;
        _dialogues = null;
        dialogueText.gameObject.SetActive(false);
        dialogueSubtext.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        if (playerController != null)
        {
            playerController.ActivatePlayerMovement(true);
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        _dialogueIsTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        _dialogueIsTyping = false;
    }
}
