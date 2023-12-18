using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATMController : MonoBehaviour
{

    public GameObject player;

    //By adding new levels, may add more interaction scripts as more separated modules
    //and switch them by checking current level
    private PlayerATMsIntroInteraction playerATMsIntroInteraction;

    public int atmIndex;

    private float distanceToPlayer;
    public const float maxInteractionDistanceToPlayer = 1.8f;
    public const float maxDistanceForSoundToPlayer = 4.0f;

    void Start()
    {
        playerATMsIntroInteraction = GameObject.Find("ATMsIntroInteraction")
            .GetComponent<PlayerATMsIntroInteraction>();
    }

    void Update()
    {
        if (IsEnoughDistanceForInteraction())
        {
            playerATMsIntroInteraction.UpdateLevelATMState(atmIndex);
        }
    }

    public bool IsEnoughDistanceForInteraction()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        return (distanceToPlayer < maxInteractionDistanceToPlayer);
    }
}
