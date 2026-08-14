using UnityEngine;

public class SampahMinigameTrigger : MonoBehaviour
{
    [Header("Minigame Setup")]
    [SerializeField] private GameObject minigameUIPanel; 
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            TriggerMinigame(other.gameObject);
        }
    }

    private void TriggerMinigame(GameObject playerObj)
    {
        PlayerController playerController = playerObj.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Rigidbody2D rb = playerObj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        Animator anim = playerObj.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("IsRun", false); 
        }

        // 2. Buka UI Minigame
        if (minigameUIPanel != null)
        {
            minigameUIPanel.SetActive(true);

            MinigameUIController uiController = minigameUIPanel.GetComponent<MinigameUIController>();
            if (uiController != null)
            {
                uiController.SetupMinigame(playerController, this.gameObject);
            }
        }
    }
}