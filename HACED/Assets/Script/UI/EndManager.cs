using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    private GameObject endCanvas;

    void Start() => endCanvas = GameObject.Find("End_canvas");
    public void SetActiveEnd(bool state) => endCanvas.SetActive(state);
    public void Restart() => SceneManager.LoadScene(2);
    public void MainMenu() => SceneManager.LoadScene(0);
    public void Quit() => Application.Quit(); 
}
