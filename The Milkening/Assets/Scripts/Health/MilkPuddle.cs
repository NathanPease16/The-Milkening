using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkPuddle : MonoBehaviour
{
    [Header ("Sounds")]
    public AudioClip _clip;

    [Header("Attributes")]
    [SerializeField] private float healAmount;

    [Header("States")]
    private float animDuration = 1.5f;

    [Header("References")]
    private MilkLevel milk;
    private Animator anim;

    private void Awake()
    {
        milk = GameObject.FindGameObjectWithTag("Player").GetComponent<MilkLevel>();
        anim = GetComponent<Animator>();

        anim.speed = 0;
        anim.Play("Absorb");
    }

    private void Update()
    {
        if (anim.speed == 1)
            animDuration -= Time.deltaTime;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && milk.CurrentMilk < milk.MaxMilk && anim.speed != 1)
        {
            SoundManager.instance.PlaySound(_clip);
            anim.speed = 1;
            StartCoroutine(DrinkMilk());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.speed = 0;
            StopAllCoroutines();
        }
    }

    private IEnumerator DrinkMilk()
    {
        yield return new WaitForSeconds(animDuration);

        milk.UpdateMilkContents(healAmount);

        Destroy(gameObject);
    }
}
