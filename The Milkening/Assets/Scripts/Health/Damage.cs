using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header ("Sounds")]
    public AudioClip _clip;

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
        {
            SoundManager.instance.PlaySound(_clip);
            milk.UpdateMilkContents(-damage);
        }
            
    }
}
