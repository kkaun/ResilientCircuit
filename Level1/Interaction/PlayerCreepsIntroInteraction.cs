using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreepsIntroInteraction : MonoBehaviour
{
    public GameObject player;

    public LevelSceneManager sceneManager;

    public List<GameObject> creeps;

    private float distanceToPlayer;
    public const float maxInteractionDistanceToPlayer = 6.0f;

    [NonSerialized]
    public bool isReadyForInteraction;
    
    [NonSerialized]
    public bool isInDialogue;
    [NonSerialized]
    public bool isInConflict;


    void Start()
    {
        isReadyForInteraction = false;

        isInConflict = false;
        isInDialogue = false;
    }

    void Update()
    {
        if (IsEnoughDistanceForInteraction() && !isReadyForInteraction
            && !player.GetComponent<PlayerController>().ShouldBeDead()
            && !AreAllCreepsDead())
        {
            GroupInteract();
        }
    }

    public bool AreAllCreepsDead()
    {
        return creeps[0].GetComponent<CreepController>().ShouldBeDead()
            && creeps[1].GetComponent<CreepController>().ShouldBeDead();
    }

    private void GroupInteract()
    {
        creeps[0].GetComponent<CreepController>().PrepareForInteraction();
        creeps[1].GetComponent<CreepController>().PrepareForInteraction();
    }

    public bool IsEnoughDistanceForInteraction()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        return (distanceToPlayer < maxInteractionDistanceToPlayer);
    }

    public void PrepareForPlayerDecision()
    {
        if (!isInDialogue)
        {
            isInDialogue = true;
            player.GetComponent<PlayerController>().StartStaticDialogue();

            sceneManager.ActivateDecisionButtons();

            sceneManager.SetPeacefulDecisionText(DialogueIntroTexts.playerSidePhase2Actions[0]);
            sceneManager.SetAgressiveDecisionText(DialogueIntroTexts.playerSidePhase2Actions[1]);

            sceneManager.SetPeacefulDecisionClickListener(LeaveInPeace);
            sceneManager.SetAgressiveDecisionClickListener(StartFight);
        }
    }

    private void StartFight()
    {
        GameObject.Find("CrabMonster")
            .GetComponent<PlayerMonsterIntroInteraction>().isInConflict = false;

        sceneManager.DeactivateDecisionButtons();
        sceneManager.DeactivateDialogContent();

        player.GetComponent<PlayerController>().EndStaticDialogue();
        player.GetComponent<PlayerController>().MaintainConflictWithNPC();

        isInDialogue = false;
        isInConflict = true;
    }

    private void LeaveInPeace()
    {
        isInConflict = false;
        isInDialogue = false;

        player.GetComponent<PlayerController>().EndStaticDialogue();

        sceneManager.DeactivateAllInterationContent();
        sceneManager.ClearDialogContent();
    }
}
