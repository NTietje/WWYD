using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAnimationManager : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator animator2 = null;

    public void CheckEvidence()
    {
        if (GameStoryManager.Instance.CheckAllEvidenceFound())
        {
            Debug.Log("all evidence found !!");
            StartCoroutine(StartAnimationProcess());
        }
        else
        {
            Debug.Log("not all evidence !!!!");
        }
    }
    
    private IEnumerator StartAnimationProcess()
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log("start animation");
        if (animator != null)
        {
            animator.SetBool("enable", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("enable", true);
        }

    }
}
