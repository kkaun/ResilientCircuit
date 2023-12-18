using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlowManager : MonoBehaviour
{

    public SceneManager sceneManager;

    public GameObject miniReactor;

    private PlayerController playerController;


    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
    }

    public void UpdateSceneStatesOnStageMonologueFinish(string currentMonologueId)
    {
        if (currentMonologueId == "playerStartMonologue")
        {
            playerController.EndMalfunctioningState();

            sceneManager.ActivateMechanicsHint();
            sceneManager.SetMechanicsHintText(GameMechanicsTexts.movementControlsText);
        }
        if (currentMonologueId == "atm2ToFixMonologue")
        {
            GameObject.Find("ObjectivesManager")
                .GetComponent<IntroLevelObjectivesManager>().SetStage1AsInteracted();

            sceneManager.ActivateMechanicsHint();
            sceneManager.SetMechanicsHintText(GameMechanicsTexts.stage2AchievementHint);
        }
        if (currentMonologueId == "atm1MonologueMonsterDead")
        {
            sceneManager.DeactivateMechanicsHint();

            miniReactor.GetComponent<MiniReactorController>().HangOnPlayer();

            playerController.FulfillHealthToMax();
        }
        if (currentMonologueId == "atm2FixedMonologue")
        {
            miniReactor.GetComponent<MiniReactorController>().DropFromPlayer();
        }

        sceneManager.DeactivateAllInterationContent();
        sceneManager.ClearDialogContent();
    }
}
