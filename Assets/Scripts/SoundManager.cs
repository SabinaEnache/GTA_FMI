using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider; // slider pt volumul master
    [SerializeField] private TMP_Text masterSliderValue;

    [SerializeField] private Slider musicSlider; // sliderul pt volumul muzicii de fundal
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private TMP_Text musicSliderValue;

    [SerializeField] private Slider sfxSlider; // sliderul pt volumul sunetului de butoane
    [SerializeField] private AudioSource sfxSound;
    [SerializeField] private TMP_Text sfxSliderValue;

    private float masterVolume = 1f; // volumul master actual sub forma de maximul e 1 nu 100

    private void Start()
    {
        float defaultMasterVolume = 100f;
        float defaultMusicVolume = 80f;
        float defaultSfxVolume = 80f;

        masterSlider.value = defaultMasterVolume;
        SetMasterVolume(masterSlider.value);

        musicSlider.value = defaultMusicVolume;
        SetMusicVolume(musicSlider.value);

        sfxSlider.value = defaultSfxVolume;
        SetSfxVolume(sfxSlider.value);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void SetMasterVolume(float sliderValue)
    {
        masterVolume = sliderValue / 100;

        SetMusicVolume(musicSlider.value);
        SetSfxVolume(sfxSlider.value);

        UpdateMasterSliderText(sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = (sliderValue / 100) * masterVolume; // seteaza volumul audio source ului
            UpdateMusicSliderText(sliderValue); // actualizam textul sliderului
        }
    }

    private void UpdateMusicSliderText(float sliderValue)
    {
        if (musicSliderValue != null)
        {
            musicSliderValue.text = Mathf.RoundToInt(sliderValue).ToString(); // afisam valoarea sliderului fara zecimale
        }
    }

    public void SetSfxVolume(float sliderValue)
    {
        if (sfxSound != null)
        {
            sfxSound.volume = (sliderValue / 100) * masterVolume;
            UpdateSfxSliderText(sliderValue);
        }
    }

    private void UpdateMasterSliderText(float sliderValue)
    {
        if (masterSliderValue != null)
        {
            masterSliderValue.text = Mathf.RoundToInt(sliderValue).ToString();
        }
    }

    private void UpdateSfxSliderText(float sliderValue)
    {
        if (sfxSliderValue != null)
        {
            sfxSliderValue.text = Mathf.RoundToInt(sliderValue).ToString();
        }
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
    }

    // reda sunetul de buton
    public void PlayButtonSound()
    {
        if (sfxSound != null)
        {
            sfxSound.PlayOneShot(sfxSound.clip);
        }
    }

    public void ResetAllVolumes()
    {
        float defaultMasterVolume = 100f;
        float defaultMusicVolume = 80f;
        float defaultSfxVolume = 80f;

        // reseteaza master
        masterSlider.value = defaultMasterVolume;
        SetMasterVolume(defaultMasterVolume);

        // reseteaza music
        musicSlider.value = defaultMusicVolume;
        SetMusicVolume(defaultMusicVolume);

        // reseteaza sfx
        sfxSlider.value = defaultSfxVolume;
        SetSfxVolume(defaultSfxVolume);
    }

}
