using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkLevel : MonoBehaviour
{
    [Header("Milk Level")]
    [SerializeField] private float maxMilk;
    [SerializeField] private float currentMilk;
    [SerializeField] private float damageCoolDown;
    private float damageTime;

    [Header("Spoilage")]
    [SerializeField] private float maxSpoilage;
    [SerializeField] private float freshenRate;
    [SerializeField] private float spoilDamage;
    [SerializeField] private float minSpoilRate;
    [SerializeField] private float maxSpoilRate;
    [SerializeField] private float minSpoilTemp;
    [SerializeField] private float maxSpoilTemp;
    private float currentSpoilage;
    private float currentTemperature;

    public float MaxMilk { get { return maxMilk; } }
    public float MaxSpoilage { get { return maxSpoilage; } }
    public float MinSpoilTemp { get { return minSpoilTemp; } }
    public float MaxSpoilTemp { get { return maxSpoilTemp; } }
    public float CurrentMilk { get { return currentMilk; } }
    public float CurrentSpoilage { get { return currentSpoilage; } }
    public float CurrentTemperature { get { return currentTemperature; } set { currentTemperature = value; } }

    private void Awake()
    {
        currentMilk = maxMilk;
    }

    private void Update()
    {
        damageTime += Time.deltaTime;
        SpoilMilk();
    }

    public void UpdateMilkContents(float amount)
    {
        if ((amount < 0 && damageTime >= damageCoolDown) || amount >= 0)
        {
            currentMilk = Mathf.Clamp(currentMilk + amount, 0, maxMilk);
            if (amount < 0)
                damageTime = 0;
        }
    }

    private void SpoilMilk()
    {
        float rate = 0;

        if (currentTemperature < minSpoilTemp)
            rate = freshenRate;
        else if (currentTemperature >= maxSpoilTemp)
            rate = maxSpoilRate;
        else
        {
            float tempPercent = (currentTemperature - minSpoilTemp) / (maxSpoilTemp - minSpoilTemp);
            rate = minSpoilRate + (tempPercent * (maxSpoilRate - minSpoilRate));
        }

        currentSpoilage = Mathf.Clamp(currentSpoilage + rate * Time.deltaTime, 0, maxSpoilage);

        if (currentSpoilage >= maxSpoilage)
            UpdateMilkContents(-spoilDamage * Time.deltaTime);
    }
}
