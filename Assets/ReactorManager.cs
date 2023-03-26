using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorManager : MonoBehaviour
{
    void Start()
    {
        GameStoryManager.Instance.allowReset = false;
    }
}
