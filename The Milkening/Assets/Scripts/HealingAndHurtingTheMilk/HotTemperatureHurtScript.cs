using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotTemperatureHurtScript : MonoBehaviour
{
    MilkBar milkbar;
    // Start is called before the first frame update
    void Start()
    {
        milkbar = GameObject.FindGameObjectWithTag("MilkHealth").GetComponent<MilkBar>();
    }

    void OnTriggerEnter(Collider Object)
    {
        if (Object.tag == "Player")
        {
            milkbar.rot = true;
        }
            
    }

    void OnTriggerExit (Collider Object)
    {
        {
        if (Object.tag == "Player")
        {
            milkbar.rot = false;
        }
            
    }
    }
}
