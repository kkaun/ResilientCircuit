using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueIntroLevelManager : MonoBehaviour
{
    private LevelSceneManager sceneManager;

    private LevelFlowManager levelFlowManager;

    private PlayerController playerController;

    private List<KeyValuePair<string, float>> currentMonologue;
    private string currentMonologueId;

    private bool isInMonologue;

    private float currentReplicStageDelay;
    private string currentReplicStr;


    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        sceneManager = GameObject.Find("SceneManager").GetComponent<LevelSceneManager>();

        levelFlowManager = GameObject.Find("LevelFlowManager").GetComponent<LevelFlowManager>();

        sceneManager.DeactivateDecisionButtons();
        sceneManager.SetDialogTitle(DialogueIntroTexts.playerDialogTitle);

        StartCoroutine(DelayedStartMonologue());
    }
    
    void Update()
    {
        UpdateMonologueContent();
    }

    public void StartNewMonologue(List<KeyValuePair<string, float>> monologue, string monologueId)
    {
        currentMonologue = monologue;
        currentMonologueId = monologueId;

        if (!playerController.IsInStaticDialogue())
        {
            playerController.StartStaticDialogue();

            AwakeMonologue();
        }
    }

    private void AwakeMonologue()
    {
        isInMonologue = true;

        List<IEnumerator> enumerators = new List<IEnumerator>();

        for (int i = 0; i < currentMonologue.Count; i++)
        {
            enumerators.Add(MonologueTask(i));
        }

        StartCoroutine(MonologueEnumeratorSequence(enumerators.ToArray()));
    }

    IEnumerator MonologueTask(int currentReplicIdx)
    {
        if (isInMonologue)
        {
            currentReplicStageDelay = currentMonologue[currentReplicIdx].Value;
            currentReplicStr = currentMonologue[currentReplicIdx].Key;

            if (currentMonologue[currentReplicIdx].Key == DialogueIntroTexts.endDialogMarker)
            {
                isInMonologue = false;

                playerController.EndStaticDialogue();

                levelFlowManager.UpdateSceneStatesOnStageMonologueFinish(currentMonologueId);
            }

            yield return new WaitForSeconds(currentReplicStageDelay);
        }
    }

    

    private IEnumerator MonologueEnumeratorSequence(params IEnumerator[] enumerators)
    {
        foreach (IEnumerator _e in enumerators)
        {
            yield return StartCoroutine(_e);
        }
    }

    private void UpdateMonologueContent()
    {
        if (isInMonologue)
        {
            sceneManager.DeactivateDecisionButtons();
            sceneManager.ActivateDialogContent();
            sceneManager.SetDialogText(currentReplicStr);

            if (currentReplicStr == DialogueIntroTexts.switchDialogSideToPlayer)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.playerDialogTitle);
            }
            if (currentReplicStr == DialogueIntroTexts.switchDialogSideToHumanMale)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.humanMaleDialogTitle);
            }
            if (currentReplicStr == DialogueIntroTexts.switchDialogSideToHumanFemale)
            {
                sceneManager.SetDialogTitle(DialogueIntroTexts.humanFemaleDialogTitle);
            }
            //may also add another chars is they'll 'participate' in monologue
        }
    }

    IEnumerator DelayedStartMonologue()
    {
        yield return new WaitForSeconds(1.5f);

        StartNewMonologue(DialogueIntroTexts.playerStartMonologue, "playerStartMonologue");
    }
}
