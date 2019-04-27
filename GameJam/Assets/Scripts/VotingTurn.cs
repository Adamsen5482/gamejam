using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class VotingTurn : PlayerTurn
{
    public Text GhostText;

    public List<VoteButton> Buttons;

    private string votedForPlayer;

    private Dictionary<PlayerInfo, int> votes;

    private void Start()
    {
        for (int i = 0; i < this.Buttons.Count; i++)
        {
            this.Buttons[i].OnSelect.AddListener(this.OnSelection);
            this.Buttons[i].OnConfirmVote.AddListener(x => this.votedForPlayer = x);
        }

        // Players cannot vote for themselves or the ghost, so only enable voting buttons for number of players minus 2.
        for (int i = PlayerList.AllPlayers.Count - 2; i < this.Buttons.Count; i++)
        {
            this.Buttons[i].gameObject.SetActive(false);
        }

        this.votes = new Dictionary<PlayerInfo, int>();
        foreach (var p in PlayerList.AllPlayers)
        {
            this.votes[p] = 0;
        }
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        var others = PlayerList.AllPlayers
            .Where(x => x.Role != PlayerRole.Ghost)
            .Where(x => x != player)
            .ToList();
        int i = 0;
        for (; i < others.Count; i++)
        {
            this.Buttons[i].SetItem(others[i].Name);
        }

        if (player.Role == PlayerRole.Ghost)
        {
            this.GhostText.gameObject.SetActive(true);
            this.GhostText.text = $"AS THE GHOST YOU GET THE FINAL SAY. YOUR FRIENDS THINKS IT'S {this.GetHightestVoted().Name.FormatName()}.";
        }
        else
        {
            this.GhostText.gameObject.SetActive(false);
        }

        this.votedForPlayer = null;
        while (this.votedForPlayer == null)
        {
            yield return null;
        }

        // Add vote.
        this.votes[PlayerList.AllPlayers.First(x => x.Name == this.votedForPlayer)]++;
    }

    public PlayerInfo GetHightestVoted()
    {
        PlayerInfo highest = null;
        int value = int.MinValue;
        foreach (var x in this.votes)
        {
            if (x.Value > value)
            {
                highest = x.Key;
                value = x.Value;
            }
        }

        return highest;
    }

    private void OnSelection()
    {
        foreach (var b in this.Buttons)
        {
            b.ResetSelection();
        }
    }
}