using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class MurderWeaponsList : ScriptableObject
{
    [TableList]
    public List<Weapon> Weapons;
}
