using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class PlayerSelect : MonoBehaviour
{
    private static PlayerSelect instance;

    public List<IPlayer> players;
    public int currPlayersCount;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        players = new List<IPlayer>();
        DontDestroyOnLoad(gameObject);
    }

    public static PlayerSelect Instance() => instance;




    private void GetInfoFromUI()
    {
        PlayerSelect_UI playerSelectUI = GameObject.Find("PlayerSelect").GetComponent<PlayerSelect_UI>();
        for (int i = 0; i < playerSelectUI.GetPlayersCount(); i++)
        {
            string name;
            PlayerType playerType;
            playerSelectUI.GetInfo(i, out name, out playerType);
            CreateNewPlayer(name, playerType);
        }
    }

    private void CreateNewPlayer(string name, PlayerType playerType)
    {
        IPlayer player = null;
        if(playerType == PlayerType.PLAYER)
            player = Instantiate(new GameObject(), transform).AddComponent<Player>();
        else if (playerType == PlayerType.AI)
            player = Instantiate(new GameObject(), transform).AddComponent<AIPlayer>();
        player.Name = name;
        players.Add(player);
    }


    public void StartGame()
    {
        GetInfoFromUI();
        //GEt all info about players and create players
        SceneManager.LoadScene("main");
    }



}
