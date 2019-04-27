using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SmartButton : MonoBehaviour
{
    public UnityEvent ClickAction;

    public bool RequireDoubleClick = true;

    private Button button;
    private float lastClick = float.MinValue;

    public bool Interactable
    {
        get { return this.button.interactable; }
        set { this.button.interactable = value; }
    }

    private void Start()
    {
        this.button = this.GetComponent<Button>();
        this.button.onClick.AddListener(this.OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (this.RequireDoubleClick == false)
        {
            this.ClickAction.Invoke();
        }
        else if (Time.time - this.lastClick < 0.5f)
        {
            // Double tapped!
            this.lastClick = float.MinValue;
            this.ClickAction.Invoke();
        }
        else
        {
            this.lastClick = Time.time;
        }
    }
}
