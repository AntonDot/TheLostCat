using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AudioSettingsController : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mixer;

    [Header("UI Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Загружаем сохранённые значения или устанавливаем по умолчанию
        float music = PlayerPrefs.GetFloat("MusicVolume", 0);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 0);

        mixer.SetFloat("MusicVolume", music);
        mixer.SetFloat("SFXVolume", sfx);

        musicSlider.value = 1;
        sfxSlider.value = 1;
    }

    public void UpdateMusicVolume()
    {
        float value = Mathf.Log10(musicSlider.value)*20;
        mixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void UpdateSFXVolume()
    {
        float value = Mathf.Log10(sfxSlider.value)*20;
        mixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}