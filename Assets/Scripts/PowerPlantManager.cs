using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantManager : MonoBehaviour
{
    [SerializeField] private SceneCall _sceneCall;
    void Awake()
    {
        GameStoryManager.Instance.allowReset = false;
    }

    public void StartExplosion()
    {
        GameStoryManager.Instance.wasDead = true;
        _sceneCall.StartAnimatorAndScene();
    }
}
