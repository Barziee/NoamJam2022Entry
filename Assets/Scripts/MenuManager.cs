using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _creditsMenu;

    private void Start() 
    {
        MainMenu();
    }

    public void MainMenu() 
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _creditsMenu.SetActive(false);
    }

    public void SettingsMenu() 
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
        _creditsMenu.SetActive(false);
    }

    public void CreditsMenu() 
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _creditsMenu.SetActive(true);
    }
}
