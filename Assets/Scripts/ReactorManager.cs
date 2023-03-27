using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorManager : MonoBehaviour
{
    void Start()
    {
        GameStoryManager.Instance.allowReset = false;
    }

    public void SetToDeadAndVisited()
    {
        GameStoryManager.Instance.visitedReactor = true;
        GameStoryManager.Instance.wasDead = true;
    }
    
}
