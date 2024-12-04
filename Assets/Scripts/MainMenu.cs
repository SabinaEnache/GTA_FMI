using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu; // referinta la meniul principal de pe ecranul de start
    public GameObject optionsMenu; // referinta la meniul de optiuni
    public GameObject soundsMenu; // referinta la submeniul optiunilor de sunete
    public Slider volumeSlider; // referinta la sliderul de volum
    public TMP_Text volumeValueText; // referinta la textul care va afisa valoarea sliderului

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

}
