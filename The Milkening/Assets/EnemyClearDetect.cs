using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClearDetect : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GameObject> blocks;
    private bool activated;

    void Awake()
    {
        foreach(GameObject block in blocks) {
            block.GetComponent<Rigidbody>().isKinematic = true;
            block.GetComponent<Rigidbody>().detectCollisions = false;
        }
        activated = false;
    }

    void Update()
    {
        enemies.RemoveAll(s => s == null);

        if(!activated && enemies.Count == 0) {
            activated = true;
            foreach(GameObject block in blocks) {
                block.GetComponent<Rigidbody>().isKinematic = false;
                block.GetComponent<Rigidbody>().detectCollisions = true;
                block.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 3);
            }
        }
    }
}
