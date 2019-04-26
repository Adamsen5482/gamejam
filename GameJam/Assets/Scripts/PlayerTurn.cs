using System.Collections;
using UnityEngine;

public abstract class PlayerTurn : MonoBehaviour
{
    public abstract IEnumerator RunTurn(PlayerInfo player);
}
