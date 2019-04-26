using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Role
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
    public Color Color;
    public Role Role;
}

public static class PlayerList
{
    public static List<PlayerInfo> Players = new List<PlayerInfo>();
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
    public static void SetupNewGame(List<PlayerInfo> players)
    {
        PlayerList.Players.Clear();

        // Assign roles
    }


    private static PlayerInfo PickRandom(List<PlayerInfo> players)
    {
        return players[UnityEngine.Random.Range(0, players.Count)];
    }
}