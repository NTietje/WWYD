using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image panelBackground;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Color fadeColor = Color.magenta;
    [SerializeField] private bool fadingAtStart = false;
    
    private bool panelIsOpaque = false;
    private bool _loadProgressActive = false;
    private float _target;
    private UnityEngine.AsyncOperation _scene;

    private float colorFadeStep = 0.05f; // adjust these to change how many fade color steps will be made, low value = many steps, many steps = more time
    private float colorFadeWaitTilNextStep = 0.00001f; // adjust the time between fade steps

    // private string _nextScene;

    // public void setNextScene(string scene)
    // {
    //     _nextScene = scene;
    // }

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

    public async void LoadScene(string sceneName, int delayMilliSec = 1000)
    {
        if (sceneName != null & sceneName != "")
        {
            Debug.Log("in LoadScene");
            await Task.Delay(delayMilliSec);
            
            // _loadProgressActive = true;
            // progressBar.value = 0;
            _target = 0;
            _scene = SceneManager.LoadSceneAsync(sceneName);
            _scene.allowSceneActivation = false;
            StartCoroutine(FadeInPanel());
        }
    }

    private IEnumerator FadeInPanel()
    {
        panelBackground.color =  fadeColor.WithAlpha(0);
        loadingPanel.SetActive(true);
        Debug.Log("will start fade in");

        for (float i = 0; i <= 1; i += colorFadeStep) // Loop through alpha values from 0 to 1
        {
            Color fadedColor = fadeColor.WithAlpha(i); // Create a new color with the current alpha
            panelBackground.color = fadedColor;
            yield return new WaitForSeconds(colorFadeWaitTilNextStep);
        }
        _scene.allowSceneActivation = true;
    }
    
    public IEnumerator FadeOutPanel()
    {
        panelBackground.color =  fadeColor.WithAlpha(1);
        loadingPanel.SetActive(true);
        Debug.Log("will start fade out");

        for (float i = 1; i >= 0; i -= colorFadeStep) // Loop through alpha values from 1 to 0
        {
            Color fadedColor = fadeColor.WithAlpha(i); // Create a new color with the current alpha
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

    // private void Update()
    // {
    //     if (_loadProgressActive)
    //     {
    //         progressBar.value = Mathf.MoveTowards(progressBar.value, _target, 3 * Time.deltaTime); 
    //     }
    // }
}

public static class ColorExtensions
{
    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
