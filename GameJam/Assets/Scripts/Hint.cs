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

        return h[UnityEngine.Random.Range(0, h.Count)]
            .Replace("<WEP>", this.Weapons.Weapons.Where(x => x.Type != PlayerList.MurderWeapon).PickRandom().Name.FormatName())
            .Replace("<MURDERWEAPON>", this.Weapons.GetWeaponItem(PlayerList.MurderWeapon).Name.FormatName())
            .Replace("<GHOST>", PlayerList.Ghost.Name.FormatName());
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
    public Location Location;
    
    public List<string> hints;
}