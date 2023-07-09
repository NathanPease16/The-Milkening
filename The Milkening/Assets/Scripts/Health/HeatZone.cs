using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class HeatZone : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float temperature;

    [Header("References")]
    private MilkLevel milk;

    [Header ("Sounds")]
    public AudioClip _clip;


    private void Awake()
    {
        milk = GameObject.FindGameObjectWithTag("Player").GetComponent<MilkLevel>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            {
                milk.CurrentTemperature = temperature;
                SoundManager.instance.PlaySound(_clip);
            }

    }
}
