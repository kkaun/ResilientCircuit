using System;
using UnityEngine;

public class IntroLevelObjectivesManager : MonoBehaviour
{

    private bool optStage0AlreadyInteracted; //Monster dialog 1 - not necessary stage to program
    private bool stage1AlreadyInteracted; //Received objective to fix ATM2
    private bool stage1TaskResolved; //Retrieved Mini-Reactor from Monster or ATM1
    private bool stage2TaskResolved; //Fixed ATM2 - gate should open

    void Start()
    {
        //optStage0AlreadyInteracted = false;
        stage1AlreadyInteracted = false;
        stage1TaskResolved = false;
        stage2TaskResolved = false;
    }

    private void Update()
    {
    }

    public void SetOptStage0AsInteracted()
    {
        Debug.Log("STAGE 0 INTERACTED");
        optStage0AlreadyInteracted = true;
    }

    public bool IsOptStage0Interacted()
    {
        //Debug.Log("IsOptStage0Interacted: " + optStage0AlreadyInteracted);
        return optStage0AlreadyInteracted;
    }

    public void SetStage1AsInteracted()
    {
        Debug.Log("STAGE 1 INTERACTED");
        stage1AlreadyInteracted = true;
    }

    public bool IsStage1Interacted()
    {
        //Debug.Log("stage1AlreadyInteracted: " + stage1AlreadyInteracted);
        return stage1AlreadyInteracted;
    }

    public void SetStage1TaskAsResolved()
    {
        Debug.Log("STAGE 1 RESOLVED");
        stage1TaskResolved = true;
    }

    public bool IsStage1TaskResolved()
    {
        return stage1TaskResolved;
    }

    public void SetStage2TaskAsResolved()
    {
        Debug.Log("STAGE 2 RESOLVED");
        stage2TaskResolved = true;
    }

    public bool IsStage2TaskResolved()
    {
        return stage2TaskResolved;
    }
}
