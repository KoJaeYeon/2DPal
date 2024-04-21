using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgm;
    public AudioSource[] sfx;

    public AudioClip[] clips;

    public Slider[] sliders;
    private void Awake()
    {
        sfx = new AudioSource[3];
        bgm = gameObject.AddComponent<AudioSource>();
        bgm.loop = true;
        bgm.clip = clips[0];
        sfx[0] = gameObject.AddComponent<AudioSource>();
        sfx[0].clip = clips[1];
        sfx[1] = gameObject.AddComponent<AudioSource>();
        sfx[1].clip = clips[2];
        sfx[2] = gameObject.AddComponent<AudioSource>();
        sfx[2].clip = clips[3];
    }

    private void Start()
    {
        bgm.Play();
    }

    public void bgmVolume()
    {
        bgm.volume = sliders[0].value;
    }

    public void bgmVolume(float bgmVolume)
    {
        sliders[0].value = bgmVolume;
        bgm.volume = sliders[0].value;
    }

    public void sfxVolume()
    {
        sfx[0].volume = sliders[1].value;
        sfx[1].volume = sliders[1].value;
        sfx[2].volume = sliders[1].value;
    }

    public void sfxVolume(float sfxVolume)
    {
        sliders[1].value = sfxVolume;
        sfx[0].volume = sliders[1].value;
        sfx[1].volume = sliders[1].value;
        sfx[2].volume = sliders[1].value;
    }
}
