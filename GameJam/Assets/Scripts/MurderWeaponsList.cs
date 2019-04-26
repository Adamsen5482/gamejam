using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MurderWeaponsList : ScriptableObject
{
    [TableList]
    public List<Weapon> Weapons;
}
