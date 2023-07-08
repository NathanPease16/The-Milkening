using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    private GameObject selection;

    private void Awake()
    {
        selection = transform.parent.Find("Selection").gameObject;
        selection.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selection.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selection.SetActive(false);
    }
}
