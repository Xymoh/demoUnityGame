using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changemusicvolume : MonoBehaviour {

    public Slider volume;
    public AudioSource myMusic;

    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("MusicSlider");
    }

    void Update()
    {
        myMusic.volume = volume.value;
        PlayerPrefs.SetFloat("MusicSlider", volume.value);
    }
}
