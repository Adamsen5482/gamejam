using System.Collections;
using UnityEngine.UI;

public class ShowPlayerNameAndRoleTurn : PlayerTurn
{
    public Text PlayerNameAndRoleText;
    public SmartButton EndTurnButton;

    public Hint Hints;

    private bool waitForEndTurn;

    private void Start()
    {
        this.EndTurnButton.ClickAction.AddListener(this.EndTurn);
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        this.waitForEndTurn = true;
        //this.PlayerNameAndRoleText.text = $"{player.Name}\n{player.Role}";
        this.PlayerNameAndRoleText.text = this.Hints.GetHint(player, Location.Lounge);

        while (this.waitForEndTurn)
        {
            yield return null;
        }
    }

    public void EndTurn()
    {
        this.waitForEndTurn = false;
    }
}
