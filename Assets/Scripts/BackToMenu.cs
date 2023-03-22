using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private KeyCode _resetKey = KeyCode.R;

void Update() 
    {     
        if (Input.GetKeyDown (_resetKey))
        {
            SceneManager.LoadScene (0);
        }

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            SceneManager.LoadScene (0);
        }


    }
}