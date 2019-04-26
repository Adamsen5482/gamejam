using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ShowPlayerRoleTurn : PlayerTurn
{
    [Required] public Text PlayerNameText;
    [Required] public Text PlayerRoleText;
    [Required] public Text FlavorText;
    [Required] public Button EndTurnButton;

    private bool waitForEndOfTurn;

    private void Start()
    {
        this.EndTurnButton.onClick.AddListener(() => this.waitForEndOfTurn = false);
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        this.waitForEndOfTurn = true;

        this.PlayerNameText.text = player.Name;
        this.PlayerRoleText.text = player.Role.ToString();
        this.FlavorText.text = this.FillFlavorText(player);

        while (this.waitForEndOfTurn)
        {
            yield return null;
        }
    }

    private string FillFlavorText(PlayerInfo player)
    {
        switch (player.Role)
        {
            case PlayerRole.Murderer:
                return $"You are the murderer!\nYour accomplices are {string.Join(" ", PlayerList.Accomplices.Select(p => p.Name))}.";
            case PlayerRole.Ghost:
                return "You are the ghost! You are dead :C";
            case PlayerRole.Accomplice:
                return $"You are an accomplice!\nThe murderer is {PlayerList.Murderer.Name}.\nThe other accomplices are {string.Join(" ", PlayerList.Accomplices.Select(p => p.Name))}";
            case PlayerRole.Detective:
                return $"You are a detective!\nFind out who the murderer is! (And their accomplices.)";
            case PlayerRole.Error:
                return $"What are you doing here? Why are you breaking my game! :C";
            default:
                throw new InvalidOperationException("Unknown role: " + player.Role);
        }

    }
}