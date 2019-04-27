using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine.UI;

public class PublicClueTurn : PlayerTurn
{
    [Required]
    public Text HintText;

    [Required]
    public SmartButton EndTurnButton;

    public PublicHints Hints;

    private bool waitForEndOfTurn;

    private void Start()
    {
        this.EndTurnButton.ClickAction.AddListener(() => this.waitForEndOfTurn = false);
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        this.HintText.text = this.Hints.GetHint();

        this.waitForEndOfTurn = true;
        while (this.waitForEndOfTurn)
        {
            yield return null;
        }
    }
}
