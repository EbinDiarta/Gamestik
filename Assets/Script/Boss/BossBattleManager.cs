using UnityEngine;
using UnityEngine.UI;

public class BossBattleManager : MonoBehaviour
{
    [Header("Player & Checkpoint Setup")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform checkpointPosition;

    [Header("UI Health")]
    [SerializeField] private GameObject[] playerHearts; 
    [SerializeField] private Image bossHealthBar;       
    [SerializeField] private TrashSortingMiniGame miniGameManager;

    [Header("Dialog Post-Hit")]
    [SerializeField] private DialogManager dialogManager;

    private int playerHealth = 3;
    private float bossHealth = 1.0f; 
    private int currentRound = 1;
    private const int maxRounds = 4;

    private void Start()
    {
        ResetBattleState();
    }

    public void StartRoundMiniGame()
    {
        FreezePlayer(true);
        miniGameManager.StartMiniGame();
    }

    public void TakePlayerDamage()
    {
        playerHealth--;

        if (playerHealth >= 0 && playerHealth < playerHearts.Length)
        {
            playerHearts[playerHealth].SetActive(false);
        }

        if (playerHealth <= 0)
        {
            HandleGameOver();
        }
    }

    public void OnRoundCleared()
    {
        bossHealth -= 0.25f;
        bossHealth = Mathf.Clamp01(bossHealth);

        if (bossHealthBar != null)
        {
            bossHealthBar.fillAmount = bossHealth;
        }

        if (dialogManager != null)
        {
            dialogManager.OnDialogCompleted += OnPostHitDialogCompleted;
            dialogManager.StartDialog();
        }
        else
        {
            OnPostHitDialogCompleted();
        }
    }

    private void OnPostHitDialogCompleted()
    {
        if (dialogManager != null)
        {
            dialogManager.OnDialogCompleted -= OnPostHitDialogCompleted;
        }

        currentRound++;

        if (currentRound > maxRounds)
        {
            FreezePlayer(false);
            Debug.Log("Pase 1 Selesai! Boss melarikan diri.");
        }
        else
        {
            FreezePlayer(false);
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over! Mereset posisi ke checkpoint ronde 1...");

        if (playerController != null && checkpointPosition != null)
        {
            playerController.transform.position = checkpointPosition.position;
        }

        ResetBattleState();
        FreezePlayer(false);
    }

    private void ResetBattleState()
    {
        playerHealth = 3;
        bossHealth = 1.0f;
        currentRound = 1;

        if (bossHealthBar != null)
        {
            bossHealthBar.fillAmount = bossHealth;
        }

        foreach (GameObject heart in playerHearts)
        {
            if (heart != null) heart.SetActive(true);
        }
    }

    private void FreezePlayer(bool freeze)
    {
        if (playerController != null)
        {
            playerController.enabled = !freeze;

            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            Animator anim = playerController.GetComponent<Animator>();
            if (anim != null) anim.SetBool("IsRun", false);
        }
    }
}