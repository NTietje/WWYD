using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image panelBackground;
    [SerializeField] private Color fadeOutColor = Color.white;
    [SerializeField] private Color fadeInColor = Color.white;
    [SerializeField] private bool fadingAtStart = false;
    [SerializeField] private int fadingOutDelaySec = 0;
    
    private bool panelIsOpaque = false;
    private UnityEngine.AsyncOperation _scene;
    private float colorFadeStep = 0.05f; // adjust these to change how many fade color steps will be made, low value = many steps, many steps = more time
    private float colorFadeWaitTilNextStep = 0.00001f; // adjust the time between fade steps

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (fadingAtStart)
        {
            Debug.Log("start fade out in LevelManager Start");
            StartCoroutine(FadeOutPanel());
        }
        else
        {
            loadingPanel.SetActive(false);
        }
    }

    public async void LoadScene(string sceneName, int delayMilliSec = 500)
    {
        if (sceneName != null & sceneName != "")
        {
            Debug.Log("in LoadScene");
            await Task.Delay(delayMilliSec);
            checkSettingResetForScene(sceneName);
            _scene = SceneManager.LoadSceneAsync(sceneName);
            _scene.allowSceneActivation = false;
            StartCoroutine(FadeInPanel());
        }
    }

    private void checkSettingResetForScene(string newScene)
    {
        if (GameStoryManager.Instance.wasDead)
        {
            Debug.Log("was dead!");
            if (newScene == ChoiceParser.GetSceneForChoiceType(ChoiceType.BackToCity)) // go to city 1
            {
                GameStoryManager.Instance.SoftResetToCity1Values();
            } else if (newScene == ChoiceParser.GetSceneForChoiceType(ChoiceType.PeopleAfterEStation)) // go to city 2
            {
                GameStoryManager.Instance.SoftResetToCity2Values();
            }
        }
        else
        {
            Debug.Log("was NOT dead!");
        }

    }

    private IEnumerator FadeInPanel()
    {
        panelBackground.color =  fadeInColor.WithAlpha(0);
        loadingPanel.SetActive(true);
        Debug.Log("will start fade in");

        for (float i = 0; i <= 1; i += colorFadeStep) // Loop through alpha values from 0 to 1
        {
            Color fadedColor = fadeInColor.WithAlpha(i); // Create a new color with the current alpha
            panelBackground.color = fadedColor;
            yield return new WaitForSeconds(colorFadeWaitTilNextStep);
        }
        _scene.allowSceneActivation = true;
    }
    
    public IEnumerator FadeOutPanel()
    {
        panelBackground.color =  fadeOutColor.WithAlpha(1);
        loadingPanel.SetActive(true);
        Debug.Log("will start fade out");
        yield return new WaitForSeconds(fadingOutDelaySec);

        for (float i = 1; i >= 0; i -= colorFadeStep) // Loop through alpha values from 1 to 0
        {
            Color fadedColor = fadeOutColor.WithAlpha(i); // Create a new color with the current alpha
            panelBackground.color = fadedColor;
            yield return new WaitForSeconds(colorFadeWaitTilNextStep);
        }
        loadingPanel.SetActive(false);
    }

    public void LoadSceneImmediately(string sceneName)
    {
        if (sceneName != null & sceneName != "")
        {
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = true;
        }
    }
}

public static class ColorExtensions
{
    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
