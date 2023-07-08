using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MilkUI : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private float milkRate;
    [SerializeField] private float heatRate;
    [SerializeField] private float spoilageRate;

    [Header("Milk Level")]
    private float currentMilkPercent;
    private RectTransform milkMask;
    private RectTransform milk;

    [Header("Heat Level")]
    [SerializeField] private Gradient heatGradient;
    private float currentHeatPercent;
    private float activeHeatPercent;
    private Image heatIcon;

    [Header("Spoilage Level")]
    [SerializeField] private Gradient spoilageGradient;
    private float currentSpoilagePercent;
    private float activeSpoilagePercent;
    private Image spoilageIcon;

    [Header("References")]
    private MilkLevel milkLevel;

    private void Awake()
    {
        milkLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<MilkLevel>();

        milkMask = transform.Find("MilkBottle").Find("MilkMask").GetComponent<RectTransform>();
        milk = milkMask.Find("Milk").GetComponent<RectTransform>();

        heatIcon = transform.Find("Heat").GetComponent<Image>();

        spoilageIcon = transform.Find("Spoilage").GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(DisplayMilkLevel());
    }

    private void Update()
    {
        FindPercentages();
    }

    private void FindPercentages()
    {
        currentMilkPercent = milkLevel.CurrentMilk / milkLevel.MaxMilk;
        currentHeatPercent = milkLevel.CurrentTemperature / milkLevel.MaxSpoilTemp;
        currentSpoilagePercent = milkLevel.CurrentSpoilage / milkLevel.MaxSpoilage;
    }

    private bool HasPassed(float current, float goal, int dir)
    {
        return (current < goal && dir < 0) || (current > goal && dir > 0);
    }

    private IEnumerator DisplayMilkLevel()
    {
        while (true)
        {
            int milkDir = (int)Mathf.Sign(currentMilkPercent - milkMask.localScale.y);

            if (milkMask.localScale.y != currentMilkPercent)
            {
                float scale = milkMask.localScale.y + milkDir * milkRate * Time.deltaTime;
                milkMask.localScale = new Vector3(1, scale, 1);
                milk.localScale = new Vector3(1, 1f/scale, 1);
            }

            if (HasPassed(milkMask.localScale.y, currentMilkPercent, milkDir))
            {
                milkMask.localScale = new Vector3(1, currentMilkPercent, 1);
                milk.localScale = currentMilkPercent == 0 ? Vector3.zero : new Vector3(1, 1f/currentMilkPercent, 1);
            }

            int heatDir = (int)Mathf.Sign(currentHeatPercent - activeHeatPercent);

            if (activeHeatPercent != currentHeatPercent)
                activeHeatPercent += heatDir * heatRate * Time.deltaTime;

            if (HasPassed(activeHeatPercent, currentHeatPercent, heatDir))
                activeHeatPercent = currentHeatPercent;

            heatIcon.color = heatGradient.Evaluate(activeHeatPercent);

            int spoilageDir = (int)Mathf.Sign(currentSpoilagePercent - activeSpoilagePercent);

            if (activeSpoilagePercent != currentSpoilagePercent)
                activeSpoilagePercent += spoilageDir * spoilageRate * Time.deltaTime;

            if (HasPassed(activeSpoilagePercent, currentSpoilagePercent, spoilageDir))
                activeSpoilagePercent = currentSpoilagePercent;
            
            spoilageIcon.color = spoilageGradient.Evaluate(activeSpoilagePercent);

            yield return null;
        }
    }
}
