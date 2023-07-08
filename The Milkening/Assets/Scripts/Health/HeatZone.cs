using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class HeatZone : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float temperature;

    [Header("References")]
    private MilkLevel milk;

    Volume v;
    Bloom b;

    private void Awake()
    {
        milk = GameObject.FindGameObjectWithTag("Player").GetComponent<MilkLevel>();
        v = GetComponent<Volume>();
        v.profile.TryGet(out b);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
                milk.CurrentTemperature = temperature;
                b.intensity.value = Mathf.Clamp(Mathf.FloorToInt(20f * (temperature/100)), 0, 20);
            }

    }
}
