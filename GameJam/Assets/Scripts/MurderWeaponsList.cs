using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class MurderWeaponsList : ScriptableObject
{
    [TableList]
    public List<WeaponItem> Weapons;

    public WeaponItem GetWeaponItem(Weapons weapon)
    {
        return this.Weapons.First(x => x.Type == weapon);
    }
}

public enum Weapons
{
    [HideInInspector]
    Error = 0,
    Keyboard,
    NerfGun,
    PoisonedCoffee,
    BrokenBeerBottle,
    VGACord,
}

[Serializable]
public class WeaponItem
{
    public Weapons Type;
    public string Name;
    [PreviewField, PropertyOrder(-1)]
    public Sprite Icon;
}
