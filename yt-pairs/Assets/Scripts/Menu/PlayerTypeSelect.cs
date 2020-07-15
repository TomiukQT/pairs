using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PlayerType
{ PLAYER,EASYAI, MEDIUMAI, HARDAI, AI }

public class PlayerTypeSelect : MonoBehaviour
{
    private PlayerType playerType;
    private const int MAX_PLAYER_TYPE = 3;

    [SerializeField]
    private TextMeshProUGUI playerTypeText;

    private void Awake()
    {
        playerType = PlayerType.PLAYER;
        UpdateUI();
    }


    public PlayerType GetPlayerType() => playerType;

    public void NextType(int dir = 1)
    {
        playerType += dir;
        if (playerType < 0)
            playerType = (PlayerType)MAX_PLAYER_TYPE;
        if (playerType > (PlayerType)MAX_PLAYER_TYPE)
            playerType = (PlayerType)0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        playerTypeText.text = PlayerTypeToString(playerType);
    }

    private string PlayerTypeToString(PlayerType playerType)
    {
        return playerType == PlayerType.PLAYER ? "Player" : playerType == PlayerType.AI ? "AI" : playerType == PlayerType.EASYAI ? "Easy AI" : 
            playerType == PlayerType.MEDIUMAI ? "Medium AI" : playerType == PlayerType.HARDAI ? "Hard AI" : "None";
    }

    //public static PlayerType StringToPlayerType(string s)
    //{
    //    return s.ToUpper() == "PLAYER" ? PlayerType.PLAYER : PlayerType.AI;
    //}


}
