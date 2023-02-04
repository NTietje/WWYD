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
    [SerializeField] private Slider progressBar;

    private float _target;
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        if (sceneName != null & sceneName != "")
        {
            progressBar.value = 0;
            _target = 0;
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
        
            loadingPanel.SetActive(true);

            do
            {
                await Task.Delay(100);
                _target = scene.progress;
            } while (scene.progress < 0.90f);
            
            scene.allowSceneActivation = true;

            await Task.Delay(2000);

            loadingPanel.SetActive(false);
        }
    }

    private void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, _target, 3 * Time.deltaTime);
    }
}
