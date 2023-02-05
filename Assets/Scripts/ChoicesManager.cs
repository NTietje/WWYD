using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    private int _selectedChoice = 0;
    private bool _choicesActive;

    public static ChoicesManager Instance;

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
        _selectedChoice = 0;
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

    void Update()
    {
        if (_choicesActive)
        {
            if (Input.GetKeyDown(_downKey))
            {
                if (_selectedChoice < _choices.Count)
                {
                    _selectedChoice += 1;
                    int indicatorIndex = 0;
                    foreach (var indicaor in _indicators)
                    {
                        if (indicatorIndex == _selectedChoice)
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
                if (_selectedChoice > 0)
                {
                    _selectedChoice -= 1;
                    int indicatorIndex = 0;
                    foreach (var indicaor in _indicators)
                    {
                        if (indicatorIndex == _selectedChoice)
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
                string nextScene = ChoiceParser.GetSceneForChoiceType(_choices[_selectedChoice]);
                if (nextScene != null)
                {
                    LevelManager.Instance.LoadScene(nextScene);
                } else if (nextScene == "NONE")
                {
                    
                }
                _choicesActive = false;
                choicesPanel.SetActive(false);
                playerController.ActivatePlayerMovement(true);
            }
        }
    }
    
}
