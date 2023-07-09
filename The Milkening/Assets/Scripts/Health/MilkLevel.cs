using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MilkLevel : MonoBehaviour
{

    [Header ("Sounds")]
    public AudioClip _clip;

    [Header("Milk Level")]
    [SerializeField] private float maxMilk;
    [SerializeField] private float currentMilk;
    [SerializeField] private float damageCoolDown;
    [SerializeField] private float timeToRestart;
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


    GameObject death;

    Animator animator;
    MilkMovement movement;
    private CameraMovement camMove;
    bool Died;
    private void Awake()
    {
        
        currentMilk = maxMilk;
        death = GameObject.FindGameObjectWithTag("Death");
        death.SetActive(false);
        animator = GetComponent<Animator>();
        movement = transform.GetChild(0).GetComponent<MilkMovement>();

        camMove = GetComponent<CameraMovement>();
    }

    private void Update()
    {
        damageTime += Time.deltaTime;
        SpoilMilk();
        if (currentMilk <= 0 && !Died)    
            Death();
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
    
    public void Death()
    {
        Died = true;
        SoundManager.instance.PlaySound(_clip);
        death.SetActive(true);
        movement.Launch();
        StartCoroutine(Restart());
        camMove.enabled = false;
        movement.enabled = false;

    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(timeToRestart);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
