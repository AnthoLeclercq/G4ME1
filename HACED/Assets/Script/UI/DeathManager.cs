using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private GameObject deathCanvas;

    void Start() => deathCanvas = GameObject.Find("Death_canvas");
    public void SetActiveDeath(bool state) => deathCanvas.SetActive(state);
    public void Restart() => SceneManager.LoadScene(2);
    public void MainMenu() => SceneManager.LoadScene(0);
    public void Quit() => Application.Quit(); 
}
