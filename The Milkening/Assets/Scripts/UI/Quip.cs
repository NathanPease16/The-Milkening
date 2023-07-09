using UnityEngine;
using TMPro;

public class Quip : MonoBehaviour
{
    [Header("Quips")]
    [SerializeField] private string[] quips;

    [Header("References")]
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "\"" + quips[Random.Range(0, quips.Length)] + "\"";
    }
}
