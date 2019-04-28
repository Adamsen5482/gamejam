using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HideGamePanel : MonoBehaviour
{
    [Required]
    public Text PlayerName;

    [Required]
    public Text FlavorText;

    [Required]
    public SmartButton ConfirmButton;
    
    private bool waitForConfirm;

    private void Start()
    {
        this.ConfirmButton.ClickAction.AddListener(() => this.StartCoroutine(this.HideHidePanelCoroutine()));
    }

    public IEnumerator ShowHidePanel(string name, string flavorText = null)
    {
        this.PlayerName.text = name.ToUpper();
        this.FlavorText.text = flavorText;
        this.gameObject.SetActive(true);

        this.waitForConfirm = true;
        while (this.waitForConfirm)
        {
            yield return null;
        }
    }

    public IEnumerator HideHidePanelCoroutine()
    {
        this.waitForConfirm = false;
        yield return null;
        //this.gameObject.SetActive(false);
    }
}