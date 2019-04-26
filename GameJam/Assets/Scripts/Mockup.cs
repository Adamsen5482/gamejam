using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerRole
{
    Error = 0,
    Murder,
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
    public static List<PlayerInfo> Players = new List<PlayerInfo>();

    public static PlayerInfo Murder;

    public static PlayerInfo Ghost;

    public static List<PlayerInfo> Accomplices;

    public static List<PlayerInfo> Detectives;
}

[Serializable]
public class Weapon
{
    public string Name;
    [PreviewField, PropertyOrder(-1)]
    public Sprite Icon;
}

public static class GameSetup
{
    public static void AssignRoles(List<PlayerInfo> players)
    {
        PlayerList.Players.Clear();
        PlayerList.Players.AddRange(players);
        PlayerList.Murder = null;
        PlayerList.Ghost = null;
        PlayerList.Accomplices.Clear();
        PlayerList.Detectives.Clear();

        int accomplices = 0;

        if (players.Count >= 5)
        {
            accomplices = 0;
        }
        else if (players.Count >= 7)
        {
            accomplices = 1;
        }
        else if (players.Count >= 9)
        {
            accomplices = 2;
        }
        else
        {
            throw new InvalidOperationException("We do not support that number of players :C");
        }

        var murder = PickAndRemove(players);
        murder.Role = PlayerRole.Murder;
        PlayerList.Murder = murder;

        var ghost = PickAndRemove(players);
        ghost.Role = PlayerRole.Ghost;
        PlayerList.Ghost = ghost;

        for (int i = 0; i < accomplices; i++)
        {
            var a = PickAndRemove(players);
            a.Role = PlayerRole.Accomplice;
            PlayerList.Accomplices.Add(a);
        }

        while (players.Count > 0)
        {
            var d = PickAndRemove(players);
            d.Role = PlayerRole.Detective;
            PlayerList.Detectives.Add(d);
        }
    }

    private static PlayerInfo PickAndRemove(List<PlayerInfo> players)
    {
        int index = UnityEngine.Random.Range(0, players.Count);
        var p = players[index];
        players.RemoveAt(index);
        return p;
    }
}