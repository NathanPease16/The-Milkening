using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public AudioClip song;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Poop());
        
    }
    IEnumerator Poop()
    {
        SoundManager.instance.PlayMusic(song);
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }

    
}
