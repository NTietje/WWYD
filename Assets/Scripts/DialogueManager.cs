using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text dialogueSubtext;
    [SerializeField] private Text dialogueSpeaker;
    [Header("Choices UI")]
    [SerializeField] private GameObject choicesPanel;
    [Header("Dialogue Settings")]
    [SerializeField] float typingDelay;
    [Header("Interact Press UI")]
    [SerializeField] private GameObject interactPanel;
    
    public static DialogueManager Instance { get; private set; }
    
    
    private readonly KeyCode _continueKey = KeyCode.Return;
    private DialogueType _dialogueType;
    // for ink use
    // public TextAsset inkJson;
    // private Story gameStory;
    private List<Dialogue> _dialogues;
    private bool _dialogueIsTyping;
    private bool _dialogueFinished;
    private bool _dialogueIsActive;
    private int _dialogueIndex;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        // gameStory = new Story(inkJson.text);
        _dialogueIsTyping = false;
        _dialogueIsActive = false;
        _dialogueFinished = false;
        dialoguePanel.SetActive(false);
        interactPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(_continueKey) & _dialogueIsActive & !_dialogueIsTyping)
        {
            if (!_dialogueFinished)
            {
                Debug.Log("CONTINUE dialogue");
                ContinueDialogue();
            }
        }
    }

    public void ShowInteractPanel()
    {
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
        _dialogueIndex = 0;
        _dialogues = DialogueParser.GetDialoguesForType(_dialogueType);
        _dialogueIsActive = true;
        _dialogueFinished = false;
        interactPanel.SetActive(false);
        dialoguePanel.SetActive(true);
        
        ContinueDialogue();
        
        // if (gameStory.canContinue)
        // {
        //     dialogueText.text = gameStory.Continue();
        // }
    }

    public void ContinueDialogue()
    {
        // dialogueText.text = "";
        dialogueSubtext.text = "";
        
        if (_dialogues.Count > 0 & _dialogueIndex < _dialogues.Count & !_dialogueIsTyping)
        {
            dialogueSpeaker.text = _dialogues[_dialogueIndex].Speaker;
            // dialogueText.text = dialogues[dialogueIndex].text;
            StartCoroutine(DisplayLine(_dialogues[_dialogueIndex].Text));
            dialogueText.gameObject.SetActive(true);
            
            if (_dialogues[_dialogueIndex].Subtext != null)
            {
                dialogueSubtext.gameObject.SetActive(true);
                dialogueSubtext.text = _dialogues[_dialogueIndex].Subtext;
            }
            else
            {
                dialogueSubtext.gameObject.SetActive(false);
            }
            
            if (_dialogues[_dialogueIndex].Choices != null)
            {
                choicesPanel.SetActive(true);
                // set choices
            }
            else
            {
                choicesPanel.SetActive(false);
            }
            
            _dialogueIndex++;
            // if (dialogueIndex == dialogues.Count)
            // {
            //     
            // }
        }
        else
        {
            CloseDialogue();
        }

    }

    private void CloseDialogue()
    {
        Debug.Log("CLOSE dialogue");
        _dialogueFinished = true;
        _dialogues = null;
        dialogueText.gameObject.SetActive(false);
        dialogueSubtext.gameObject.SetActive(false);
        _dialogueIsActive = false;
        dialoguePanel.SetActive(false);
    }

    private IEnumerator DisplayLine(string line)
    {
        _dialogueIsTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingDelay);
        }
        _dialogueIsTyping = false;
    }
}
