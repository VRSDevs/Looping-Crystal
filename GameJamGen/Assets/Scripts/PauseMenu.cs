using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //************ VARIABLES ************//
    public static bool gamePaused = false;
    bool calledPauseMenu = false;
    public GameObject gameUI;
    public Animator pauseAnimator;

    //************ FUNCIONES ************//
    // REANUDAR JUEGO //
    public void Resume() {
        Time.timeScale = 1f;
        pauseAnimator.SetBool("ShowPauseMenu", false);
        gamePaused = false;
        Cursor.visible = false;
        calledPauseMenu = false;
    }

    // PAUSAR EL JUEGO //
    void Pause(){
        Cursor.visible = true;
        Time.timeScale = 0f;
        gamePaused = true;
        calledPauseMenu = false;
    }

    // IR AL MENÚ PRINCIPAL //
    public void goToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.Find("AudioManager"));
    }

    // REINICIAR PARTIDA //
    public void restartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void InvokeFunction(string function, float time) {
        Invoke(function, time);
    }

    // UPDATE //
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!calledPauseMenu) {
                calledPauseMenu = true;
                if(gamePaused) {
                    Time.timeScale = 1f;
                    pauseAnimator.SetBool("ShowPauseMenu", false);

                    InvokeFunction("Resume", 1.0f);
                } else {
                    pauseAnimator.SetBool("ShowPauseMenu", true);

                    InvokeFunction("Pause", 1.6f);
                }
            }
        }
    }
}
