using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Menu_UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI creditsText;
    [SerializeField]
    private GameObject creditsParent;

    [SerializeField]
    private GameObject playerSelectParent;

    private void Awake()
    {
        ToggleUI(creditsParent, false);
        creditsText.text += Application.version;
        ToggleUI(playerSelectParent, false);
    }

    public void StartGame()
    {
        
    }

    public void ShowPlayerSelect()
    {
        ToggleUI(playerSelectParent);
    }

    public void Credits()
    {
        ToggleUI(creditsParent);
    }

    public void ExitGame()
    {
        Application.Quit();
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
