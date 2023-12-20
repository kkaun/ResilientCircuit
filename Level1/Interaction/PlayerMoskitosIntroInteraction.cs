using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoskitosIntroInteraction : MonoBehaviour
{
    public GameObject player;

    public LevelSceneManager sceneManager;

    public List<GameObject> moskitos;

    private float distanceToPlayer;
    public const float maxInteractionDistanceToPlayer = 5.0f;

    [NonSerialized]
    public bool isInConflict;


    void Start()
    {
        isInConflict = false;
    }

    void Update()
    {
        if (IsEnoughDistanceForInteraction()
            && !player.GetComponent<PlayerController>().ShouldBeDead()
            && !AreAllMobsDead())
        {
            StartFight();
        }
        if (isInConflict == false && AreAllMobsDead())
        {
            //Workaround; should make a callback if there'll be more
            //game mechanics instructions later
            sceneManager.DeactivateMechanicsHint();
        }
    }

    private void StartFight()
    {
        sceneManager.DeactivateDecisionButtons();
        sceneManager.DeactivateDialogContent();

        player.GetComponent<PlayerController>().EndStaticDialogue();
        player.GetComponent<PlayerController>().MaintainConflictWithNPC();

        sceneManager.SetMechanicsHintText(GameMechanicsTexts.fightControlsText);

        isInConflict = true;
    }

    public bool AreAllMobsDead()
    {
        return moskitos[0].GetComponent<MoskitoController>().ShouldBeDead()
            && moskitos[1].GetComponent<MoskitoController>().ShouldBeDead()
            && moskitos[1].GetComponent<MoskitoController>().ShouldBeDead();
    }

    public bool IsEnoughDistanceForInteraction()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        return (distanceToPlayer < maxInteractionDistanceToPlayer);
    }
}
