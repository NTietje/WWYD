using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int sceneID)
    {
        Debug.Log("load scene in SceneChanger");
        SceneManager.LoadScene(sceneID);
    }
}
