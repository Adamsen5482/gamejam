using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class PublicHints : ScriptableObject
{
    [SerializeField, Required]
    private MurderWeaponsList weaponList;

    [SerializeField, TextArea]
    private List<string> hints;

    public string GetHint()
    {
        var queue = PlayerList.AllPlayers
            .Where(x => x.Role != PlayerRole.Murderer)
            .Where(x => x.Role != PlayerRole.Ghost)
            .Shuffle()
            .ToQueue();

        var text = this.hints.PickRandom()
            .Replace("<WEP>", this.weaponList.Weapons.PickRandom().Name.FormatName())
            .Replace("<WEP_NOTMURDER>", this.weaponList.Weapons.Where(x => x.Type != PlayerList.MurderWeapon).PickRandom().Name.FormatName())
            .Replace("<GHOST>", PlayerList.Ghost.Name.FormatName())
            .Replace("<MURDERER>", PlayerList.Murderer.Name.FormatName())
            .Replace("<PLAYER>", PlayerList.AllPlayers.PickRandom().Name.FormatName())
            .Replace("<NOTMURDERER1>", queue.Dequeue().Name.FormatName())
            .Replace("<NOTMURDERER2>", queue.Dequeue().Name.FormatName())
            .Replace("<NOTMURDERER3>", queue.Dequeue().Name.FormatName())
            ;

        return text;
    }
}
