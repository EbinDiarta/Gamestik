using System.Collections;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    [Header("UI Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float holdBlackDuration = 0.5f;

    [Header("Player Setup")]
    [SerializeField] private PlayerController playerController;

    [Header("Positioning Player & Boss")]
    [SerializeField] private bool useTeleport = true;
    [SerializeField] private Vector2 playerBossPosition = new Vector2(0f, 0f);

    [Header("Boss & Dialog Setup")]
    [SerializeField] private GameObject bossObject;
    [SerializeField] private DialogManager dialogManager; // Referensi ke DialogManager

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(StartBossCutsceneRoutine());
        }
    }

    private IEnumerator StartBossCutsceneRoutine()
    {
        // 1. Kunci Player & Set Animasi ke Idle
        if (playerController != null)
        {
            playerController.enabled = false;

            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            Animator anim = playerController.GetComponent<Animator>();
            if (anim != null) anim.SetBool("IsRun", false);
        }

        // 2. Fade Out (Hitam)
        yield return StartCoroutine(Fade(0f, 1f));

        // 3. Teleport Player & Aktifkan Boss
        if (useTeleport && playerController != null)
        {
            playerController.transform.position = new Vector3(
                playerBossPosition.x, 
                playerBossPosition.y, 
                playerController.transform.position.z
            );
        }

        if (bossObject != null) bossObject.SetActive(true);

        yield return new WaitForSeconds(holdBlackDuration);

        // 4. Fade In (Terang)
        yield return StartCoroutine(Fade(1f, 0f));

        // 5. Mulai Dialog & Kunci kontrol sampai dialog selesai
        if (dialogManager != null)
        {
            // Subscribe event saat dialog selesai
            dialogManager.OnDialogCompleted += EnablePlayerControl;
            dialogManager.StartDialog();
        }
        else
        {
            // Jika tidak ada dialog manager, langsung lepas kontrol player
            EnablePlayerControl();
        }
    }

    private void EnablePlayerControl()
    {
        if (dialogManager != null)
        {
            dialogManager.OnDialogCompleted -= EnablePlayerControl;
        }

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }
}