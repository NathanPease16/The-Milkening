using UnityEngine;

public class HeatZone : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float temperature;

    [Header("References")]
    private MilkLevel milk;

    private void Awake()
    {
        milk = GameObject.FindGameObjectWithTag("Player").GetComponent<MilkLevel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            milk.CurrentTemperature = temperature;
    }
}
