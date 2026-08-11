using UnityEngine;
using UnityEngine.UI;

public class TrashSortingMiniGame : MonoBehaviour
{
    [Header("Referensi Manager Utama")]
    [SerializeField] private BossBattleManager bossBattleManager;

    [Header("UI Mini Game Panel")]
    [SerializeField] private GameObject miniGamePanel;
    [SerializeField] private GameObject[] trashItems; 

    private int sortedTrashCount = 0;
    private int totalTrashInRound = 5;

    public void StartMiniGame()
    {
        sortedTrashCount = 0;
        miniGamePanel.SetActive(true);

        foreach (GameObject trash in trashItems)
        {
            if (trash != null)
            {
                trash.SetActive(true);
                DragAndDropTrash dragScript = trash.GetComponent<DragAndDropTrash>();
                if (dragScript != null)
                {
                    dragScript.ResetPosition();
                }
            }
        }
    }

    public void OnTrashSorted(DragAndDropTrash trash, bool isCorrect)
    {
        if (isCorrect)
        {
            trash.gameObject.SetActive(false); 
            sortedTrashCount++;

            if (sortedTrashCount >= totalTrashInRound)
            {
                CompleteRound();
            }
        }
        else
        {
            trash.ResetPosition(); 
            bossBattleManager.TakePlayerDamage();
        }
    }

    private void CompleteRound()
    {
        miniGamePanel.SetActive(false);
        bossBattleManager.OnRoundCleared();
    }
}