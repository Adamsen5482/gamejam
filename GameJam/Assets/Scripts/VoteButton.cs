using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VoteButton : MonoBehaviour
{
    public Text VoteLabel;
    public Button SelectButton;
    public Button ConfirmButton;
    public Image ButtonBackground;

    public Color DefaultColor;
    public Color SelectedColor;

    public UnityEvent OnSelect;
    public PlayerInfoEvent OnConfirmVote;

    [NonSerialized]
    public PlayerInfo Player;

    private void Start()
    {
        this.SelectButton.onClick.AddListener(this.Selected);
        this.ConfirmButton.onClick.AddListener(this.Confirmed);
    }

    private void OnEnable()
    {
        this.ResetSelection();
    }

    public void SetPlayer(PlayerInfo player)
    {
        this.Player = player;
        this.VoteLabel.text = this.Player.Name;
    }

    public void ResetSelection()
    {
        this.ButtonBackground.color = this.DefaultColor;
        this.ConfirmButton.gameObject.SetActive(false);
    }

    private void Selected()
    {
        this.OnSelect.Invoke();
        this.ButtonBackground.color = this.SelectedColor;
        this.ConfirmButton.gameObject.SetActive(true);
    }

    private void Confirmed()
    {
        this.OnConfirmVote.Invoke(this.Player);
    }
}

[Serializable]
public class PlayerInfoEvent : UnityEvent<PlayerInfo>
{

}