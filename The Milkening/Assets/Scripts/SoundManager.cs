using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance;
    [SerializeField] private AudioSource _musicSource, _effectsSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
            return;
        
        _effectsSource.PlayOneShot(clip);
    } 
    
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void Pitch(float pitch)
    {
        _effectsSource.pitch = pitch;
    }

    public void Loop()
    {
        _musicSource.loop = true;
    }
}
