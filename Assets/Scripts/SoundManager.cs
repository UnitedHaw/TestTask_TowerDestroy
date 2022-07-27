using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;
    public enum Sound
    {
        BuildingDestroy,
        CanonSound,
        ShellSound,
        ShildActivated
    }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();      
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(sound.ToString()));
    }
}
