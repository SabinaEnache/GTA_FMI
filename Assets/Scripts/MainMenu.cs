using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu; // referinta la meniul principal de pe ecranul de start
    public GameObject optionsMenu; // referinta la meniul de optiuni
    public GameObject soundsMenu; // referinta la submeniul optiunilor de sunete
    public Slider volumeSlider; // referinta la sliderul de volum
    public TMP_Text volumeValueText; // referinta la textul care va afisa valoarea sliderului
    public GameObject credits; // referinta la panoul de credite
    public Button GitHubButton; // referinta catre butonul de github
    public GameObject graphicsMenu; // referinta la submeniul de setari
    public Sprite muteImage; // imaginea pt cand e pe mute
    public Sprite unmuteImage; // imaginea pt cand e pe sunet
    public Button muteButton; // referinta la butonul de mute/unmute

    private bool isMute = false; // starea curenta a sunetului

    // dezactivam meniul principal si activam meniul de optiuni
    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // revenim la meniul principal si dezactivam meniul de optiuni
    public void CloseOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // activam submeniul de sunete si ascundem meniul de optiuni
    public void OpenSoundsOptions()
    {
        optionsMenu.SetActive(false);
        soundsMenu.SetActive(true);
    }

    // dezactivam submeniul de sunete si activam meniul de optiuni pt a reveni la toate optiunile
    public void CloseSoundsOptions()
    {
        soundsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // incarca scena jocului si incepe jocul
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // !!!VERIFICA sa fie scena jocului adaugata in Build Settings si schimba numele!!!
    }

    // inchide jocul
    public void ExitGame()
    {
        // verifica daca aplicatia ruleaza in Editor
#if UNITY_EDITOR
        // opreste modeul Play
        EditorApplication.isPlaying = false;
#else
        // inchide aplicatia
        Application.Quit();
#endif
    }

    // pentru deschiderea linkului de github
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/SabinaEnache/GTA_FMI");
        // resetam starea butonului ca sa nu ramana apasat si sa functioneze in continuare highlight color
        EventSystem.current.SetSelectedGameObject(null); // adica deselectam butonul
    }

    // activam submeniul de credite si ascundem meniul de optiuni
    public void OpenCredits()
    {
        optionsMenu.SetActive(false);
        credits.SetActive(true);
    }

    // dezactivam submeniul de credite si activam meniul de optiuni pt a reveni la toate optiunile
    public void CloseCredits()
    {
        credits.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // activam submeniul de grafica si ascundem meniul de optiuni
    public void OpenGraphicsMenu()
    {
        optionsMenu.SetActive(false);
        graphicsMenu.SetActive(true);
    }

    // dezactivam submeniul de grafuca si activam meniul de optiuni pt a reveni la toate optiunile
    public void CloseGraphicsMenu()
    {
        graphicsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // pt meniul de setari din timpul jocului

    // fct pentru a reveni la meniul principal
    public void QuitLevel()
    {
        SceneManager.LoadScene("Start Screen");
    }

    // fct pt a restarta scena curenta, adica nivelul
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // am obtinut scena curenta
        SceneManager.LoadScene(currentScene.name); // si o reincarcam
    }

    // fcti pt butonul de mute/unmute

    private void Start()
    {
        UpdateButtonImage();
    }

    public void Mute()
    {
        isMute = !isMute; // trecem de la o stare la alta
        AudioListener.pause = isMute; // pornim sau oprim sunetul
        UpdateButtonImage(); // updatam imaginea
        // resetam starea butonului ca sa nu ramana apasat si sa functioneze in continuare highlight color
        EventSystem.current.SetSelectedGameObject(null); // adica deselectam butonul
    }

    private void UpdateButtonImage()
    {
        if (isMute)
        {
            muteButton.image.sprite = muteImage;
        }
        else
        {
            muteButton.image.sprite = unmuteImage;
        }
    }










}
