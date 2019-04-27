using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public Text numOfPlayers;
    
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
        this.PlayerNameField.onEndEdit.AddListener(this.OnSubmit);

        this.OnClearClicked();
    }

    private void NewOnSumbit(BaseEventData obj)
    {
        throw new NotImplementedException();
    }

    private void OnClearClicked()
    {
        this.StartGameButton.interactable = false;
        this.AddPlayerButton.interactable = false;
        this.ClearButton.interactable = false;
        this.addedPlayers.Clear();
        this.PlayerNameField.text = string.Empty;
        this.PlayerNamesText.text = string.Empty;
        this.PlayerNameField.interactable = true;
    }

    private void Update()
    {
        if (this.addedPlayers.Count >= this.RequiredPlayerCount.y)
        {
            this.AddPlayerButton.interactable = false;
            this.PlayerNameField.interactable = false;
            return;
        }

        var name = this.PlayerNameField.text.Trim().ToUpper();
        if (string.IsNullOrEmpty(name))
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

        if(numOfPlayers != null)
        {
            string text;
            switch (this.addedPlayers.Count)
            {
                case 5:
                    text = "3 DETECTIVES / 1 MURDERER";
                    break;
                case 6:
                    text = "3 DETECTIVES / 1 ACCOMPLICE\n1 MURDERER";
                    break;
                case 7:
                    text = "4 DETECTIVES / 1 ACCOMPLICE\n1 MURDERER";
                    break;
                case 8:
                    text = "4 DETECTIVES / 2 ACCOMPLICES\n1 MURDERER";
                    break;
                case 9:
                    text = "5 DETECTIVES / 2 ACCOMPLICES\n1 MURDERER";
                    break;
                default:
                    text = "";
                    break;
            }

            numOfPlayers.text = text;
        }
    }

    private void OnStartClicked()
    {
        GameSetup.AssignRoles(this.addedPlayers);
        SceneManager.LoadScene(this.GameScenePath);
    }

    private void OnAddPlayerClicked()
    {
        this.addedPlayers.Add(new PlayerInfo() { Name = this.PlayerNameField.text.Trim().ToUpper() });
        this.PlayerNameField.text = string.Empty;
        this.PlayerNamesText.text = string.Join("\n", this.addedPlayers.Select(x => x.Name.FormatName()));

        this.ClearButton.interactable = true;
        if (this.addedPlayers.Count >= this.RequiredPlayerCount.x)
        {
            this.StartGameButton.interactable = true;
        }
    }

    private void OnSubmit(string text)
    {
        if (this.AddPlayerButton.interactable)
        {
            this.OnAddPlayerClicked();
        }
    }
}