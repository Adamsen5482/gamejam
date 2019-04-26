using System.Collections;
using UnityEngine.UI;

public class ShowPlayerNameAndRoleTurn : PlayerTurn
{
    public Text PlayerNameAndRoleText;
    public Button EndTurnButton;

    private bool waitForEndTurn;

    private void Start()
    {
        this.EndTurnButton.onClick.AddListener(() => this.waitForEndTurn = false);
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        this.waitForEndTurn = true;
        this.PlayerNameAndRoleText.text = $"{player.Name}\n{player.Role}";

        while (this.waitForEndTurn)
        {
            yield return null;
        }
    }
}
