using UnityEngine;

public class BossTrashOverworld : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && BossQuizManager.instance != null)
            {
                isTriggered = true;
                BossQuizManager.instance.OpenBossQuiz(player, gameObject);                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}