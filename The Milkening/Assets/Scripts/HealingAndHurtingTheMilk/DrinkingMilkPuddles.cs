using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingMilkPuddles : MonoBehaviour
{
    Animator animator;
    MilkBar milk;
    int amount = 5;
    void Start()
    {
        animator = GetComponent<Animator>();
        milk = GetComponent<MilkBar>();
    }
    void OnTriggerStay(Collider Other)
    {
        if (Other.tag == "Player")
        {
            
            StartCoroutine(BeginDrinkingMilk());
            
        }
    }
    IEnumerator BeginDrinkingMilk()
    {
        milk.SetHealth(milk.currenthealth + amount);
        animator.SetTrigger("Drunk");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
