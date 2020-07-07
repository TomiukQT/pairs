using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject alertBox;
    [SerializeField]
    private TextMeshProUGUI alertText;

    [SerializeField] private TextMeshProUGUI scoreText;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Subscribe();
    }

    private void Start()
    {
        ToggleUI(alertBox, false);
        UpdateScore();
    }

    private void Subscribe()
    {
        gameManager.OnTurnStart += NewTurnAlert;
    }

    private void NewTurnAlert(object sender, OnTurnStartEventArgs e)
    {
        alertText.text = "Player: " + e.playerName;
        if (e.next)
            alertText.text += " is on turn!";
        else
            alertText.text += " is playing again!";
        ToggleUI(alertBox, true);
        UpdateScore();

        StartCoroutine(ToggleAlertAfterTime(alertBox,1f));
    }

    private void UpdateScore()
    {
        scoreText.text = "Score \n";
        foreach (Player p in gameManager.GetPlayers())
        {
            scoreText.text += p.Name + ": " + p.Score + "\n";
        }
    }

    private IEnumerator ToggleAlertAfterTime(GameObject what, float time)
    {
        yield return new WaitForSeconds(time);
        ToggleUI(what);
    }

    private void ToggleUI(GameObject what)
    {
        what.SetActive(!what.activeSelf);
    }

    private void ToggleUI(GameObject what, bool state)
    {
        what.SetActive(state);
    }



}
