using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardResetGame : MonoBehaviour
{
    void Update() 
    {     
       
        GameStoryManager.Instance.HardResetValues();

    }
}
