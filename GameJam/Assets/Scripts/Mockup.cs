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
            }
            .Select(x => new PlayerInfo() { Name = x })
            .ToList() );

        //GameSetup.AssignRoles(Enumerable.Range(0, 5).Select(x => new PlayerInfo { Name = Guid.NewGuid().ToString() }).ToList());
    }
#endif
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
        PlayerList.AllPlayers.Clear();
        PlayerList.AllPlayers.AddRange(players);
        PlayerList.Murderer = null;
        PlayerList.Ghost = null;
        PlayerList.Accomplices.Clear();
        PlayerList.Detectives.Clear();

        var temp = players.ToList();

        int accomplices = 0;

        if (temp.Count >= 5)
        {
            accomplices = 0;
        }
        else if (temp.Count >= 7)
        {
            accomplices = 1;
        }
        else if (temp.Count >= 9)
        {
            accomplices = 2;
        }
        else
        {
            throw new InvalidOperationException("We do not support that number of players :C");
        }

        var murder = PickAndRemove(temp);
        murder.Role = PlayerRole.Murderer;
        PlayerList.Murderer = murder;

        var ghost = PickAndRemove(temp);
        ghost.Role = PlayerRole.Ghost;
        PlayerList.Ghost = ghost;

        for (int i = 0; i < accomplices; i++)
        {
            var a = PickAndRemove(temp);
            a.Role = PlayerRole.Accomplice;
            PlayerList.Accomplices.Add(a);
        }

        while (temp.Count > 0)
        {
            var d = PickAndRemove(temp);
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