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

    public List<AudioClip> musicClips = new List<AudioClip>();

    System.Random random = new System.Random();

    private void Start()
    {
        musicClips.Add(music1); 
        musicClips.Add(music2); 
        musicClips.Add(music3); 
        musicClips.Add(music4); 
        musicClips.Add(music5);
        musicClips.Add(music6);

        musicSource.clip = musicClips[random.Next(musicClips.Count + 1)];

        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}