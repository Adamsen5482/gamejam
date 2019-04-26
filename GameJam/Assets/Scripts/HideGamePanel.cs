using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HideGamePanel : MonoBehaviour
{
    public Text PlayerName;

    [HideInInspector]
    public bool IsVisible;

    public IEnumerator ShowHidePanel(PlayerInfo player)
    {
        this.IsVisible = true;
        this.PlayerName.text = player.Name;
        this.gameObject.SetActive(true);

        yield return null;
    }

    public void HideHidePanel()
    {
        this.StartCoroutine(this.HideHidePanelCoroutine());
    }

    public IEnumerator HideHidePanelCoroutine()
    {
        Debug.Log("Hiding panel...");
        yield return null;
        this.gameObject.SetActive(false);
        this.IsVisible = false;
    }
}