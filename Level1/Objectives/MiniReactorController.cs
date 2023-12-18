using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniReactorController : MonoBehaviour
{

    private IntroLevelObjectivesManager objectivesManager;

    void Start()
    {
        objectivesManager = GameObject.Find("ObjectivesManager")
            .GetComponent<IntroLevelObjectivesManager>();

        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    public void HangOnPlayer()
    {
        gameObject.SetActive(true);
        objectivesManager.SetStage1AsInteracted();
        objectivesManager.SetStage1TaskAsResolved();
    }

    public void DropFromPlayer()
    {
        gameObject.SetActive(false);
        objectivesManager.SetStage2TaskAsResolved();
    }

    public bool IsMiniReactorWorn()
    {
        return gameObject.activeSelf;
    }
}
