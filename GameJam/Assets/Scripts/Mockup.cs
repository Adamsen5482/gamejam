using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerRole
{
    Error = 0,
    Murderer,
    Ghost,
    Accomplice,
    Detective,
}

public class PlayerInfo
{
    public string Name;
    public PlayerRole Role;
}

public static class PlayerList
{
    public static List<PlayerInfo> AllPlayers = new List<PlayerInfo>();

    public static PlayerInfo Murderer;

    public static PlayerInfo Ghost;

    public static List<PlayerInfo> Accomplices = new List<PlayerInfo>();

    public static List<PlayerInfo> Detectives = new List<PlayerInfo>();

    public static Weapons MurderWeapon;

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
    private static void AddRandomPlayersInEditor()
    {
        GameSetup.AssignRoles(new string[]
            {
                "Mommy",
                "Daddy",
                "Sony",
                "Xbox",
                "Switch",
                //"Hello",
                //"World",
                //"Damm",
            }
            .Select(x => new PlayerInfo() { Name = x.ToUpper() })
            .ToList() );
    }
#endif
}

public static class GameSetup
{
    public static void AssignRoles(List<PlayerInfo> players)
    {
        PlayerList.AllPlayers.Clear();
        PlayerList.AllPlayers.AddRange(players);
        PlayerList.Murderer = null;
        PlayerList.Ghost = null;
        PlayerList.Accomplices.Clear();
        PlayerList.Detectives.Clear();

        var queue = players.Shuffle().ToQueue();

        int accomplices = 0;

        if (players.Count <= 5)
        {
            accomplices = 0;
        }
        else if (players.Count <= 7)
        {
            accomplices = 1;
        }
        else if (players.Count <= 9)
        {
            accomplices = 2;
        }
        else
        {
            throw new InvalidOperationException("We do not support that number of players :C");
        }

        var murder = queue.Dequeue();
        murder.Role = PlayerRole.Murderer;
        PlayerList.Murderer = murder;

        var ghost = queue.Dequeue();
        ghost.Role = PlayerRole.Ghost;
        PlayerList.Ghost = ghost;

        for (int i = 0; i < accomplices; i++)
        {
            var a = queue.Dequeue();
            a.Role = PlayerRole.Accomplice;
            PlayerList.Accomplices.Add(a);
        }

        while (queue.Count > 0)
        {
            var d = queue.Dequeue();
            d.Role = PlayerRole.Detective;
            PlayerList.Detectives.Add(d);
        }

        PlayerList.MurderWeapon = (Weapons)UnityEngine.Random.Range(1, 6);
        
        Debug.Log($@"Roles assigned with {PlayerList.AllPlayers.Count} players
Murderer: {PlayerList.Murderer.Name}
Ghost: {PlayerList.Ghost.Name}
Murder weapon: {PlayerList.MurderWeapon}
Detectives: {PlayerList.Detectives.Count}
{string.Join("\n", PlayerList.Detectives.Select(x => " - " + x.Name))}
Accomplices: {PlayerList.Accomplices.Count}
{string.Join("\n", PlayerList.Accomplices.Select(x => " - " + x.Name))}");
    }

    private static PlayerInfo PickAndRemove(List<PlayerInfo> players)
    {
        int index = UnityEngine.Random.Range(0, players.Count);
        var p = players[index];
        players.RemoveAt(index);
        return p;
    }
}