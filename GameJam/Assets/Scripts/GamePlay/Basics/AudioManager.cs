using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Musics")]
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public AudioClip music5;
    public AudioClip music6;

    [Header("Sound Effects")]
    public AudioClip victory;

    private List<AudioClip> musicClips = new List<AudioClip>();

    private void Start()
    {
        AddIfNotNull(music1);
        AddIfNotNull(music2);
        AddIfNotNull(music3);
        AddIfNotNull(music4);
        AddIfNotNull(music5);
        AddIfNotNull(music6);

        PlayRandomMusic();
    }

    private void Update()
    {
        if (musicClips.Count > 0 && !musicSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    private void AddIfNotNull(AudioClip clip)
    {
        if (clip != null)
        {
            musicClips.Add(clip);
        }
    }

    private void PlayRandomMusic()
    {
        if (musicClips.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, musicClips.Count);
        musicSource.clip = musicClips[randomIndex];
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}