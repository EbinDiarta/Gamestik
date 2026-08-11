using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropTrash : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum TrashType
    {
        Organic,
        Anorganic,
        B3
    }

    [Header("Identitas Sampah")]
    [SerializeField] private TrashType trashType;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    public TrashType Type => trashType;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        ResetPosition();
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas != null ? canvas.scaleFactor : 1f;
    }
}