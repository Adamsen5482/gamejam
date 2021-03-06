﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance = null;

    [Required]
    public HideGamePanel HidePanel;

    [Required]
    public PlayerTurn ShowPlayerRoleTurn;

    [Required]
    public TimeLoop TimeLoop;

    [Required]
    public PlayerTurn PublicClueRevealTurn;

    [Required]
    public PlayerTurn InvestigateLocationTurn;

    [Required]
    public PlayerTurn DiscussionTurn;

    [Required]
    public VotingTurn VotingTurn;

    [Required]
    public MurderWeaponVotingTurn MurderWeaponVotingTurn;

    [Required]
    public EndGame GameoverPanel;

    [Required]
    public TransistionController Transistion;

    private Queue<PlayerInfo> turnQueue = new Queue<PlayerInfo>();

    void Awake()
    {
        Instance = this;
        this.StartCoroutine(this.MainGameLoop());
    }

    private IEnumerator MainGameLoop()
    {
        yield return null;

        this.turnQueue = PlayerList.AllPlayers.ToQueue();

        Debug.Log(">Show players their assigned role round");
        while (this.turnQueue.Count > 0)
        {
            var nextPlayer = this.NextPlayer();

            // Show hide panel
            yield return this.StartCoroutine(this.HidePanel.ShowHidePanel(nextPlayer.Name, "FOR YOUR EYES ONLY\nYOU'RE ABOUT TO RECEIVE\nYOUR ROLE IN THIS THRILLER\nIT IS A SECRET FOR YOU ALONE"));
            yield return this.Transistion.ShowTransistion();
            this.HidePanel.gameObject.SetActive(false);

            this.ShowPlayerRoleTurn.gameObject.SetActive(true);
            yield return this.StartCoroutine(this.ShowPlayerRoleTurn.RunTurn(nextPlayer));
            yield return this.Transistion.ShowTransistion();
            this.ShowPlayerRoleTurn.gameObject.SetActive(false);
        }

        // Public clue reveal.
        Debug.Log(">First clue reveal");
        yield return this.StartCoroutine(this.HidePanel.ShowHidePanel("EVERYONE"));
        yield return this.Transistion.ShowTransistion();
        this.HidePanel.gameObject.SetActive(false);

        // IMMERSION
        this.TimeLoop.gameObject.SetActive(true);
        yield return this.TimeLoop.ScrollingText();
        yield return this.Transistion.ShowTransistion();
        this.TimeLoop.gameObject.SetActive(false);

        this.PublicClueRevealTurn.gameObject.SetActive(true);
        yield return this.StartCoroutine(this.PublicClueRevealTurn.RunTurn(null));
        yield return this.Transistion.ShowTransistion();
        this.PublicClueRevealTurn.gameObject.SetActive(false);

        // Visit locations
        Debug.Log(">Visit and investigate locations round.");
        this.turnQueue = PlayerList.AllPlayers.Shuffle().ToQueue();
        while (this.turnQueue.Count > 0)
        {
            var nextPlayer = this.NextPlayer();
            yield return this.StartCoroutine(this.HidePanel.ShowHidePanel(nextPlayer.Name, "THE TRUTH IS FOR YOUR EYES ONLY\nYOU MAY TELL THEM LIES\nYOU MAY TELL THEM TRUTH\nTHE CHOICE IS YOURS"));
            yield return this.Transistion.ShowTransistion();
            this.HidePanel.gameObject.SetActive(false);

            this.InvestigateLocationTurn.gameObject.SetActive(true);
            yield return this.StartCoroutine(this.InvestigateLocationTurn.RunTurn(nextPlayer));
            yield return this.Transistion.ShowTransistion();
            this.InvestigateLocationTurn.gameObject.SetActive(false);

            //yield return this.HidePanel.ShowHidePanel("EVERYONE");
            this.DiscussionTurn.gameObject.SetActive(true);
            yield return this.StartCoroutine(this.DiscussionTurn.RunTurn(null));
            yield return this.Transistion.ShowTransistion();
            this.DiscussionTurn.gameObject.SetActive(false);
        }

        // Voting turn!
        Debug.Log(">Voting turn.");
        this.turnQueue = PlayerList.AllPlayers
            .Where(x => x.Role != PlayerRole.Ghost)
            .Shuffle()
            .Append(PlayerList.Ghost)
            .ToQueue();
        while (this.turnQueue.Count > 0)
        {
            var nextPlayer = this.NextPlayer();
            yield return this.StartCoroutine(this.HidePanel.ShowHidePanel(nextPlayer.Name, "YOU ARE ABOUT TO VOTE\nON WHO YOU THINK DID IT\nTHE GHOST MUST TAKE IT\nINTO CONSIDERATION\nPICK CAREFULLY"));
            yield return this.Transistion.ShowTransistion();
            this.HidePanel.gameObject.SetActive(false);

            this.VotingTurn.gameObject.SetActive(true);
            yield return this.StartCoroutine(this.VotingTurn.RunTurn(nextPlayer));
            yield return this.Transistion.ShowTransistion();
            this.VotingTurn.gameObject.SetActive(false);
        }

        // Ghost voting for murder weapon!
        Debug.Log(">Ghost vote for murder weapon.");
        this.MurderWeaponVotingTurn.gameObject.SetActive(true);
        yield return this.StartCoroutine(this.MurderWeaponVotingTurn.RunTurn(PlayerList.Ghost));
        yield return this.Transistion.ShowTransistion();
        this.MurderWeaponVotingTurn.gameObject.SetActive(false);

        Debug.Log(">End of game.");
        Audiomanager.instance.playFinalVote();
        yield return this.StartCoroutine(this.HidePanel.ShowHidePanel("EVERYONE", "THE TRUTH IS ABOUT TO BE REVEALED..."));
        yield return this.Transistion.ShowTransistion();
        this.HidePanel.gameObject.SetActive(false);

        this.GameoverPanel.gameObject.SetActive(true);
        this.GameoverPanel.BuildEndGame(this.VotingTurn.GhostVotedPlayer, this.MurderWeaponVotingTurn.VotedWeapon);
    }

    public PlayerInfo NextPlayer()
    {
        var nextPlayer = this.turnQueue.Dequeue();
        Debug.Log("Next player: " + nextPlayer.Name);
        return nextPlayer;
    }
}


public static class CollectionExtenions
{
    public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
    {
        var queue = new Queue<T>();
        foreach (var item in collection)
        {
            queue.Enqueue(item);
        }

        return queue;
    }

    public static T PickRandom<T>(this IEnumerable<T> collection)
    {
        var list = collection.ToList();
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
    {
        var list = collection.ToList();

        for (int i = list.Count - 1; i > 0; i--)
        {
            var r = UnityEngine.Random.Range(0, i);
            var a = list[i];
            list[i] = list[r];
            list[r] = a;
        }

        for (int i = 0; i < list.Count; i++)
        {
            yield return list[i];
        }
    }
}