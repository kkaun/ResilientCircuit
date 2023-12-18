using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGateController : MonoBehaviour
{
    private IntroLevelObjectivesManager introLevelObjectivesManager;

    private float slideSpeed = 0.015f;
    private float slideLimitX = 15.0f;
    private float counter = 0;
    private float slideDuration = 1.5f;


    private void Start()
    {
        introLevelObjectivesManager = GameObject.Find("ObjectivesManager")
                    .GetComponent<IntroLevelObjectivesManager>();
    }

    void Update()
    {
        if(counter < slideDuration)
        //if (introLevelObjectivesManager.IsStage2TaskResolved())
        {
            OpenGateSlowly();
            counter += Time.deltaTime;
        }
    }

    private void OpenGateSlowly()
    {
        transform.Translate(Vector3.right * slideSpeed);
    }
}
