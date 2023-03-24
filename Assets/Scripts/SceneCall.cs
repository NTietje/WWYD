using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

public class SceneCall : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private int delay;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator animator2 = null;

    public void StartScene()
    {
        Debug.Log("load scene in StartScene");
        LevelManager.Instance.LoadScene(sceneName);
    }

    public void StartAnimatorAndScene()
    {
        StartCoroutine(HandleAnimationAndSceneSwitch());
    }

    private IEnumerator HandleAnimationAndSceneSwitch()
    {
        Debug.Log("start animation");
        if (animator != null)
        {
            animator.SetBool("enable", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("enable", true);
        }
        
        if (sceneName != null)
        {
            Debug.Log("waiting");
            yield return new WaitForSeconds(delay);
            Debug.Log("start next scene");
            LevelManager.Instance.LoadScene(sceneName); 
        }
        
    }

}
