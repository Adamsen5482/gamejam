using System.Collections;

public class DiscussionTurn : PlayerTurn
{
    public SmartButton EndTurnButton;

    private bool waitForEndOfTurn;

    private void Start()
    {
        this.EndTurnButton.ClickAction.AddListener(() => waitForEndOfTurn = false);
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        this.waitForEndOfTurn = true;
        while (this.waitForEndOfTurn)
        {
            yield return null;
        }
    }
}