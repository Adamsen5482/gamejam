using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text resultText;
    public Text whowinText;
    public MurderWeaponsList Weapons;

    public void BuildEndGame(PlayerInfo executedPlayer, WeaponItem votedForMurderWeapon)
    {
        this.resultText.text= PlayerList.Murderer.Name.FormatName() + " \nMURDERED THE VICTIM USING \n" + this.Weapons.GetWeaponItem(PlayerList.MurderWeapon).Name.FormatName();

        if (executedPlayer.Role == PlayerRole.Murderer && PlayerList.MurderWeapon == votedForMurderWeapon.Type)
        {
            // Ghost wins!
            this.whowinText.text =
$@"THEREFORE WE CAN CONFIRM THAT
{PlayerList.Ghost.Name.FormatName()} CHOSE CORRECTLY
THE MURDERER TEAM LOSES
GOODNIGHT."; // Ghost is still dead btw.
        }
        else
        {
            // Murderer wins!
            this.whowinText.text =
$@"THEREFORE WE CAN CONFIRM THAT
{PlayerList.Ghost.Name.FormatName()} CHOSE WRONGLY
THE MURDERER HAS WON
GOODNIGHT.";
        }
    }

    public void OnDisconnectClicked()
    { 
        SceneManager.LoadScene(0);
    }
}
