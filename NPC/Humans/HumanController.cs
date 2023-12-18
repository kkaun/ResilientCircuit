using System;
using System.Collections;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public int humanIndex;

    private Animator animator;

    private float commonBehaviorTimeSec = 1.5f;

    private bool isPlayerNearby;

    private const float minSoundLength = 2.5f;
    private const float maxSoundLength = 3.5f;

    void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(HumanBehaviorTask());

        if (humanIndex == 1)
        {
            StartCoroutine(TalkTask(10f));
        }
    }

    void Update()
    {
        
    }

    public void ShowRandomBehaviour(int randomBehaviorIdx)
    {
        switch (randomBehaviorIdx)
        {
            case 0:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior1));
                break;
            case 1:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior2));
                break;
            case 2:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior3));
                break;
            case 3:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior4));
                break;
            case 4:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior5));
                break;
            case 5:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior6));
                break;
            case 6:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior7));
                break;
            default:
                animator.SetTrigger(nameof(HumanAnimBehaviours.WeirdBehavior8));
                break;
        }
    }

    IEnumerator HumanBehaviorTask()
    {
        int randomBehaviorIdx = UnityEngine.Random.Range(0, 7);

        ShowRandomBehaviour(randomBehaviorIdx);

        yield return new WaitForSeconds(commonBehaviorTimeSec);

        StartCoroutine(HumanBehaviorTask());
    }

    IEnumerator TalkTask(float randomSoundLength)
    {
        float randSoundLength = UnityEngine.Random.Range(minSoundLength, maxSoundLength);

        if (isPlayerNearby)
        {
            GetComponent<AudioSource>().Stop();

            int randomDialogSound = UnityEngine.Random.Range(1, 10); //number of sounds available

            AudioClip clip = Resources.Load<AudioClip>("Sounds/HumanVoices/Conv" + randomDialogSound);

            GetComponent<AudioSource>().PlayOneShot(clip);
        }

        yield return new WaitForSeconds(randSoundLength);

        StartCoroutine(TalkTask(randomSoundLength));
    }

    public void InformIsPlayerNearby(bool isNearby)
    {
        isPlayerNearby = isNearby;
    }

}
