using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI dialogTitle;

    public Button peacefulDecisionButton;
    public Button agressiveDecisionButton;

    public TextMeshProUGUI peacefulDecisionButtonText;
    public TextMeshProUGUI agressiveDecisionButtonText;

    public TextMeshProUGUI healthIndicator;
    public TextMeshProUGUI gameMechanicsHint;

    private static string playerHealthIndicationText = "Integrity: ";

    void Start()
    {
        dialogText.gameObject.SetActive(false);
        dialogTitle.gameObject.SetActive(false);

        peacefulDecisionButton.gameObject.SetActive(false);
        agressiveDecisionButton.gameObject.SetActive(false);

        gameMechanicsHint.gameObject.SetActive(false);
    }

    void Update()
    {
    }

    public void SetDialogTitle(string title)
    {
        dialogTitle.text = title;
    }

    public void SetDialogText(string text)
    {
        if (text == DialogueIntroTexts.playerDialogTitle
            || text == DialogueIntroTexts.monsterDialogTitle
            || text == DialogueIntroTexts.humanFemaleDialogTitle
            || text == DialogueIntroTexts.humanMaleDialogTitle) {
            dialogText.text = "";
        }
        else
        {
            dialogText.text = text;
        }
    }

    public void ActivateDecisionButtons()
    {
        peacefulDecisionButton.gameObject.SetActive(true);
        agressiveDecisionButton.gameObject.SetActive(true);
    }

    public void DeactivateDecisionButtons()
    {
        peacefulDecisionButton.gameObject.SetActive(false);
        agressiveDecisionButton.gameObject.SetActive(false);
    }

    public void ActivateDialogContent()
    {
        dialogText.gameObject.SetActive(true);
        dialogTitle.gameObject.SetActive(true);
    }

    public void DeactivateDialogContent()
    {
        dialogText.gameObject.SetActive(false);
        dialogTitle.gameObject.SetActive(false);
    }

    public void SetPeacefulDecisionText(string text)
    {
        peacefulDecisionButtonText.text = text;
    }

    public void SetAgressiveDecisionText(string text)
    {
        agressiveDecisionButtonText.text = text;
    }

    public void SetPeacefulDecisionClickListener(UnityAction call)
    {
        peacefulDecisionButton.GetComponent<Button>().onClick.AddListener(call);
    }

    public void SetAgressiveDecisionClickListener(UnityAction call)
    {
        agressiveDecisionButton.GetComponent<Button>().onClick.AddListener(call);
    }

    public void ClearDialogContent()
    {
        dialogTitle.text = "";
        dialogText.text = "";
    }

    public void DeactivateAllInterationContent()
    {
        DeactivateDecisionButtons();
        DeactivateDialogContent();
    }

    public void UpdateHeathIndicator(float health)
    {
        healthIndicator.text = playerHealthIndicationText + health.ToString();
    }

    public void SetMechanicsHintText(string hint)
    {
        gameMechanicsHint.text = hint;
    }

    public void ActivateMechanicsHint()
    {
        gameMechanicsHint.gameObject.SetActive(true);
    }

    public void DeactivateMechanicsHint()
    {
        gameMechanicsHint.gameObject.SetActive(false);
    }
}
