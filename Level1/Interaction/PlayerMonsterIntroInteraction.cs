using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerMonsterIntroInteraction : MonoBehaviour
{
    public GameObject player;

    public LevelSceneManager sceneManager;

    public GameObject miniReactor;

    private IntroLevelObjectivesManager levelObjectivesManager;

    private MonsterController monsterController;

    [NonSerialized]
    public float distanceToPlayer;
    public const float maxInteractionDistanceToPlayer = 4.5f;
    public const float maxFightDistanceToPlayer = 3.5f;
    private const float maxDistanceForSoundToPlayer = 4.5f;

    [NonSerialized]
    public int interactionPhase;
    [NonSerialized]
    public bool isInDialogue;
    [NonSerialized]
    public bool isInConflict;

    private float currentDialogueStageDelay;
    private string currentDialogueStr;


    void Start()
    {
        monsterController = GameObject.Find("CrabMonster").GetComponent<MonsterController>();

        levelObjectivesManager = GameObject.Find("ObjectivesManager")
                    .GetComponent<IntroLevelObjectivesManager>();

        interactionPhase = 1;

        isInDialogue = false;
        isInConflict = false;

        currentDialogueStageDelay = 0f;
        currentDialogueStr = "";
    }

    void Update()
    {
        //Debug.Log("[UPDATE]INTER.PHASE: " + interactionPhase);
        //Debug.Log("[UPDATE]IS IN DIALOGUE: " + isInDialogue);

        if (IsEnoughDistanceForInteraction()
            && !player.GetComponent<PlayerController>().ShouldBeDead()
            && !IsMonsterDead())
        {
            if (!isInConflict && !isInDialogue && interactionPhase == 1
                && !levelObjectivesManager.IsOptStage0Interacted()
                && !levelObjectivesManager.IsStage1Interacted())
            {
                monsterController.PrepareForInteraction();
                PrepareForPlayerDecision();
            }
            if (!isInDialogue
                && levelObjectivesManager.IsOptStage0Interacted()
                && levelObjectivesManager.IsStage1Interacted()
                && !levelObjectivesManager.IsStage1TaskResolved())
            {
                interactionPhase = 2;

                monsterController.PrepareForInteraction();
                ContinueMonsterInteraction();
            }
            UpdateDialogueContent();
        }
    }

    private void ContinueMonsterInteraction()
    {
        if (!IsMonsterDead() && levelObjectivesManager.IsOptStage0Interacted()
                && levelObjectivesManager.IsStage1Interacted())
        {
            sceneManager.DeactivateMechanicsHint();

            player.GetComponent<PlayerController>().StartStaticDialogue();

            sceneManager.DeactivateDecisionButtons();

            sceneManager.ActivateDialogContent();

            AwakePhaseDialogue();
        }
    }

    public void ForceEndDialogue()
    {
        isInDialogue = false;
    }

    public void PrepareForPlayerDecision()
    {
        if (interactionPhase == 1 && !levelObjectivesManager.IsOptStage0Interacted())
        {
            player.GetComponent<PlayerController>().StartStaticDialogue();

            sceneManager.ActivateDecisionButtons();

            sceneManager.SetPeacefulDecisionText(DialogueIntroTexts.playerSidePhase1Actions[0]);
            sceneManager.SetAgressiveDecisionText(DialogueIntroTexts.playerSidePhase1Actions[1]);

            sceneManager.SetPeacefulDecisionClickListener(AwakePhaseDialogue);
            sceneManager.SetAgressiveDecisionClickListener(StartFight);
        }
    }

    public bool IsEnoughDistanceForInteraction()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        return (distanceToPlayer < maxInteractionDistanceToPlayer);
    }

    public bool IsEnoughDistanceForCombat()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        return (distanceToPlayer < maxFightDistanceToPlayer);
    }

    public bool IsAcceptableDistanceForSound()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        return (distanceToPlayer < maxDistanceForSoundToPlayer);
    }

    private void StartFight()
    {
        sceneManager.DeactivateDecisionButtons();

        player.GetComponent<PlayerController>().EndStaticDialogue();
        player.GetComponent<PlayerController>().MaintainConflictWithNPC();

        isInDialogue = false;
        isInConflict = true;
    }

    private void AwakePhaseDialogue() 
    {
        //!

        List<IEnumerator> enumerators = new List<IEnumerator>();

        for (int i = 0; i < GetTextsForCurrentDialogue().Count; i++)
        {
            enumerators.Add(MonsterDialogueTask(i, interactionPhase));
        }

        isInConflict = false;
        isInDialogue = true;

        StartCoroutine(DialogueEnumeratorSequence(enumerators.ToArray()));
    }

    IEnumerator MonsterDialogueTask(int currentDialogueIdx, int phase)
    {
        if (isInDialogue)
        {
            currentDialogueStageDelay = GetTextsForCurrentDialogue()[currentDialogueIdx].Value;
            currentDialogueStr = GetTextsForCurrentDialogue()[currentDialogueIdx].Key;

            if (GetTextsForCurrentDialogue()[currentDialogueIdx].Key == DialogueIntroTexts.endDialogMarker)
            {
                levelObjectivesManager.SetOptStage0AsInteracted();

                isInDialogue = false;

                if (phase == 2)
                {
                    interactionPhase = 3;
                    miniReactor.GetComponent<MiniReactorController>().HangOnPlayer();
                }

                player.GetComponent<PlayerController>().EndStaticDialogue();

                sceneManager.DeactivateAllInterationContent();
                sceneManager.DeactivateDecisionButtons();
                sceneManager.ClearDialogContent();
            }

            yield return new WaitForSeconds(currentDialogueStageDelay);
        }
    }

    private List<KeyValuePair<string, float>> GetTextsForCurrentDialogue()
    {
        if (interactionPhase == 1 && !levelObjectivesManager.IsOptStage0Interacted())
        {
            return DialogueIntroTexts.playerMonsterDialogue1;
        }
        else
        {
            return DialogueIntroTexts.playerMonsterDialogue2;
            //return new List<KeyValuePair<string, float>>(); //no more dialogs with monster
        }
    }

    private IEnumerator DialogueEnumeratorSequence(params IEnumerator[] enumerators)
    {
        foreach (IEnumerator _e in enumerators)
        {
            yield return StartCoroutine(_e);
        }
    }

    private void UpdateDialogueContent()
    {
        if (isInDialogue)
        {
            sceneManager.DeactivateDecisionButtons();
            sceneManager.ActivateDialogContent();

            sceneManager.SetDialogText(currentDialogueStr);

            if (currentDialogueStr == DialogueIntroTexts.switchDialogSideToPlayer)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.playerDialogTitle);

                player.GetComponent<PlayerController>().AllowTalkingAudio();
                monsterController.RestrictTalkingAudio();
            }
            if (currentDialogueStr == DialogueIntroTexts.switchDialogSideToMonster)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.monsterDialogTitle);

                player.GetComponent<PlayerController>().RestrictTalkingAudio();
                monsterController.AllowTalkingAudio();
            }
            if (currentDialogueStr == DialogueIntroTexts.endDialogMarker)
            {
                monsterController.EndInteraction();
            }

        }
    }

    public bool IsMonsterDead()
    {
        return monsterController.ShouldBeDead();
    }
}