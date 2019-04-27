using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerRoleTurn : PlayerTurn
{
    [Required] public RoleIcons Icons;

    [Required] public Image RoleIconImage;
    [Required] public Text PlayerRoleText;
    [Required] public Text FlavorText;
    [Required] public SmartButton EndTurnButton;

    private bool waitForEndOfTurn;

    private void Start()
    {
        this.EndTurnButton.ClickAction.AddListener(() => this.waitForEndOfTurn = false);
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        this.waitForEndOfTurn = true;

        this.PlayerRoleText.text = player.Role.ToString().ToUpper();
        this.RoleIconImage.sprite = this.Icons.GetRoleIcon(player.Role);
        this.FlavorText.text = this.FillFlavorText(player);

        while (this.waitForEndOfTurn)
        {
            yield return null;
        }
    }

    private string FillFlavorText(PlayerInfo player)
    {
        string ghostPlayerText = $"THE GHOST IS {PlayerList.Ghost.Name.FormatPlayerName()}.";

        switch (player.Role)
        {
            case PlayerRole.Murderer:
                {
                    string accomplicesText;
                    int count = PlayerList.Accomplices.Count;
                    if (count == 0)
                    {
                        accomplicesText = "YOU HAVE NO FRIENDS.";
                    }
                    else if (count == 1)
                    {
                        accomplicesText = $"YOUR FRIEND IS {PlayerList.Accomplices[0].Name.FormatPlayerName()}.";
                    }
                    else
                    {
                        accomplicesText = $"YOUR ACCOMPLICES ARE {string.Join(", ", PlayerList.Accomplices.Take(count - 1).Select(x => x.Name.FormatPlayerName()))} and {PlayerList.Accomplices[count - 1].Name.FormatPlayerName()}.";
                    }

                    return $"YOU ARE THE MURDERER!\n{accomplicesText}\n{ghostPlayerText}";
                }

            case PlayerRole.Ghost:
                return "YOU ARE THE GHOST!";

            case PlayerRole.Accomplice:
                {
                    string accomplicesText;
                    var other = PlayerList.Accomplices.Where(x => x != player).ToList();
                    int count = other.Count;
                    if (count == 0)
                    {
                        accomplicesText = "YOU HAVE NO FRIENDS.";
                    }
                    else if (count == 1)
                    {
                        accomplicesText = $"YOUR FRIEND IS {other[0].Name.FormatPlayerName()}.";
                    }
                    else
                    {
                        accomplicesText = $"YOUR FRIENDS ARE {string.Join(", ", other.Take(count - 1).Select(x => x.Name.FormatPlayerName()))} AND {other[count - 1].Name.FormatPlayerName()}.";
                    }

                    return $"YOU ARE AN ACCOMPLICE!\nTHE MURDERER IS {PlayerList.Murderer.Name.FormatPlayerName()}.\n{accomplicesText}\n{ghostPlayerText}";
                }

            case PlayerRole.Detective:
                {
                    string accomplicesText;
                    if (PlayerList.Accomplices.Count == 0)
                    {
                        accomplicesText = "";
                    }
                    else if (PlayerList.Accomplices.Count == 1)
                    {
                        accomplicesText = $" AND THEIR 1 ACCOMPLICE";
                    }
                    else
                    {
                        accomplicesText = $" AND THEIR {PlayerList.Accomplices.Count} ACCOMPLICES";
                    }

                    return $"YOU ARE THE DETECTIVE!\nFIND OUT WHO THE MURDERER{accomplicesText} IS!\n{ghostPlayerText}";
                }

            case PlayerRole.Error:
                return $"What are you doing here? Why are you breaking my game! :C\nI am not even going to tell you who the ghost is!";

            default:
                throw new InvalidOperationException("Unknown role: " + player.Role);
        }

    }
}

public static class StringUtils
{
    public static string FormatPlayerName(this string name)
    {
        return $"<color=#FF585D>{name.ToUpper()}</color>";
    }
}