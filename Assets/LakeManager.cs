using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeManager : MonoBehaviour
{
    void Start()
    {
        GameStoryManager.Instance.allowReset = false;
    }
}
