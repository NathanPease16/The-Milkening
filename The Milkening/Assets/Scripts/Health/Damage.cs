using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float damage;

    [Header("References")]
    private MilkLevel milk;

    private void Awake()
    {
        milk = GameObject.FindGameObjectWithTag("Player").GetComponent<MilkLevel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            milk.UpdateMilkContents(-damage);
    }
}
