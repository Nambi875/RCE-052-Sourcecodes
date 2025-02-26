using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider BGSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider FootSlider;
    [SerializeField] private Slider VoiceSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            ResetPlayerPrefs();
            PlayerPrefs.SetInt("FirstRun", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetBGVolume();
            SetSFXVolume();
            SetFootVolume();
            SetVoiceVolume();
        }

        SetMasterVolume();
        SetMusicVolume();
        SetBGVolume();
        SetSFXVolume();
        SetFootVolume();
        SetVoiceVolume();
    }

    private void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        BGSlider.value = PlayerPrefs.GetFloat("BGVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        FootSlider.value = PlayerPrefs.GetFloat("FootVolume");
        VoiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetBGVolume();
        SetSFXVolume();
        SetFootVolume();
        SetVoiceVolume();
    }

    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetBGVolume()
    {
        float volume = BGSlider.value;
        myMixer.SetFloat("BG", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void SetFootVolume()
    {
        float volume = FootSlider.value;
        myMixer.SetFloat("Foot", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("FootVolume", volume);
    }
    public void SetVoiceVolume()
    {
        float volume = VoiceSlider.value;
        myMixer.SetFloat("Voice", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("VoiceVolume", volume);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        PlayerPrefs.SetFloat("musicVolume", 1.0f);
        PlayerPrefs.SetFloat("SFXVolume", 1.0f);
        PlayerPrefs.SetFloat("BGVolume", 1.0f);
        PlayerPrefs.SetFloat("FootVolume", 1.0f);
        PlayerPrefs.SetFloat("VoiceVolume", 1.0f);
        PlayerPrefs.Save();
    }
}
