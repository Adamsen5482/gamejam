using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MurderWeaponVotingTurn : PlayerTurn
{
    [Required]
    public MurderWeaponsList WeaponList;

    public List<VoteButton> Buttons;

    private string votedWeaponName;

    [NonSerialized]
    public WeaponItem VotedWeapon;
    
    private void Start()
    {
        for (int i = 0; i < this.WeaponList.Weapons.Count; i++)
        {
            this.Buttons[i].SetItem(this.WeaponList.Weapons[i].Name);
            this.Buttons[i].OnSelect.AddListener(this.OnSelection);
            this.Buttons[i].OnConfirmVote.AddListener(x => this.votedWeaponName = x);
        }
    }

    public override IEnumerator RunTurn(PlayerInfo player)
    {
        foreach (var b in this.Buttons)
        {
            b.ResetSelection();
        }

        this.votedWeaponName = null;
        this.VotedWeapon = null;
        while (this.votedWeaponName == null)
        {
            yield return null;
        }

        this.VotedWeapon = this.WeaponList.Weapons.First(x => x.Name == votedWeaponName);
    }

    private void OnSelection()
    {
        foreach (var b in this.Buttons)
        {
            b.ResetSelection();
        }
    }
}