using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                FindFirstObjectByType<AudioManager>().Init();

            return _instance;
        }
    }

    private List<AudioSource> audioSources;

    void Awake()
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Init()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        audioSources = new List<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        AudioSource audioSource;
        CheckForFreeAudioSource(out audioSource);

        if (audioSource == null)
        {
            audioSource = CreateNewAudioSource();
        }
            audioSource.PlayOneShot(audio);
    }

    public void CheckForFreeAudioSource(out AudioSource source)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                source = audioSource;
                return;
            }
        }

        source = null;
    }

    public AudioSource CreateNewAudioSource()
    {
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(newAudioSource);
        return newAudioSource;
    }
}
