using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgm;
    public AudioSource sfx;

    public AudioClip[] clips;
    private void Awake()
    {
        bgm = gameObject.AddComponent<AudioSource>();
        bgm.clip = clips[0];
        sfx = gameObject.AddComponent<AudioSource>();
        sfx.clip = clips[1];
    }
}
