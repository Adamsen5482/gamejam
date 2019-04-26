using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;

    [Required]
    public HideGamePanel HidePanel;

    [Required]
    public PlayerTurn PlayerTurnSomething;

    [HideInInspector]
    public int Rounds;

    private Queue<PlayerInfo> turnQueue;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        this.turnQueue = new Queue<PlayerInfo>(PlayerList.AllPlayers);

        this.StartCoroutine(this.MainGameLoop());
    }

    private IEnumerator MainGameLoop()
    {
        yield return null;
        bool gameHasEnded = false;

        while (gameHasEnded == false)
        {
            var nextPlayer = this.NextPlayer();

            Debug.Log("Next player: " + nextPlayer.Name);

            // Show hide panel
            yield return this.StartCoroutine(this.HidePanel.ShowHidePanel(nextPlayer));

            while (this.HidePanel.IsVisible)
            {
                yield return null;
            }

            this.PlayerTurnSomething.gameObject.SetActive(true);

            // Show new player stuff
            yield return this.StartCoroutine(this.PlayerTurnSomething.RunTurn(nextPlayer));

            this.PlayerTurnSomething.gameObject.SetActive(false);


            //yield return waitForEndOfTurn;
        }
    }

    public PlayerInfo NextPlayer()
    {
        var nextPlayer = this.turnQueue.Dequeue();
        this.turnQueue.Enqueue(nextPlayer);

        if (nextPlayer == PlayerList.AllPlayers[0])
        {
            this.Rounds++;
        }

        return nextPlayer;
    }
}