using UnityEngine;
using UnityEngine.EventSystems;

public class BossBinDropZone : MonoBehaviour, IDropHandler
{
    public Trash.TrashType binType;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DragTrash dragTrash = eventData.pointerDrag.GetComponent<DragTrash>();
        if (dragTrash == null) return;

        // 1. Cek jika BossQuizManager aktif (Scene Boss)
        if (BossQuizManager.instance != null && BossQuizManager.instance.isQuizActive)
        {
            BossQuizManager.instance.CheckDrop(dragTrash, binType);
        }
        // 2. Jika bukan Scene Boss, gunakan QuizManager standar
        else if (QuizManager.instance != null)
        {
            QuizManager.instance.CheckDrop(dragTrash, binType);
        }
    }
}