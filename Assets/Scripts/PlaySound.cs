using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource _bingSound;
    void Awake()
    {
        _bingSound = GetComponents<AudioSource>().First(x => x.clip.name == "ping");
    }

    public void PlayBingSound()
    {
        _bingSound.Play();
    }
}
