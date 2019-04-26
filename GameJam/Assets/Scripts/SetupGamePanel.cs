using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupGamePanel : MonoBehaviour
{
    [Required]
    public Button StartGameButton;
    [Required]
    public Button AddPlayerButton;
    [Required]
    public Button ClearButton;
    [Required]
    public InputField PlayerNameField;
    [Required]
    public Text PlayerNamesText;

    [Required, FilePath(Extensions = ".unity")]
    public string GameScenePath;

    [MinMaxSlider(1, 15, ShowFields = true)]
    public Vector2Int RequiredPlayerCount;

    private readonly List<PlayerInfo> addedPlayers = new List<PlayerInfo>();

    private void Start()
    {
        this.StartGameButton.onClick.AddListener(this.OnStartClicked);
        this.AddPlayerButton.onClick.AddListener(this.OnAddPlayerClicked);
        this.ClearButton.onClick.AddListener(this.OnClearClicked);

        this.OnClearClicked();
    }

    private void OnClearClicked()
    {
        this.StartGameButton.interactable = false;
        this.AddPlayerButton.interactable = false;
        this.ClearButton.interactable = false;
        this.addedPlayers.Clear();
        this.PlayerNameField.text = string.Empty;
        this.PlayerNamesText.text = string.Empty;
    }

    private void Update()
    {
        var name = this.PlayerNameField.text.Trim();
        if (this.addedPlayers.Count > this.RequiredPlayerCount.y)
        {
            this.AddPlayerButton.interactable = false;
        }
        else if (string.IsNullOrEmpty(name))
        {
            this.AddPlayerButton.interactable = false;
        }
        else if (this.addedPlayers.Any(x => x.Name == name))
        {
            this.AddPlayerButton.interactable = false;
        }
        else
        {
            this.AddPlayerButton.interactable = true;
        }
    }

    private void OnStartClicked()
    {
        GameSetup.AssignRoles(this.addedPlayers);

        SceneManager.LoadScene(this.GameScenePath);
    }

    private void OnAddPlayerClicked()
    {
        this.addedPlayers.Add(new PlayerInfo() { Name = this.PlayerNameField.text.Trim() });
        this.PlayerNameField.text = string.Empty;
        this.PlayerNamesText.text = string.Join("\n", this.addedPlayers.Select(x => x.Name));

        this.ClearButton.interactable = true;
        if (this.addedPlayers.Count >= this.RequiredPlayerCount.x)
        {
            this.StartGameButton.interactable = true;
        }
    }
}