using UnityEngine;
using UnityEngine.EventSystems;

public enum TrashCategory
{
    Organik,
    Anorganik,
    B3_DaurUlang
}

public class TrashDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Trash Category")]
    public TrashCategory category;

    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private BossBattlePhase1 battleManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void SetupBattleManager(BossBattlePhase1 manager)
    {
        battleManager = manager;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (battleManager != null && !battleManager.isBattleActive) return;

        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false; // Membiarkan raycast menembus ke Tong Sampah di bawahnya
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (battleManager != null && !battleManager.isBattleActive) return;

        rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (battleManager != null && !battleManager.isBattleActive) return;

        canvasGroup.blocksRaycasts = true;

        // Cek apakah di-drop di atas Tong Sampah (TrashBin)
        GameObject droppedTarget = eventData.pointerCurrentRaycast.gameObject;

        if (droppedTarget != null)
        {
            TrashBin bin = droppedTarget.GetComponent<TrashBin>();
            if (bin != null)
            {
                if (bin.binCategory == this.category)
                {
                    // Benar
                    battleManager.OnTrashSortedCorrectly();
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    // Salah
                    battleManager.OnTrashSortedWrongly();
                    Destroy(gameObject);
                    return;
                }
            }
        }

        // Jika dilepas di sembarang tempat, kembalikan ke posisi awal
        rectTransform.anchoredPosition = originalPosition;
    }

    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas != null ? canvas.scaleFactor : 1f;
    }
}
