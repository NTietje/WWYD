using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City2Manager : MonoBehaviour
{
    void Awake()
    {
        GameStoryManager.Instance.allowReset = true;
        GameStoryManager.Instance.SubtractFromClockTime(1, 30, 0);
    }

}
