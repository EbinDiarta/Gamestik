using UnityEngine;

public class OverworldTrash : MonoBehaviour
{
    [SerializeField] private BossBattleManager bossBattleManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossBattleManager != null)
            {
                bossBattleManager.StartRoundMiniGame();
            }

            gameObject.SetActive(false);
        }
    }
}