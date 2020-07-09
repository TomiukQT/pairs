using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class PlayerSelect_UI : MonoBehaviour
{
    private const int MIN_PLAYERS = 2;
    private const int MAX_PLAYERS = 4;
    private List<GameObject> playerUI;
    private int playersCount = 0;

    [SerializeField]
    private GameObject playerUIPrefab;

    private void Awake()
    {
        playerUI = new List<GameObject>();
    }

    private void Start()
    {
        AddPlayer();
        AddPlayer();
    }


    public void AddPlayer()
    {
        if (playersCount >= MAX_PLAYERS)
            return;
        playersCount++;
        GameObject p = Instantiate(playerUIPrefab, this.transform);
        playerUI.Add(p);
    }

    public void RemoveLastPlayer()
    {
        if (playersCount <= 2)
            return;
        playersCount--;
        GameObject p = playerUI[playerUI.Count - 1];
        playerUI.Remove(p);
        Destroy(p);
    }

    public int GetPlayersCount() => playersCount;

    public void GetInfo(int player, out string name, out PlayerType playerType)
    {
        name = "NULL";
        playerType = PlayerType.PLAYER;
        if (player >= playerUI.Count || player < 0)
            return;
        GameObject currPlayer = playerUI[player];
        name = currPlayer.transform.Find("InputName").GetComponent<TMP_InputField>().text;
        playerType = currPlayer.transform.Find("TypeSelect").GetComponent<PlayerTypeSelect>().GetPlayerType();

    }

    

}
