using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // To any future programmers:
    // Sound Manager is instanced and can be used anywhere,
    // add more sounds using the array menu via the attached script on the Sound Manager Game Object.
    //
    // Implemented commands are as follow:
    // 
    // Play music with the same name
    // SoundManager.Instance.PlayMusic("MusicName");
    //
    // Stop music with the same name
    // SoundManager.Instance.StopMusic("MusicName");
    //
    // Set volume of music with the same name
    // SoundManager.Instance.SetMusicVolume(float);
    //
    // Play sound with the same name
    // SoundManager.Instance.PlaySound("SoundName");
    //
    // Stop sound with the same name
    // SoundManager.Instance.StopSound("SoundName");
    //
    // Set volume of sound with the same name
    // SoundManager.Instance.SetVolume("SoundName", 0.5f);

    public static SoundManager Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
        public float volume = 1.0f;
    }

    public Sound[] musicTracks;
    public Sound[] soundEffects;

    private AudioSource musicSource;
    private Dictionary<string, AudioSource> sfxSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSoundManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSoundManager()
    {
        musicSource = gameObject.AddComponent<AudioSource>();

        foreach (Sound s in musicTracks)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.loop = s.loop;
            source.volume = s.volume;
            sfxSources.Add(s.name, source);
        }

        foreach (Sound s in soundEffects)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.loop = s.loop;
            source.volume = s.volume;
            sfxSources.Add(s.name, source);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicTracks, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.loop = s.loop;
        musicSource.volume = s.volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySound(string name)
    {
        if (sfxSources.ContainsKey(name))
        {
            sfxSources[name].Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void StopSound(string name)
    {
        if (sfxSources.ContainsKey(name))
        {
            sfxSources[name].Stop();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void SetVolume(string name, float volume)
    {
        if (sfxSources.ContainsKey(name))
        {
            sfxSources[name].volume = volume;
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
