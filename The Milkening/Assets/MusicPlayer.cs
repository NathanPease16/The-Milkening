using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip song;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SoundManager.instance.FadeIn(3f));
            SoundManager.instance.Loop();
            SoundManager.instance.PlayMusic(song);
        }
            
    }
    void OnTriggerExit(Collider other)
    {
        StartCoroutine(SoundManager.instance.FadeOut(3f));
    }
}
