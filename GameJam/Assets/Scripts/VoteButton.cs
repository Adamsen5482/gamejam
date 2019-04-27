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
    public Image KnifeIcon;

    public Color DefaultColor;
    public Color SelectedColor;

    public UnityEvent OnSelect;
    public UnityEventString OnConfirmVote;

    [NonSerialized]
    public string SelectedItemName;

    private void Start()
    {
        this.SelectButton.onClick.AddListener(this.Selected);
        this.ConfirmButton.onClick.AddListener(this.Confirmed);
    }

    private void OnEnable()
    {
        this.ResetSelection();
    }

    public void SetItem(string selectedItemName)
    {
        this.SelectedItemName = selectedItemName;
        this.VoteLabel.text = selectedItemName;
    }

    public void ResetSelection()
    {
        this.ButtonBackground.color = this.DefaultColor;
        this.ConfirmButton.gameObject.SetActive(false);
        this.KnifeIcon.gameObject.SetActive(false);
    }

    private void Selected()
    {
        this.OnSelect.Invoke();
        this.ButtonBackground.color = this.SelectedColor;
        this.KnifeIcon.gameObject.SetActive(true);
        this.ConfirmButton.gameObject.SetActive(true);
    }

    private void Confirmed()
    {
        this.OnConfirmVote.Invoke(this.SelectedItemName);
    }
}

[Serializable]
public class PlayerInfoEvent : UnityEvent<PlayerInfo>
{

}

[Serializable]
public class UnityEventString : UnityEvent<string>
{

}