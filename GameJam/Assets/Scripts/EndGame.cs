using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGame : MonoBehaviour
{
    
    public Text resultText;
    public Text whowinText;
    public MurderWeaponsList Weapons;

    public void OnEnable()
    {
        buildEndGame();
    }
    public void buildEndGame()
    {
       
        string result = PlayerList.Murderer.Name.FormatName() + " \nMURDERED THE VICTIM USING \n" + this.Weapons.GetWeaponItem(PlayerList.MurderWeapon).Name.FormatName(); 
        resultText.text= result;
    }

    public void OnDisconnectClicked()
    { 
        SceneManager.LoadScene(0);

    }
}
