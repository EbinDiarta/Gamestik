using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragTrash : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Trash.TrashType trashType;
    public Trash currentTrash; // Variabel penampung referensi objek Trash

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    // Dipanggil saat spawn untuk menyimpan posisi asal
    public void ResetPosition()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.position;
    }

    // Mengembalikan fisik UI secara instan ke koordinat awal
    public void ReturnToStartPosition()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        rectTransform.position = startPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;

        if (transform.parent == originalParent)
        {
            rectTransform.position = startPosition;
        }
    }
}