using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    [SerializeField] private DragAndDropTrash.TrashType acceptedType;
    [SerializeField] private TrashSortingMiniGame miniGameManager;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragAndDropTrash draggedTrash = eventData.pointerDrag.GetComponent<DragAndDropTrash>();

            if (draggedTrash != null)
            {
                bool isCorrect = (draggedTrash.Type == acceptedType);
                miniGameManager.OnTrashSorted(draggedTrash, isCorrect);
            }
        }
    }
}