using Sirenix.OdinInspector;
using System;
using UnityEngine;

[CreateAssetMenu]
public class RoleIcons : ScriptableObject
{
    [Required, PreviewField] public Sprite MurdererIcon;
    [Required, PreviewField] public Sprite GhostIcon;
    [Required, PreviewField] public Sprite AccompliceIcon;
    [Required, PreviewField] public Sprite DetectiveIcon;

    public Sprite GetRoleIcon(PlayerRole role)
    {
        switch (role)
        {
            case PlayerRole.Murderer:
                return this.MurdererIcon;
            case PlayerRole.Ghost:
                return this.GhostIcon;
            case PlayerRole.Accomplice:
                return this.AccompliceIcon;
            case PlayerRole.Detective:
                return this.DetectiveIcon;
            default:
                throw new InvalidOperationException("Unknown player role: " + role.ToString());
        }
    }
}