using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class HouseAnimationManager : MonoBehaviour
{
    [SerializeField] private int delay;
    [SerializeField] private Animator animatorTür1;
    [SerializeField] private NpcMovable npcMovable = null;
    [SerializeField] private GameObject destinationInHouse = null;
    [SerializeField] private GameObject destinationOutOfHouse = null;

    // private void Start()
    // {
    //     if (npcMovable != null & destinationInHouse != null)
    //     {
    //         Debug.Log("start walk to");
    //         npcMovable.WalkTo(destinationInHouse);
    //     }
    // }

    public void CheckEvidence()
    {
        if (GameStoryManager.Instance.CheckAllEvidenceFound())
        {
            Debug.Log("all evidence found !!");
            StartCoroutine(StartAnimationProcess1());
        }
        else
        {
            Debug.Log("not all evidence !!!!");
        }
    }

    private IEnumerator StartAnimationProcess1()
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log("start animation");
 
        if (npcMovable != null & destinationInHouse != null)
        {
            Debug.Log("start walk to");
            npcMovable.WalkTo(destinationInHouse);
        }
        if (animatorTür1 != null)
        {
            yield return new WaitForSeconds(1);
            animatorTür1.SetBool("enable", true);
        }

    }
}
