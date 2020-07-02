using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI alertText;

    private void Awake()
    {
        
    }

    private void Start()
    {
        ToggleUI(alertText.gameObject, false);
    }

    private void Subscribe()
    {

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
