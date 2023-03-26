using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantManager : MonoBehaviour
{
    void Awake()
    {
        GameStoryManager.Instance.allowReset = false;
    }
}
