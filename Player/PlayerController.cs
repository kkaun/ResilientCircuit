using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private SceneManager sceneManager;

	private Animator animator;

	private FollowPlayer followPlayer;

	private const float initialHelth = 200f;
	private const float maxHealth = 300f;
	private float health;
	public const float damage = 2f;

	private bool isInConflict;
	private bool isInStaticDialogue;
	private bool isAllowedToTalk;

	private const float minSoundLength = 0.8f;
	private const float maxSoundLength = 1.6f;

	void Start()
	{
		animator = GetComponent<Animator>();

		followPlayer = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();

		OnGameStart();

		StartCoroutine(TalkTask(1.0f));
	}

	void Update()
	{
		if (isInConflict)
		{
			if (Input.GetKey(KeyCode.R))
			{
				PerformHighAttack();
			}
			if (Input.GetKey(KeyCode.E))
			{
				PerformLowAttack();
			}
			sceneManager.UpdateHeathIndicator(health);
		}
	}

	private void OnGameStart()
    {
		isInConflict = false;
		isInStaticDialogue = false;
		isAllowedToTalk = false;

		health = initialHelth;
		sceneManager.UpdateHeathIndicator(health);

		StartMalfunctioningState();
	}

	private void PerformLowAttack()
	{
		int randomAttackAnimIdx = Random.Range(0, 2);

		if (randomAttackAnimIdx == 1) {
			animator.SetTrigger(nameof(PlayerCombatActions.StrikeLow2));
		}
		else {
			animator.SetTrigger(nameof(PlayerCombatActions.StrikeLow1));
		}
	}

	private void PerformHighAttack()
	{
		animator.SetTrigger(nameof(PlayerCombatActions.StrikeHigh));
	}	

	private void OnTriggerEnter(Collider other)
	{
		CheckCameraSwitch(other);
		CheckForDamageTaken(other);
	}

	private void CheckForDamageTaken(Collider other)
	{
		if (isInConflict && !ShouldBeDead()) {

			switch(other.gameObject.tag)
            {
				case "MonsterWeapon":
					ProcessDamage(MonsterController.damage);
					break;
				case "CreepWeapon":
					ProcessDamage(CreepController.damage);
					break;
				case "MobWeapon":
					ProcessDamage(MoskitoController.damage);
					break;
				default:
					break;
			}
		}
	}

	private void ProcessDamage(float damage)
    {
		PlayCombatSmashSound();

		TakeDamage(damage);

		if (ShouldBeDead())
		{
			Die();
		}
		else
		{
			int randomSufferAnimIdx = Random.Range(0, 2);

			if (randomSufferAnimIdx == 0)
            {
				animator.SetTrigger(nameof(PlayerCombatActions.TakeDamage1));
			} else
            {
				animator.SetTrigger(nameof(PlayerCombatActions.TakeDamage2));
			}
		}
	}

	private void Die()
	{
		animator.SetTrigger(nameof(PlayerCombatActions.Death));

		isInConflict = false;

		//TODO game over within SceneController
	}

	private void CheckCameraSwitch(Collider other)
    {
		if (other.CompareTag("SwitchCameraTrigger"))
		{
			SwitchCameraCollider switchCamera =
				other.gameObject.GetComponent<SwitchCameraCollider>();

			CameraDirection updatedDirection = switchCamera.triggersCameraDirection;

			followPlayer.SetCameraDirection(updatedDirection);
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
	}

	public bool ShouldBeDead()
	{
		return health < 0;
	}

	public void FulfillHealthToMax()
    {
		health = maxHealth;
    }

	public void MaintainConflictWithNPC()
	{
		animator.SetBool(nameof(PlayerAnimStates.IsInConflict), true);
		isInConflict = true;
	}

	public void EndConflictWithNPC()
	{
		animator.SetBool(nameof(PlayerAnimStates.IsInConflict), false);
		isInConflict = false;
	}

	public bool IsInConflict()
	{
		return isInConflict;
	}

	public bool IsInStaticDialogue()
    {
		return isInStaticDialogue;
    }

	public void StartStaticDialogue()
    {
		animator.SetBool(nameof(PlayerAnimStates.IsInStaticDialog), true);
		isInStaticDialogue = true;
		AllowTalkingAudio();
    }

	public void EndStaticDialogue()
    {
		animator.SetBool(nameof(PlayerAnimStates.IsInStaticDialog), false);
		isInStaticDialogue = false;
		RestrictTalkingAudio();
    }

	public void StartMalfunctioningState()
    {
		animator.SetBool(nameof(PlayerAnimStates.IsMalfunctioning), true);
		animator.SetTrigger(nameof(PlayerCombatActions.Malfunction));
	}

	public void EndMalfunctioningState()
	{
		animator.SetBool(nameof(PlayerAnimStates.IsMalfunctioning), false);
		animator.SetTrigger(nameof(PlayerCombatActions.Idle));
	}

	public void AllowTalkingAudio()
    {
		isAllowedToTalk = true;
    }

	public void RestrictTalkingAudio()
    {
		isAllowedToTalk = false;
    }

	public void PlayCombatSmashSound()
    {
		if (!GetComponent<AudioSource>().isPlaying)
        {
			int randomSmashSound = Random.Range(1, 12); //number of sounds available

			AudioClip clip = Resources.Load<AudioClip>("Sounds/Contacts/Smash" + randomSmashSound);

			GetComponent<AudioSource>().PlayOneShot(clip);
		}
	}

	IEnumerator TalkTask(float randomSoundLength)
	{
		float randSoundLength = Random.Range(minSoundLength, maxSoundLength);

		if (isInStaticDialogue && isAllowedToTalk)
		{
			GetComponent<AudioSource>().Stop();

			int randomDialogSound = Random.Range(1, 10); //number of sounds available

			AudioClip clip = Resources.Load<AudioClip>("Sounds/Player/Conv" + randomDialogSound);

			GetComponent<AudioSource>().PlayOneShot(clip);
		}

		yield return new WaitForSeconds(randSoundLength);

		StartCoroutine(TalkTask(randomSoundLength));
	}
}
