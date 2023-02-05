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
    [Header("Choices UI")]
    // [SerializeField] private GameObject choicesPanel;
    [Header("Dialogue Settings")]
  //  [SerializeField] float typingDelay;
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
        // gameStory = new Story(inkJson.text);
        canvas.gameObject.SetActive(true);
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
        Debug.Log("Show interact panel");
        canvas.gameObject.SetActive(true);
        interactPanel.SetActive(true);
    }
    
    public void HideInteractPanel()
    {
        Debug.Log("Hide interact panel");
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
        Debug.Log("Set dialogue type to: " + type);
    }
    
    public void StartNewDialogue()
    {
        Debug.Log("Start new dialogue");
        canvas.gameObject.SetActive(true);
        _dialogueIndex = 0;
        _dialogues = DialogueParser.GetDialoguesForType(_dialogueType);
        _dialogueIsActive = true;
        _dialogueFinished = false;
        dialoguePanel.SetActive(true);
        
        interactPanel.SetActive(false);
        
        ContinueDialogue();
        
        // if (gameStory.canContinue)
        // {
        //     dialogueText.text = gameStory.Continue();
        // }
    }

    public void ContinueDialogue()
    {
        Debug.Log("Try continue dialogue");
        dialogueText.text = "";
        dialogueSubtext.text = "";

        Debug.Log("dialogues count: " + _dialogues.Count);
        Debug.Log("dialogues index: " + _dialogueIndex);
        Debug.Log("dialogue is NOT typing: " + !_dialogueIsTyping);
        if (_dialogues.Count > 0 & _dialogueIndex < _dialogues.Count & !_dialogueIsTyping)
        {
            Debug.Log("continue dialogue");
            dialogueSpeaker.text = _dialogues[_dialogueIndex].Speaker;
            // dialogueText.text = dialogues[dialogueIndex].text;
            StartCoroutine(DisplayLine(_dialogues[_dialogueIndex].Text));
            dialogueText.gameObject.SetActive(true);
            
            if (_dialogues[_dialogueIndex].Subtext != null)
            {
                dialogueSubtext.text = _dialogues[_dialogueIndex].Subtext;
            }
            if (_dialogues[_dialogueIndex].Choices != null)
            {
                Debug.Log("choices found");
                Debug.Log("choices count: "+ _dialogues[_dialogueIndex].Choices.Count);
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
            yield return new WaitForSeconds(0.02f);
        }

        _dialogueIsTyping = false;
    }
}
