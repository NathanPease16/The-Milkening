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
            StartCoroutine(FadeIn(3f));
            SoundManager.instance.Loop();
            SoundManager.instance.PlayMusic(song);
        }
            
    }
    void OnTriggerExit(Collider other)
    {
        StartCoroutine(FadeOut(3f));
    }
}
