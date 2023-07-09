using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Sound")]
    public AudioClip clip;
    [Header("Checkpoint")]
    public int priority;
    bool PlayOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CheckpointManager.instance != null)
        {
            CheckpointManager.instance.UpdateCheckpoint(priority);
            if (PlayOnce) 
            {
                SoundManager.instance.PlaySound(clip);
                PlayOnce = false;
            }   
                
        }
    }
}
