using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject videoLayer;
    [SerializeField] private VideoPlayer videoPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowGameMenu();
        HideIntroPanel();
        ShowVideoLayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartVideoAndIntoFlow()
    {
        StartCoroutine(VideoFlow());
    }
    
    private IEnumerator VideoFlow()
    {
        introPanel.SetActive(true);
        gameMenu.SetActive(false);
        yield return new WaitForSeconds(35);
        videoPlayer.Play();
        yield return new WaitForSeconds(5);
        introPanel.SetActive(false);
        yield return new WaitForSeconds(43);
        LevelManager.Instance.LoadScene("02_Wasteland");
        videoLayer.SetActive(false);
    }
    
    public void HideIntroPanel()
    {
        introPanel.SetActive(false);
    }
    
    public void ShowIntroPanel()
    {
        introPanel.SetActive(true);
    }

    public void HideGameMenu()
    {
        gameMenu.SetActive(false);
    }

    public void ShowGameMenu()
    {
        gameMenu.SetActive(true);
    }

    public void HideVideoLayer()
    {
        videoLayer.SetActive(false);
    }

    public void ShowVideoLayer()
    {
        videoLayer.SetActive(true);
    }

}
