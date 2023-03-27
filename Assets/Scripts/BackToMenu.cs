using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{


void Update() 
    {     
        
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            GameStoryManager.Instance.HardResetValues();
            SceneManager.LoadScene(0);
        }

    }
}