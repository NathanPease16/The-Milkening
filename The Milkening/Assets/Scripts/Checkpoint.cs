using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint")]
    public int priority;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CheckpointManager.instance != null)
            CheckpointManager.instance.UpdateCheckpoint(priority);
    }
}
