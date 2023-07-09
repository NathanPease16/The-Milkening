using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Attributes")]
    [SerializeField] private float time;
    private float startHeight;

    [Header("References")]
    private GameObject selection;
    private RectTransform mask;
    private RectTransform milk;

    private void Awake()
    {
        selection = transform.parent.Find("Selection").gameObject;
        selection.SetActive(false);

        mask = transform.parent.Find("Mask").GetComponent<RectTransform>();
        milk = mask.Find("Image").GetComponent<RectTransform>();

        startHeight = mask.rect.height;
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        StartCoroutine(Fill(-1, 0.00000001f));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selection.SetActive(true);
        StartCoroutine(Fill(1, time));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selection.SetActive(false);
        StartCoroutine(Fill(-1, time));
    }

    private IEnumerator Fill(int dir, float a)
    {
        if (dir == 1)
        {
            for (float t = 0; t <= 1; t += Time.deltaTime/a)
            {
                mask.sizeDelta = new Vector2(mask.rect.width, startHeight * t);
                yield return null;
            }

            mask.sizeDelta = new Vector2(mask.rect.width, startHeight);
        }
        else
        {
            for (float t = 1; t >= 0; t -= Time.deltaTime/a)
            {
                mask.sizeDelta = new Vector2(mask.rect.width, startHeight * t);
                yield return null;
            }

            mask.sizeDelta = new Vector2(mask.rect.width, 0);
        }
    }
}
