using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown; // referinta la dropdownul pentru calitate
    public Toggle fullScreenToggle; // referinta la toggle ul de fulll screen

    void Start()
    {
        // initializsam cu calitatea curenta
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // setam toggle ul la starea cureanta a ecranului
        if (fullScreenToggle != null)
        {
            fullScreenToggle.isOn = Screen.fullScreen;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        // debug:
        Debug.Log("Calitatea: " + qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen: " + isFullscreen);
    }
}
