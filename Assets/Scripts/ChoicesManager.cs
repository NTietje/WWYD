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
    private KeyCode _upKey = KeyCode.LeftArrow;
    private KeyCode _downKey = KeyCode.RightArrow;
    private KeyCode _enter = KeyCode.Return;
    private int _selectedChoiceIndex = 0;
    private bool _choicesActive;
    private bool _allowEventAfterDialogue = false;
    private float _eventDelay;
    private ChoiceType currentChoiceType;

    public static ChoicesManager Instance;

    public ChoiceType GetCurrentChoiceType()
    {
        return currentChoiceType;
    }

    public bool getIsActive()
    {
        return _choicesActive;
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
        // if (!DialogueManager.Instance.getIsDialogueActive() & _allowEventAfterDialogue)
        // {
        //     Debug.Log("dialog off, will invoke event");
        //     _allowEventAfterDialogue = false;
        //     StartCoroutine(ShowEvent(currentChoiceType));
        // }
        
        if (_choicesActive)
        {
            if (Input.GetKeyDown(_downKey))
            {
                if (_selectedChoiceIndex < _choices.Count)
                {
                    _selectedChoiceIndex += 1;
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
                currentChoiceType = _choices[_selectedChoiceIndex];
                string nextScene = ChoiceParser.GetSceneForChoiceType(_choices[_selectedChoiceIndex]);
                if (nextScene == "NONE")
                {
                    switch (_choices[_selectedChoiceIndex])
                    {
                        case ChoiceType.ConfrontBadGuy:
                            DialogueManager.Instance.setDialogueType(DialogueType.ConfrontBadGuy);
                            DialogueManager.Instance.StartNewDialogue();
                            _eventDelay = 1;
                            _allowEventAfterDialogue = true;
                            
                            break;
                    }
                    
                } else
                {
                    LevelManager.Instance.LoadScene(nextScene);
                }
                _choicesActive = false;
                choicesPanel.SetActive(false);
                playerController.ActivatePlayerMovement(true);
            }
        }
    }
    
}
