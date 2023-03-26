using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    void Awake()
    {
        GameStoryManager.Instance.allowReset = true;
        GameStoryManager.Instance.SubtractFromClockTime(0, 30, 0);
    }

    public void SetAxeFound()
    {
        GameStoryManager.Instance.AxeWasFound();
    }
    
    public void SetPaperFound()
    {
        GameStoryManager.Instance.PaperWereFound();
    }
    
    public void SetPlansFound()
    {
        GameStoryManager.Instance.PowerPlanWasFound();
    }

}
