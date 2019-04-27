using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;

    [Required]
    public HideGamePanel HidePanel;

    [Required]
    public PlayerTurn ShowPlayerRoleTurn;

    [Required]
    public PlayerTurn OtherStuffTurn;

    [HideInInspector]
    public int RoundNumber;

    private Queue<PlayerInfo> turnQueue = new Queue<PlayerInfo>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        this.StartCoroutine(this.MainGameLoop());
    }

    private IEnumerator MainGameLoop()
    {
        this.FillTurnQueue();
        bool gameHasEnded = false;
        this.RoundNumber = 1;

        yield return null;

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

            var turnRunner = this.SelectTurnRunner(nextPlayer);
            turnRunner.gameObject.SetActive(true);

            // Show new player stuff
            yield return this.StartCoroutine(turnRunner.RunTurn(nextPlayer));

            turnRunner.gameObject.SetActive(false);

            if (this.turnQueue.Count == 0)
            {
                this.RoundNumber++;
                this.FillTurnQueue();
            }
        }
    }

    public PlayerInfo NextPlayer()
    {
        var nextPlayer = this.turnQueue.Dequeue();
        return nextPlayer;
    }

    private void FillTurnQueue()
    {
        foreach (var p in PlayerList.AllPlayers.Shuffle())
        {
            this.turnQueue.Enqueue(p);
        }
    }

    private PlayerTurn SelectTurnRunner(PlayerInfo player)
    {
        if (this.RoundNumber == 1)
        {
            return this.ShowPlayerRoleTurn;
        }

        return this.OtherStuffTurn;
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