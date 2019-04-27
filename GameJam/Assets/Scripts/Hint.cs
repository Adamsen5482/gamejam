using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu]
public class Hint : ScriptableObject
{
    [Required]
    public MurderWeaponsList Weapons;

    [SerializeField, TableList]
    private List<HintItem> hintitems;

    private Dictionary<Location, List<string>> hintmap;
    public string GetHint(PlayerInfo currentPlayer, Location location)
    {
        if (hintmap == null)
        {
            hintmap = new Dictionary<Location, List<string>>();
            for (int i = 0; i < hintitems.Count; i++)
            {
                hintmap.Add(hintitems[i].Location, hintitems[i].hints);
            }
        }
        var h= hintmap[location];

        var playerQueue = PlayerList.AllPlayers
            .Where(x => x.Role != PlayerRole.Ghost)
            .Where(x => x.Role != PlayerRole.Murderer)
            .Shuffle()
            .ToQueue();

        var weaponQueue = this.Weapons.Weapons
            .Where(x => x.Type != PlayerList.MurderWeapon)
            .Shuffle()
            .ToQueue();

        string text = h[UnityEngine.Random.Range(0, h.Count)]
            .Replace("<WEP>", weaponQueue.Dequeue().Name.FormatName())
            .Replace("<WEP2>", weaponQueue.Dequeue().Name.FormatName())
            .Replace("<WEP3>", weaponQueue.Dequeue().Name.FormatName())
            .Replace("<MURDERWEAPON>", this.Weapons.GetWeaponItem(PlayerList.MurderWeapon).Name.FormatName())
            .Replace("<GHOST>", PlayerList.Ghost.Name.FormatName())
            .Replace("<RPNoCrrNoMurNoGos1>", playerQueue.Dequeue().Name.FormatName())
            .Replace("<RPNoCrrNoMurNoGos2>", playerQueue.Dequeue().Name.FormatName())
            .Replace("<RPNoCrrNoMurNoGos3>", playerQueue.Dequeue().Name.FormatName())
            // Use once
            .Replace("<RPNoCrrNoMur>", PlayerList.AllPlayers.Where(x => x.Role != PlayerRole.Murderer || x != currentPlayer).PickRandom().Name.FormatName())
            .Replace("<RPNoCrrNoGos>", PlayerList.AllPlayers.Where(x => x.Role != PlayerRole.Ghost || x != currentPlayer).PickRandom().Name.FormatName())
            .Replace("<RPNoCrr>", PlayerList.AllPlayers.Where(x => x != currentPlayer).PickRandom().Name.FormatName())
            .Replace("<MUR>", PlayerList.Murderer.Name.FormatName())
            .Replace("<RP>", PlayerList.AllPlayers.PickRandom().Name.FormatName())
            .Replace("<LIE>", currentPlayer.Name.FormatName())
            ;
        return text;
    }
}


public enum Location
{
    Error = 0,
    Lounge,
    Cafeteria,
    ServerRoom,
    MainHall,
    Entrance,
    Workshop,
    StaffRoom,
    ITDepartment,
    Bathroom,
    Gardens,
}

[Serializable]
public class HintItem 
{
    [TableColumnWidth(80, Resizable = false)]
    public Location Location;
    
    [TextArea]
    public List<string> hints;
}