using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManage : MonoBehaviour
{
    public AudioSource mapaudio;
    public AudioClip[] clips;
    public int BreakCount = 0;
    public static RockManage instance;

    private void Awake()
    {
        instance = this;
        mapaudio = GetComponent<AudioSource>();
    }

    public void SoundPlay()
    {
        mapaudio.clip = clips[0];
        mapaudio.Play();
    }
    public void RockAttacked()
    {
        mapaudio.clip = clips[1];
        mapaudio.Play();
    }
}
