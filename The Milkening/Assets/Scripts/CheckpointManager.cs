using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [Header("Checkpoint")]
    private int priority = -1;

    [Header("Singleton")]
    public static CheckpointManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0 && priority != -1)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Transform checkpoints = GameObject.FindGameObjectWithTag("Checkpoints").transform;

            foreach (Transform checkpoint in checkpoints)
            {
                Checkpoint chkpt = checkpoint.GetComponent<Checkpoint>();
                if (chkpt.priority == priority)
                {
                    player.position = checkpoint.GetChild(0).position;
                    break;
                }
            }
        }
    }

    public void UpdateCheckpoint(int _priority)
    {
        if (_priority > priority)
            priority = _priority;
    }
}
