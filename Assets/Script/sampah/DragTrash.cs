using UnityEngine;
using UnityEngine.EventSystems;

public class DragTrash : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    public Trash.TrashType trashType;
    public Trash currentTrash;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Method khusus untuk mengunci posisi awal dari Manager
    public void SetStartPosition(Vector3 newPosition)
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        
        rectTransform.position = newPosition;
        startPosition = newPosition; // Kunci koordinat spawn yang benar!
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void ResetPosition()
    {
        rectTransform.position = startPosition;
    }
}