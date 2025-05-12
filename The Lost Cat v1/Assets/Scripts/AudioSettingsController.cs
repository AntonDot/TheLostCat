using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
        float music = PlayerPrefs.GetFloat("MusicVolume", -20f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", -20f);

        mixer.SetFloat("MusicVolume", music);
        mixer.SetFloat("SFXVolume", sfx);

        musicSlider.value = music;
        sfxSlider.value = sfx;
    }

    public void UpdateMusicVolume()
    {
        float value = musicSlider.value;
        mixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void UpdateSFXVolume()
    {
        float value = sfxSlider.value;
        mixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}