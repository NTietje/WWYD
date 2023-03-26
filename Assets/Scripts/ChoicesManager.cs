using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class ChoicesManager : MonoBehaviour
{
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private GameObject indicator1;
    [SerializeField] private GameObject indicator2;
    [SerializeField] private GameObject indicator3;
    [SerializeField] private AnimationAndMovementController playerController;
    
    private List<ChoiceType> _choices;
    private List<GameObject> _indicators;
    private List<TextMeshProUGUI> _texts;
    private KeyCode _upKey = KeyCode.UpArrow;
    private KeyCode _downKey = KeyCode.DownArrow;
    private KeyCode _enter = KeyCode.Return;
    private int _selectedChoiceIndex = 0;
    private bool _choicesActive;
    private bool _allowEventAfterDialogue = false;
    private ChoiceType currentChoiceType;
    private bool _allowRunToStart = false;

    public static ChoicesManager Instance;

    public ChoiceType GetCurrentChoiceType()
    {
        return currentChoiceType;
    }

    public bool getIsActive()
    {
        return _choicesActive;
    }

    public bool GetAllowRun()
    {
        return _allowRunToStart;
    }
    private void Awake()
    {
        _choicesActive = false;
        choicesPanel.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _texts = new List<TextMeshProUGUI>();
        _indicators = new List<GameObject>();
        Debug.Log("Start in ChoiceManager called");
        _choicesActive = false;
        choicesPanel.SetActive(false);
        _texts.Add(text1);
        _texts.Add(text2);
        _texts.Add(text3);
        _indicators.Add(indicator1);
        _indicators.Add(indicator2);
        _indicators.Add(indicator3);
    }
    
    public void StartChoices(List<ChoiceType> choices)
    {   
        Debug.Log("in StartChoices");
        foreach (var text in _texts)
        {
            text.text = "";
        }
        indicator1.SetActive(true);
        indicator2.SetActive(false);
        indicator3.SetActive(false);
        _choices = choices;
        int index = 0;
        _selectedChoiceIndex = 0;
        foreach (var choice in _choices)
        {
            Debug.Log("choice:" + choice);
            string text = ChoiceParser.GetTextForChoiceType(choice);
            Debug.Log("text: " + text);
            _texts[index].text = text;
            index++;
        }
        playerController.ActivatePlayerMovement(false);
        choicesPanel.SetActive(true);
        StartCoroutine(enableChoiceControls());
    }
    
    private IEnumerator enableChoiceControls()
    {
        yield return new WaitForSeconds(0.01f); // delay to not fire the choice immediately
        _choicesActive = true;
    }

    // void StartEndAfterConfront()
    // {
    //     Debug.Log("load end level");
    //     LevelManager.Instance.LoadScene("09_Ende_1");
    // }
    
    // private IEnumerator ShowEvent(ChoiceType choice)
    // {
    //     yield return new WaitForSeconds(_eventDelay);
    //     Debug.Log("is in enumerator showEvent");
    //     switch (_choices[_selectedChoiceIndex])
    //     {
    //         case ChoiceType.ConfrontBadGuy: 
    //             Debug.Log("case confront");
    //             StartEndAfterConfront(); 
    //             break;
    //     }
    // }

    void Update()
    {
        if (_allowEventAfterDialogue & !DialogueManager.Instance.getIsDialogueActive())
        {
            // Debug.Log("dialog off, will invoke event");
            _allowEventAfterDialogue = false;
            DialogueManager.Instance.setDialogueType(DialogueType.ConfrontBadGuyRun);
            DialogueManager.Instance.StartNewDialogue();
            Debug.Log("set allow run to true");
            _allowRunToStart = true;
        }
        
        if (_choicesActive)
        {
            if (Input.GetKeyDown(_downKey))
            {
                if (_selectedChoiceIndex < _choices.Count)
                {
                    _selectedChoiceIndex += 1;
                    Debug.Log("selected choice index: " + _selectedChoiceIndex);
                    Debug.Log("choice would be: " + _choices[_selectedChoiceIndex].ToString());
                    int indicatorIndex = 0;
                    foreach (var indicaor in _indicators)
                    {
                        if (indicatorIndex == _selectedChoiceIndex)
                        {
                            indicaor.SetActive(true);
                        }
                        else
                        {
                            indicaor.SetActive(false);
                        }

                        indicatorIndex++;
                    }
                }
            } else if (Input.GetKeyDown(_upKey))
            {
                if (_selectedChoiceIndex > 0)
                {
                    _selectedChoiceIndex -= 1;
                    Debug.Log("selected choice index: " + _selectedChoiceIndex);
                    Debug.Log("choice would be: " + _choices[_selectedChoiceIndex].ToString());
                    int indicatorIndex = 0;
                    foreach (var indicaor in _indicators)
                    {
                        if (indicatorIndex == _selectedChoiceIndex)
                        {
                            indicaor.SetActive(true);
                        }
                        else
                        {
                            indicaor.SetActive(false);
                        }

                        indicatorIndex++;
                    }
                }
            } else if (Input.GetKeyDown(_enter))
            {
                Debug.Log("it is key down");
                currentChoiceType = _choices[_selectedChoiceIndex];
                Debug.Log("choice is: " + _choices[_selectedChoiceIndex].ToString());
                string nextScene = ChoiceParser.GetSceneForChoiceType(_choices[_selectedChoiceIndex]);
                if (nextScene == "NONE")
                {
                    switch (_choices[_selectedChoiceIndex])
                    {
                        case ChoiceType.ConfrontBadGuy:
                            DialogueManager.Instance.setDialogueType(DialogueType.ConfrontBadGuy);
                            DialogueManager.Instance.StartNewDialogue();
                            _allowEventAfterDialogue = true;
                            break;
                    }
                    
                } else
                {
                    Debug.Log("will load scene in ChoiceManager");
                    if (nextScene == ChoiceParser.GetSceneForChoiceType(ChoiceType.BackToCity))
                    {
                        GameStoryManager.Instance.SetManagerValuesToAgainCity1();
                    }
                    LevelManager.Instance.LoadScene(nextScene);
                }
                _choicesActive = false;
                choicesPanel.SetActive(false);
                playerController.ActivatePlayerMovement(true);
            }
        }
    }
    
}
