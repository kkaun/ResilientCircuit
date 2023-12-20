using UnityEngine;

public class LevelGateController : MonoBehaviour
{
    private IntroLevelObjectivesManager introLevelObjectivesManager;

    private float slideSpeed = 0.015f;
    private float counter = 0;
    private float slideDuration = 1.8f;
    private bool playedSound = false;

    private void Start()
    {
        introLevelObjectivesManager = GameObject.Find("ObjectivesManager")
                    .GetComponent<IntroLevelObjectivesManager>();
    }

    void Update()
    {
        if(counter < slideDuration && introLevelObjectivesManager.IsStage2TaskResolved())
        {
            OpenGateSlowly();
            counter += Time.deltaTime;

            if (!playedSound)
            {
                AudioClip clip = Resources.Load<AudioClip>("Sounds/Gate/GateOpeningSound");
                GetComponent<AudioSource>().PlayOneShot(clip);
                playedSound = true;
            }
        }
    }

    private void OpenGateSlowly()
    {
        transform.Translate(Vector3.right * slideSpeed);
    }
}
