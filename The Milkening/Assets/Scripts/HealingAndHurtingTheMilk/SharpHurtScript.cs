using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpHurtScript : MonoBehaviour
{
    MilkBar milkbar;
    int dmg = 10;
    // Start is called before the first frame update
    void Start()
    {
        milkbar = GameObject.FindGameObjectWithTag("MilkHealth").GetComponent<MilkBar>();
    }

    void OnTriggerEnter(Collider Object)
    {
        if (Object.tag == "Player")
        {
            milkbar.SetHealth(milkbar.currenthealth -= dmg); 
        }
            
    }
}
