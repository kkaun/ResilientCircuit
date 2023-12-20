using System.Collections;
using UnityEngine;

public class MoskitoController : MonoBehaviour
{
    public GameObject player;

    public int creepIndex;

    //By adding new levels, may add more interaction scripts as more separated modules
    //and switch them by checking current level
    private PlayerMoskitosIntroInteraction playerMoskitosIntroInteraction;

    private Animator animator;

    private float health;

    public const float damage = 2f;
    private const float moveSpeed = 1.5f;

    private float distanceToPlayer;
    public const float maxFightDistanceToPlayer = 1.0f;
    public const float maxDistanceForSoundToPlayer = 4.0f;

    private int randomAttackAnimIdx = 0;
    private int randomSufferAnimIdx = 0;

    private const float minSoundLength = 2f;
    private const float maxSoundLength = 3f;


    void Start()
    {
        playerMoskitosIntroInteraction = GameObject.Find("MoskitosIntroInteraction")
            .GetComponent<PlayerMoskitosIntroInteraction>();

        animator = GetComponent<Animator>();

        health = 18f;

        StartCoroutine(SoundTask(3f));
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().IsInConflict()
            && playerMoskitosIntroInteraction.isInConflict  
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
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void PerformRandomAttack()
    {
        player.GetComponent<PlayerController>().MaintainConflictWithNPC();

        randomAttackAnimIdx = Random.Range(0, 3);

        switch (randomAttackAnimIdx)
        {
            case 0:
                animator.SetTrigger(nameof(MoskitoCombatActions.Attack_01));
                break;
            case 1:
                animator.SetTrigger(nameof(MoskitoCombatActions.Attack_02));
                break;
            case 2:
                animator.SetTrigger(nameof(MoskitoCombatActions.Attack_03));
                break;
            default:
                animator.SetTrigger(nameof(MoskitoCombatActions.Attack_04));
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
            && playerMoskitosIntroInteraction.isInConflict
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
            randomSufferAnimIdx = Random.Range(0, 2);

            switch (randomSufferAnimIdx)
            {
                case 0:
                    animator.SetTrigger(nameof(MoskitoCombatActions.Damage_01));
                    break;
                default:
                    animator.SetTrigger(nameof(MoskitoCombatActions.Damage_02));
                    break;
            }
        }
    }

    private void Die()
    {
        if (playerMoskitosIntroInteraction.AreAllMobsDead())
        {
            playerMoskitosIntroInteraction.isInConflict = false;
            player.GetComponent<PlayerController>().EndConflictWithNPC();
        }

        animator.SetTrigger(nameof(MoskitoCombatActions.Dead_01));

        animator.SetBool("IsAlive", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log("MOSKITO (ID " + creepIndex + ") TOOK DAMAGE: " + damage + " | MOSKITO HEALTH: " + health);
    }

    public bool ShouldBeDead()
    {
        return health < 0;
    }

    IEnumerator SoundTask(float randomSoundLength)
    {
        float randSoundLength = Random.Range(minSoundLength, maxSoundLength);

        if (IsAcceptableDistanceForSound() && !ShouldBeDead())
        {
            GetComponent<AudioSource>().Stop();

            int randomSound = Random.Range(1, 7); //number of sounds available

            AudioClip clip = Resources.Load<AudioClip>("Sounds/Mobs/Insects/Insect" + randomSound);

            GetComponent<AudioSource>().PlayOneShot(clip);
        }

        yield return new WaitForSeconds(randSoundLength);

        StartCoroutine(SoundTask(randomSoundLength));
    }
}
