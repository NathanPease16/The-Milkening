using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemies : MonoBehaviour
{
    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Enemy")
            Destroy(Other.gameObject);
    }
}
