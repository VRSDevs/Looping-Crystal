using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class mainMenuScript : MonoBehaviour
{
    //************ VARIABLES ************//
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    // GAMEOBJECTS //
    public GameObject mainMenuGO;   // GO del menú principal
    public GameObject settingsGO;   //GO del menú de ajustes
    public GameObject creditsGO;
    public GameObject skipGO;
    public CanvasGroup[] menus;
    int menuIndex;
    // ANIMATORS //
    public Animator[] listAnimatorsMM;  // Lista de animadores del menú principal
    public Animator[] listAnimatorsSM;  // Lista de animadores del menú de ajustes
    public Animator[] listAnimatorsCM;
    public Animator fadeScene;

    // AUXILIARES //
    char menu;  // Variable auxiliar para detectar el menú de reinicio de animaciones
    bool canSkip;

    public VideoPlayer videoInicio;
    public GameObject render;
    //************ FUNCIONES ************//
    // START //
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        // Reproducir música de fondo del juego
        FindObjectOfType<AudioManager>().Play("BGMusic");

        Cursor.visible = true;
    }

    void Update() {
        if(canSkip) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                skipGO.SetActive(false);
                PrepareGame();
            }
        }
    }

    // INVOCACIÓN DE FUNCIONES //
    public void InvokeFunction(string function)
    {
        Invoke(function, 2.0f);
    }

    // JUGAR //
    public void botonPlay()
    {
        Invoke("ponerVideo",2);
        Invoke("quitarMenu", 2);
        Invoke("quitarVideo",22);
        Invoke("PrepareGame", 21);

    }


    public void PrepareGame()
    {
        fadeScene.SetBool("Start", true);
        Invoke("PlayGame", 2.0f);
    }

    void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    // MOSTRAR MENÚ PRINCIPAL //
    public void ShowMainMenu()
    {
        // Activación / Desactivación de GOs
        mainMenuGO.SetActive(true);
        settingsGO.SetActive(false);
        creditsGO.SetActive(false);

        // Ajuste variable auxiliar
        menu = 'm';
        // Invocar función de reset de animaciones
        InvokeFunction("ResetAnimation");
    }

    // MOSTRAR MENÚ AJUSTES //
    public void ShowSettingsMenu()
    {
        // Activación / Desactivación de GOs
        mainMenuGO.SetActive(false);
        settingsGO.SetActive(true);

        // Ajuste variable auxiliar
        menu = 's';
        // Invocar función de reset de animaciones
        InvokeFunction("ResetAnimation");
    }

    public void ShowCreditsMenu() {
        // Activación / Desactivación de GOs
        mainMenuGO.SetActive(false);
        creditsGO.SetActive(true);

        // Ajuste variable auxiliar
        menu = 'c';
        // Invocar función de reset de animaciones
        InvokeFunction("ResetAnimation");
    }

    // SALIR DEL JUEGO //
    public void QuitGame()
    {
        Application.Quit();
    }
    void ActivateButtons() {
        switch (menuIndex)
        {
            case 0:
                menus[menuIndex].interactable = true;
                break;
            case 1:
                menus[menuIndex].interactable = true;
                break;
            case 2:
                menus[menuIndex].interactable = true;
                break;
        }
    }

    public void DeactivateButtons(int i) {
        switch (i)
        {
            case 0:
                menus[menuIndex].interactable = false;
                break;
            case 1:
                menus[menuIndex].interactable = false;
                break;
            case 2:
                menus[menuIndex].interactable = false;
                break;
        }
    }

    // RESET ANIMACIONES BOTONES //
    void ResetAnimation()
    {
        switch (menu)
        {
            case 'm':
                foreach (Animator a in listAnimatorsMM)
                {
                    a.SetBool("StartAnimation", true);
                }
                menuIndex = 0;
                Invoke("ActivateButtons", 1.0f);
                break;
            case 's':
                foreach (Animator a in listAnimatorsSM)
                {
                    a.SetBool("StartAnimation", true);
                }
                menuIndex = 1;
                Invoke("ActivateButtons", 1.0f);
                break;
            case 'c':
                foreach (Animator a in listAnimatorsCM)
                {
                    a.SetBool("StartAnimation", true);
                }
                menuIndex = 2;
                Invoke("ActivateButtons", 1.0f);
                break;
        }
    }

    public void SetFullscreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

    public void setResolution(int resIndex) {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }



    public void ponerVideo()
    {
        render.SetActive(true);
        videoInicio.gameObject.SetActive(true);
        videoInicio.Play();
        Invoke("AbleSkip", 2.0f);
    }

    public void quitarVideo()
    {
        render.SetActive(false);
        videoInicio.gameObject.SetActive(false);
        videoInicio.Stop();
    }

    public void quitarMenu() 
    {
        mainMenuGO.SetActive(false);
    }

    void AbleSkip() {
        canSkip = true;
        skipGO.SetActive(true);
    }
}
