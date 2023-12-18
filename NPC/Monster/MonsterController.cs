using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public GameObject player;

    //By adding new levels, may add more interaction scripts as more separated modules
    //and switch them by checking current level
    private PlayerMonsterIntroInteraction playerMonsterIntroInteraction;

    private Animator animator;

    private float health;

    public const float damage = 3f;
	private const float maxRotationSpeed = 12.0f;
    private const float moveSpeed = 2f;

    private int randomAttackAnimIdx = 0;
    private int randomSufferAnimIdx = 0;

    private bool isAllowedToTalk;
    private const float minSoundLength = 1f;
    private const float maxSoundLength = 2.2f;


    void Start () {
        playerMonsterIntroInteraction = gameObject
            .GetComponent<PlayerMonsterIntroInteraction>();

        animator = GetComponent<Animator>();

        health = 25f;

        isAllowedToTalk = false;
        StartCoroutine(SoundTask(8f));
    }
	
	void Update () {

        if (playerMonsterIntroInteraction.isInConflict
            && player.GetComponent<PlayerController>().IsInConflict()
            && !ShouldBeDead())
        {
            if (playerMonsterIntroInteraction.IsEnoughDistanceForCombat())
            {
                PerformRandomAttack();
            } else
            {
                PursuePlayer();
            }
        }
        if (!playerMonsterIntroInteraction.isInConflict && !ShouldBeDead())
        {
            CheckAndUpdateTalikngMovement();
        }
    }

    private void PursuePlayer()
    {
        animator.SetTrigger(nameof(MonsterCombatActions.Walk_Cycle_2));

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.LookAt(lookDirection);

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void PerformRandomAttack()
    {
        player.GetComponent<PlayerController>().MaintainConflictWithNPC();

        randomAttackAnimIdx = Random.Range(0, 4);

        switch (randomAttackAnimIdx)
        {
            case 0:
                animator.SetTrigger(nameof(MonsterCombatActions.Attack_1));
                break;
            case 1:
                animator.SetTrigger(nameof(MonsterCombatActions.Attack_2));
                break;
            case 2:
                animator.SetTrigger(nameof(MonsterCombatActions.Attack_3));
                break;
            case 3:
                animator.SetTrigger(nameof(MonsterCombatActions.Attack_4));
                break;
            default:
                animator.SetTrigger(nameof(MonsterCombatActions.Attack_5));
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckForDamageTaken(other);
    }

    private void CheckForDamageTaken(Collider other)
    {
        if (player.GetComponent<PlayerController>().IsInConflict()
            && playerMonsterIntroInteraction.isInConflict
            && playerMonsterIntroInteraction.IsEnoughDistanceForCombat()
            && other.gameObject.CompareTag("PlayerWeapon"))
        {
            player.GetComponent<PlayerController>().PlayCombatSmashSound();

            TakeDamage(PlayerController.damage);
            ProcessDamage();
        }
    }

    private void ProcessDamage()
    {
        if (ShouldBeDead())
        {
            Die();
        }
        else
        {
            randomSufferAnimIdx = Random.Range(0, 3);

            switch (randomSufferAnimIdx)
            {
                case 0:
                    animator.SetTrigger(nameof(MonsterCombatActions.Take_Damage_1));
                    break;
                case 1:
                    animator.SetTrigger(nameof(MonsterCombatActions.Take_Damage_2));
                    break;
                default:
                    animator.SetTrigger(nameof(MonsterCombatActions.Take_Damage_3));
                    break;
            }
        }
    }

    private void Die()
    {
        playerMonsterIntroInteraction.isInConflict = false;

        player.GetComponent<PlayerController>().EndConflictWithNPC();

        animator.SetTrigger(nameof(MonsterCombatActions.Die));

        animator.SetBool("IsAlive", false);
    }

    public void TakeDamage(float damage)
	{
		health -= damage;
        //Debug.Log("MONSTER TOOK DAMAGE: " + damage + " | MONSTER HEALTH: " + health);
	}

    public bool ShouldBeDead()
    {
        return health < 0;
    }

	public void ShowExcitement()
    {
		animator.SetTrigger(nameof(MonsterCombatActions.Take_Damage_3));
	}

    public void PrepareForInteraction()
    {
        Vector3 targetDirection = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);

        // Check if the rotation is almost complete
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        if (angleDifference < 1.0f)
        {
            ShowExcitement();
        }
    }

    public void AllowTalkingAudio()
    {
        isAllowedToTalk = true;
    }

    public void RestrictTalkingAudio()
    {
        isAllowedToTalk = false;
    }

    IEnumerator SoundTask(float randomSoundLength)
    {
        float randSoundLength = Random.Range(minSoundLength, maxSoundLength);

        if (playerMonsterIntroInteraction.IsAcceptableDistanceForSound() && !ShouldBeDead())
        {
            GetComponent<AudioSource>().Stop();

            if (playerMonsterIntroInteraction.isInConflict)
            {
                PlayRandomCombatSound();
            }
            else if (playerMonsterIntroInteraction.isInDialogue && isAllowedToTalk)
            {
                PlayRandomDialogueSound();
            }
        }

        yield return new WaitForSeconds(randSoundLength);

        StartCoroutine(SoundTask(randomSoundLength));
    }

    private void PlayRandomDialogueSound()
    {
        int randomCombatSound = Random.Range(1, 13);  //number of sounds available

        AudioClip clip = Resources.Load<AudioClip>("Sounds/MonsterVoices/Monster" + randomCombatSound);

        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    private void PlayRandomCombatSound()
    {
        int randomCombatSound = Random.Range(1, 9);  //number of sounds available

        AudioClip clip = Resources.Load<AudioClip>("Sounds/Mobs/MonsterCreeps/Creep" + randomCombatSound);

        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    private void CheckAndUpdateTalikngMovement()
    {
        if (isAllowedToTalk)
        {
            animator.SetTrigger("Talk");
            animator.SetBool("IsTalking", true);
        }
        else
        {
            animator.SetBool("IsTalking", false);
        }
    }
}
