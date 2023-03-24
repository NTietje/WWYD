using System;
using System.Collections;
using System.Collections.Generic;
using Polyperfect.Common;
using UnityEngine;
using UnityEngine.AI;

public enum MovementType
{
    InHouse,
    OutOfHouse
}

public class NpcMovable : MonoBehaviour
{
    [SerializeField] private Animator npcAnimator;
    
    NavMeshAgent meshAgent = null;
    
    private string idleAnimationBool = "isIdling";
    
    private string walkingAnimationBool = "isWalking";
    private string runningAnimationBool = "isRunning";
    private string standingAnimationBool = "isStanding";

    private string lastAnimationBool = null;
    private bool enableMove = false;
    private GameObject destination = null;
    private MovementType movementTyoe;

    private void Start()
    {
        lastAnimationBool = idleAnimationBool;
        meshAgent = GetComponent<NavMeshAgent>();
        if (meshAgent != null)
        {
            Debug.Log("mashAgent is NOT null");
        }
    }

    private void Update()
    {
        if (enableMove)
        {
            meshAgent.SetDestination(destination.transform.position);
            if (Vector3.Distance(meshAgent.transform.position, destination.transform.position) < 0.5)
            {
                Debug.Log("destination reached, enableMove set to false");
                enableMove = false;
                SetToIdle();
            }
        }
    }

    public void WalkTo(GameObject destinationObject)
    {
        destination = destinationObject;
        setAnimationBool(walkingAnimationBool);
        enableMove = true;
    }
    
    public void RunTo(GameObject destinationObject)
    {
        meshAgent.speed = 9f;
        destination = destinationObject;
        setAnimationBool(runningAnimationBool);
        enableMove = true;
    }
    
    public void SetToStand()
    {
        setAnimationBool(standingAnimationBool);
    }
    
    public void SetToIdle()
    {
        npcAnimator.SetBool(walkingAnimationBool, false);
        npcAnimator.SetBool(runningAnimationBool, false);
        npcAnimator.SetBool(idleAnimationBool, true);
    }

    private void setAnimationBool(string animationBool)
    {
        if (npcAnimator != null)
        {
            if (lastAnimationBool != null)
            {
                npcAnimator.SetBool(idleAnimationBool, false);
                npcAnimator.SetBool(lastAnimationBool, false);
            }
            npcAnimator.SetBool(animationBool, true);
            lastAnimationBool = animationBool;
        }
    }
}
