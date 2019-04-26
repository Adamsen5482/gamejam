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
        string ghostPlayerText = $"The ghost is {PlayerList.Ghost.Name}.";

        switch (player.Role)
        {
            case PlayerRole.Murderer:
                {
                    string accomplicesText;
                    int count = PlayerList.Accomplices.Count;
                    if (count == 0)
                    {
                        accomplicesText = "You have no accomplices.";
                    }
                    else if (count == 1)
                    {
                        accomplicesText = $"Your accomplice is {PlayerList.Accomplices[0].Name}.";
                    }
                    else
                    {
                        accomplicesText = $"Your accomplices are {string.Join(", ", PlayerList.Accomplices.Take(count - 1).Select(x => x.Name))} and {PlayerList.Accomplices[count - 1].Name}.";
                    }

                    return $"You are the murderer!\n{accomplicesText}\n{ghostPlayerText}";
                }

            case PlayerRole.Ghost:
                return "You are the ghost! You are dead :C";

            case PlayerRole.Accomplice:
                {
                    string accomplicesText;
                    var other = PlayerList.Accomplices.Where(x => x != player).ToList();
                    int count = other.Count;
                    if (count == 0)
                    {
                        accomplicesText = "You have no other accomplices.";
                    }
                    else if (count == 1)
                    {
                        accomplicesText = $"The other accomplice is {other[0].Name}.";
                    }
                    else
                    {
                        accomplicesText = $"The other accomplices are {string.Join(", ", other.Take(count - 1).Select(x => x.Name))} and {other[count - 1].Name}.";
                    }

                    return $"You are an accomplice!\nThe murderer is {PlayerList.Murderer.Name}.\n{accomplicesText}\n{ghostPlayerText}";
                }

            case PlayerRole.Detective:
                {
                    string accomplicesText;
                    if (PlayerList.Accomplices.Count == 0)
                    {
                        accomplicesText = "!";
                    }
                    else if (PlayerList.Accomplices.Count == 1)
                    {
                        accomplicesText = $" and his 1 accomplice!";
                    }
                    else
                    {
                        accomplicesText = $" and his {PlayerList.Accomplices.Count} accomplices!";
                    }

                    return $"You are a detective!\nFind out who the murderer is{accomplicesText}\n{ghostPlayerText}";
                }

            case PlayerRole.Error:
                return $"What are you doing here? Why are you breaking my game! :C\nI am not even going to tell you who the ghost is!";

            default:
                throw new InvalidOperationException("Unknown role: " + player.Role);
        }

    }
}