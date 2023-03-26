using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 
public class ResetScene : MonoBehaviour
{
    private KeyCode _resetKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown (_resetKey))
        {
            Restart();
        }
    }
 
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}