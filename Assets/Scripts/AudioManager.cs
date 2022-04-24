using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource[] audioSources;

    void Awake()
    {
        this.audioSources = GetComponents<AudioSource>();
    }

    public void PlayAudioClip(EAudioClip audioClip)
    {
        var audioSource = GetFreeAudioSource();
        if (audioSource == null)
            return;
        audioSource.clip = clips[(int) audioClip];
        audioSource.Play();
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (var audioSource in audioSources) {
            if (audioSource.isPlaying)
                continue;

            return audioSource;
        }

        return null;
    }
}

public enum EAudioClip
{
    UI_CLICK,
    UI_NEW_TASK
}
