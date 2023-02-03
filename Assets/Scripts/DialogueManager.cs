using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueType
{
    WastelandCan,
    WastelandDoll,
    WastelandPicture,
    WastelandSphere,
    CityIntro,
    CityChoices,
    CityPeopleDialogue,
    RiverIntro,
    ControlRoomIntro,
    ControlRoomChoices,
    ControlRoom,
    ReactorIntro,
    Powerhouse,
    HouseChoicesOne,
    HouseChoicesTwo
}

public enum Choice
{
    People,
    Chill,
    Investigate
}

public class Dialogue
{
    public string speaker { get; set; }
    public string text { get; set; }
    public string subtext { get; set; }
    public List<Choice> choices { get; set; }

    public Dialogue(string speaker, string text, string subtext = null, List<Choice> choices = null)
    {
        this.speaker = speaker;
        this.text = text;
        this.subtext = subtext;
        this.choices = choices;
    }
}

public static class DialogueParser {

    public static List<Dialogue> GetDialoguesForType(DialogueType type)
    {
        List<Dialogue> dialogues = new List<Dialogue>();
        switch (type)
        {
            case DialogueType.WastelandCan:
                dialogues.Add(new Dialogue("Ich", "bla bla bla", "*singt*"));
                dialogues.Add(new Dialogue("Ich", "nasfjbf sajfbghas jdfh asuihgas"));
                return dialogues;
            case DialogueType.WastelandDoll:
                break;
            case DialogueType.WastelandPicture:
                break;
            case DialogueType.WastelandSphere:
                break;
            case DialogueType.CityIntro:
                break;
            case DialogueType.CityChoices:
                break;
            case DialogueType.CityPeopleDialogue:
                break;
            case DialogueType.RiverIntro:
                break;
            case DialogueType.ControlRoomIntro:
                break;
            case DialogueType.ControlRoomChoices:
                break;
            case DialogueType.ControlRoom:
                break;
            case DialogueType.ReactorIntro:
                break;
            case DialogueType.Powerhouse:
                break;
            case DialogueType.HouseChoicesOne:
                break;
            case DialogueType.HouseChoicesTwo:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return dialogues;
    }
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text dialogueSubtext;
    [SerializeField] private Text dialogueSpeaker;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] float typingDelay = 0f;

    public DialogueType dialogueType;
    // public TextAsset inkJson;
    private List<Dialogue> dialogues;
    // private Story gameStory;
    private bool dialogueIsTyping;
    public static DialogueManager instance { get; private set; }
    private bool dialogueIsPlaying;
    private int dialogueIndex = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // gameStory = new Story(inkJson.text);
        dialogueIsTyping = false;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
    
    public void StartNewDialogue()
    {
        dialogueIndex = 0;
        dialogues = DialogueParser.GetDialoguesForType(dialogueType);
        dialogueIsPlaying = true;
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
        
        if (dialogues.Count > 0 & dialogueIndex < dialogues.Count & !dialogueIsTyping)
        {
            dialogueSpeaker.text = dialogues[dialogueIndex].speaker;
            // dialogueText.text = dialogues[dialogueIndex].text;
            StartCoroutine(DisplayLine(dialogues[dialogueIndex].text));
            dialogueText.gameObject.SetActive(true);
            
            if (dialogues[dialogueIndex].subtext != null)
            {
                dialogueSubtext.gameObject.SetActive(true);
                dialogueSubtext.text = dialogues[dialogueIndex].subtext;
            }
            else
            {
                dialogueSubtext.gameObject.SetActive(false);
            }
            
            if (dialogues[dialogueIndex].choices != null)
            {
                choicesPanel.SetActive(true);
                // set choices
            }
            else
            {
                choicesPanel.SetActive(false);
            }
            
            dialogueIndex++;
            // if (dialogueIndex == dialogues.Count)
            // {
            //     
            // }
        }

    }

    private void EndDialogueReached()
    {
        
    }

    private void CloseDialogue()
    {
        dialogues = null;
        dialogueText.gameObject.SetActive(false);
        dialogueSubtext.gameObject.SetActive(false);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueIsTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingDelay);
        }
        dialogueIsTyping = false;
    }
}
