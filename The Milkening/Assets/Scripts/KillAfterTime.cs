using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float time;

    void Start()
    {
        StartCoroutine(Kill());
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
