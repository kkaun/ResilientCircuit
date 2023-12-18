using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATMsIntroInteraction : MonoBehaviour
{
    public List<GameObject> atms;

    private PlayerMonsterIntroInteraction playerMonsterIntroInteraction;

    private MonologueIntroLevelManager monologueIntroManager;

    private IntroLevelObjectivesManager levelObjectivesManager;


    void Start()
    {
        playerMonsterIntroInteraction = GameObject.Find("CrabMonster")
            .GetComponent<PlayerMonsterIntroInteraction>();

        monologueIntroManager = GameObject.Find("MonologueManager")
            .GetComponent<MonologueIntroLevelManager>();

        levelObjectivesManager = GameObject.Find("ObjectivesManager")
            .GetComponent<IntroLevelObjectivesManager>();
    }

    void Update()
    {
    }

    public void UpdateLevelATMState(int atmIndex)
    {
        if (atmIndex == 1)
        {
            if (playerMonsterIntroInteraction.IsMonsterDead()
                && !levelObjectivesManager.IsStage1TaskResolved())
            {
                levelObjectivesManager.SetStage1TaskAsResolved();

                monologueIntroManager.StartNewMonologue(
                    DialogueIntroTexts.atm1MonologueMonsterDead, "atm1MonologueMonsterDead");
            }
        }

        if (atmIndex == 2)
        {
            if (!levelObjectivesManager.IsStage1Interacted()
                && !levelObjectivesManager.IsStage1TaskResolved())
            {
                //Debug.Log("atmIndex == 2 ::: CASE 1");
                monologueIntroManager
                    .StartNewMonologue(DialogueIntroTexts.atm2ToFixMonologue, "atm2ToFixMonologue");
                //stage1AlreadyInteracted will be updated after monologue completion
            }
            else if (levelObjectivesManager.IsStage1Interacted()
                && levelObjectivesManager.IsStage1TaskResolved()
                && !levelObjectivesManager.IsStage2TaskResolved())
            {
                //Debug.Log("atmIndex == 2 ::: CASE 2");
                monologueIntroManager
                    .StartNewMonologue(DialogueIntroTexts.atm2FixedMonologue, "atm2FixedMonologue");
                //stage1AlreadyInteracted will be updated after monologue completion
            }
        }
    }
}
