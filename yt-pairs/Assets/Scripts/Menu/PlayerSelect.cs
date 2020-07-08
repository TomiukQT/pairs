using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        DontDestroyOnLoad(gameObject);
    }

    public static PlayerSelect Instance() => instance;





}
