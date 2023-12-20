using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHumanIntroInteraction : MonoBehaviour
{
    public GameObject player;

    public LevelSceneManager sceneManager;

    public List<GameObject> humans;

    private float distanceToPlayer;
    public const float maxInteractionDistanceToPlayer = 5.0f;

    [NonSerialized]
    public int interactionPhase; //just 1 for now

    [NonSerialized]
    public bool isInDialogue;

    private float currentDialogueStageDelay;
    private string currentDialogueStr;


    void Start()
    {
        interactionPhase = 1;

        isInDialogue = false;

        currentDialogueStageDelay = 0f;
        currentDialogueStr = "";
    }

    void Update()
    {
        if (IsEnoughDistanceForInteraction()
            && !player.GetComponent<PlayerController>().ShouldBeDead()
            && !isInDialogue
            && interactionPhase == 1)
        {
            AwakeDialogue();
        }
        UpdateDialogueContent();
    }

    public bool IsEnoughDistanceForInteraction()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        bool isNearby =  (distanceToPlayer < maxInteractionDistanceToPlayer);

        humans[0].GetComponent<HumanController>().InformIsPlayerNearby(isNearby);

        return isNearby;
    }

    private void AwakeDialogue()
    {
        isInDialogue = true;

        player.GetComponent<PlayerController>().StartStaticDialogue();

        List<IEnumerator> enumerators = new List<IEnumerator>();

        for (int i = 0; i < GetTextsForCurrentDialogue().Count; i++)
        {
            enumerators.Add(HumansDialogueTask(i, interactionPhase));
        }

        StartCoroutine(DialogueEnumeratorSequence(enumerators.ToArray()));
    }

    IEnumerator HumansDialogueTask(int currentDialogueIdx, int phase)
    {
        if (isInDialogue)
        {
            currentDialogueStageDelay = GetTextsForCurrentDialogue()[currentDialogueIdx].Value;
            currentDialogueStr = GetTextsForCurrentDialogue()[currentDialogueIdx].Key;

            if (GetTextsForCurrentDialogue()[currentDialogueIdx].Key == DialogueIntroTexts.endDialogMarker)
            {
                isInDialogue = false;

                interactionPhase += 1;

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
        if (interactionPhase == 1)
        {
            return DialogueIntroTexts.playerHumansDialogue1;
        }
        else
        {
            interactionPhase = 2;
            return new List<KeyValuePair<string, float>>();
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
            }
            if (currentDialogueStr == DialogueIntroTexts.switchDialogSideToHumanMale)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.humanMaleDialogTitle);
            }
            if (currentDialogueStr == DialogueIntroTexts.switchDialogSideToHumanFemale)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.humanFemaleDialogTitle);
            }
        }
    }

}
