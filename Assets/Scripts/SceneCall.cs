using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCall : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void StartScene()
    {
        LevelManager.Instance.LoadScene(sceneName);
    }
}
