using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    public AudioListener audioListener;
    public AudioMixer audioMixer;
    //public AudioSource music;
    //public AudioSource sfx;
    static MixerController instance = null;

    [Space(10)]
    public Button musicButton;
    public Button sfxButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    [Space(10)]
    public Slider musicSlider;
    //public Slider sfxSlider;

    
    void Start()
    {
        //   musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
        musicSlider.value = PlayerPrefs.GetFloat("volumew");
        UpdateIconMusic();
        UpdateIconSfx();
    }

    void Update()
    {
        AudioListener.volume = musicSlider.value;
    }

    private void OnDisable()
    {
        float musicVolume = 0;
        float sfxVolume = 0;

        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("sfxVolume", out sfxVolume);

        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
   //     PlayerPrefs.SetFloat("volumew", AudioListener.volume);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volumew", volume);
     //   audioMixer.SetFloat("musicVolume", volume);
    }

    //public void SetSfxVolume(float volume)
    //{
    //    audioMixer.SetFloat("sfxVolume", volume);
    //}

    void UpdateIconMusic()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            float volume = 0f;
            audioMixer.SetFloat("musicVolume", volume);
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        }
        else
        {
            float volume = -80f;
            audioMixer.SetFloat("musicVolume", volume);
            musicButton.GetComponent<Image>().sprite = musicOffSprite;
        }
    }

    void UpdateIconSfx()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            float volume = 0f;
            audioMixer.SetFloat("sfxVolume", volume);
            sfxButton.GetComponent<Image>().sprite = musicOnSprite;
        }
        else
        {
            float volume = -80f;
            audioMixer.SetFloat("sfxVolume", volume);
            sfxButton.GetComponent<Image>().sprite = musicOffSprite;
        }
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
    }

    public void PauseMusic()
    {
        ToggleSound();
        UpdateIconMusic();
    }

    public void PauseSfx()
    {
        ToggleSound();
        UpdateIconSfx();
    }
}
