using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource; // música
    [SerializeField] private AudioSource sfxSource;   // efectos 

    [SerializeField] private AudioClip[] musicClips;

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
    public enum MusicType
    {
        Menu = 0,
        Game = 1
    }
    private void Awake()
    {
       
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    public void PlayMusic(MusicType type)
    {
        int index = (int)type;

        if (musicClips == null || index < 0 || index >= musicClips.Length)
            return;

        AudioClip clip = musicClips[index];

        
        if (audioSource.clip == clip && audioSource.isPlaying)
            return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}

