using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MilkBar : MonoBehaviour
{

    private Slider slider;
	public Gradient gradient;
    public Gradient rot_gradient;
	private Image fill;

    public bool rot;

    private float timeBetweenDoingSomething = 2f; // wait two seconds
    private float timeWhenWeNextDoSomething;  //The next time we will do something

    [HideInInspector]
    public int maxhealth = 100;
    [HideInInspector]
    public int currenthealth;
 

    void Start()
    {
        //don't rearrange the hierarchy of stuff :( script depends on it
        slider = transform.GetChild(0).GetComponent<Slider>();
        Transform fill_ref = slider.transform.GetChild(2);
        fill = fill_ref.GetChild(0).GetComponent<Image>();

        timeWhenWeNextDoSomething = Time.time + timeBetweenDoingSomething;

        SetMaxHealth(maxhealth);
        maxhealth = currenthealth;
        SetHealth(currenthealth);
    }

    void Update()
    {
        if (timeWhenWeNextDoSomething <= Time.time && rot)
        {
            currenthealth -= 2;
            SetHealth(currenthealth - 2);
            timeWhenWeNextDoSomething = Time.time + timeBetweenDoingSomething;
        }
    }
    

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
        if (rot)
            fill.color = rot_gradient.Evaluate(1f);
        else
		    fill.color = gradient.Evaluate(1f);
        
	}

    public void SetHealth(int health)
	{
		if (health < maxhealth)
        {
            slider.value = health;
        
	        currenthealth = Mathf.CeilToInt(slider.value);

            if (rot)
                fill.color = gradient.Evaluate(slider.normalizedValue);
            else
                fill.color = gradient.Evaluate(slider.normalizedValue);
        }     
            
            
	}

}
