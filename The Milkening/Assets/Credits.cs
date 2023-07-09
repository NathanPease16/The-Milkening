using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public AudioClip song;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic(song);
    }

    
}
