using System.Collections;
using UnityEngine;

public class CreepController : MonoBehaviour
{
    public GameObject player;

    public int creepIndex;

    //By adding new levels, may add more interaction scripts as more separated modules
    //and switch them by checking current level
    private PlayerCreepsIntroInteraction playerCreepsIntroInteraction;

    private Animator animator;

    private float health;

    public const float damage = 2f;
    private const float maxRotationSpeed = 12.0f;
    private const float moveSpeed = 2f;

    private float distanceToPlayer;
    public const float maxFightDistanceToPlayer = 2.5f;
    public const float maxDistanceForSoundToPlayer = 4.0f;

    private int randomAttackAnimIdx = 0;
    private int randomSufferAnimIdx = 0;

    private const float minSoundLength = 2.0f;
    private const float maxSoundLength = 3.0f;


    void Start()
    {
        playerCreepsIntroInteraction = GameObject.Find("CreepsIntroInteraction")
            .GetComponent<PlayerCreepsIntroInteraction>();

        animator = GetComponent<Animator>();

        health = 23f;

        StartCoroutine(SoundTask(10f));
    }

    void Update()
    {
        if (playerCreepsIntroInteraction.isInConflict
            && player.GetComponent<PlayerController>().IsInConflict()
            && !ShouldBeDead())
        {
            transform.LookAt(player.transform);

            if (IsEnoughDistanceForCombat())
            {
                PerformRandomAttack();
            }
            else
            {
                PursuePlayer();
            }
        }
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

    private void PursuePlayer()
    {
        animator.SetTrigger(nameof(MonsterCombatActions.Walk_Cycle_2));

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void PerformRandomAttack()
    {
        player.GetComponent<PlayerController>().MaintainConflictWithNPC();

        randomAttackAnimIdx = UnityEngine.Random.Range(0, 4);

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
        bool isPlayerAttacking = player.GetComponent<PlayerController>().IsAttacking();

        if (isPlayerAttacking
            && player.GetComponent<PlayerController>().IsInConflict()
            && playerCreepsIntroInteraction.isInConflict
            && IsEnoughDistanceForCombat()
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
            randomSufferAnimIdx = UnityEngine.Random.Range(0, 3);

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
        if (playerCreepsIntroInteraction.AreAllCreepsDead())
        {
            playerCreepsIntroInteraction.isInConflict = false;
            player.GetComponent<PlayerController>().EndConflictWithNPC();
        }

        animator.SetTrigger(nameof(MonsterCombatActions.Die));

        animator.SetBool("IsAlive", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log("CREEP TOOK DAMAGE: " + damage + " | CREEP HEALTH: " + health);
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
            playerCreepsIntroInteraction.isReadyForInteraction = true;
            ShowExcitement();
        }

        playerCreepsIntroInteraction.PrepareForPlayerDecision();
    }

    IEnumerator SoundTask(float randomSoundLength)
    {
        float randSoundLength = UnityEngine.Random.Range(minSoundLength, maxSoundLength);

        if (IsAcceptableDistanceForSound() && !ShouldBeDead())
        {
            GetComponent<AudioSource>().Stop();

            int randomSound = UnityEngine.Random.Range(1, 8);  //number of sounds available

            AudioClip clip = Resources.Load<AudioClip>("Sounds/Mobs/MonsterCreeps/Creep" + randomSound);

            GetComponent<AudioSource>().PlayOneShot(clip);
        }

        yield return new WaitForSeconds(randSoundLength);

        StartCoroutine(SoundTask(randomSoundLength));
    }
}
