using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LocationVisitor : MonoBehaviour
{
    [Required]
    public Text LocationName;

    [Required]
    public SmartButton EndTurnButton;

    [Required]
    public Hint Hints;

    [Required]
    public Text HintText;

    private bool waitForEndTurn;

    private void Start()
    {
        this.EndTurnButton.ClickAction.AddListener(() => this.waitForEndTurn = false);
    }

    public IEnumerator VisitLocation(PlayerInfo player, Location location)
    {
        this.LocationName.text = location.ToString().ToUpper();

        this.HintText.text = this.Hints.GetHint(player, location);

        this.waitForEndTurn = true;
        while (this.waitForEndTurn)
        {
            yield return null;
        }
    }
}