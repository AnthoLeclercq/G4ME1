using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    private void Start() => ActivateMainMenu(true);
    public void ActivateMainMenu(bool state) => mainMenu.SetActive(state);
    public void Play() => SceneManager.LoadScene(1);
    public void Quit() => Application.Quit();
}
