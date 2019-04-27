using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPicker : MonoBehaviour
{
    [SerializeField]
    private List<LocationButton> buttons;

    [NonSerialized]
    public Location PickedLocation;

    private void Start()
    {
        for (int i = 0; i < this.buttons.Count; i++)
        {
            var b = this.buttons[i];
            b.Button.ClickAction.AddListener(() => this.OnLocationPick(b));
        }
    }

    public IEnumerator PickLocation(PlayerInfo player)
    {
        this.PickedLocation = Location.Error;

        while (this.PickedLocation == Location.Error)
        {
            yield return null;
        }
    }

    private void OnLocationPick(LocationButton button)
    {
        this.PickedLocation = button.Location;
        button.Button.Interactable = false;
    }

    [Serializable]
    private class LocationButton
    {
        public Location Location;
        [Required]
        public SmartButton Button;
    }
}
