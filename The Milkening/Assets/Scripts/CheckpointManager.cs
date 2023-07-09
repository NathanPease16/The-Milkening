using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    [Header("Checkpoint")]
    private int priority = -1;

    [Header("Singleton")]
    public static CheckpointManager instance;

    [Header("References")]
    private Animator checkpointUI;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        checkpointUI = GameObject.FindGameObjectWithTag("CheckptUI").GetComponent<Animator>();
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

        instance = this;
    }

    public void UpdateCheckpoint(int _priority)
    {
        if (_priority > priority)
        {
            priority = _priority;
            StartCoroutine(AnimateUI());
        }
    }

    private IEnumerator AnimateUI()
    {
        checkpointUI.Play("CheckptIn");

        yield return new WaitForSeconds(3f);

        checkpointUI.Play("CheckptOut");
    }
}
