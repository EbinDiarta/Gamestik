using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 
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
    [SerializeField] private DialogManager dialogManager; 

    [Header("Next Scene Settings")]
    [SerializeField] //private string nextSceneName = "stlhbanjir"; 

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
        if (playerController != null)
        {
            playerController.enabled = false;

            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            Animator anim = playerController.GetComponent<Animator>();
            if (anim != null) anim.SetBool("IsRun", false);
        }

        yield return StartCoroutine(Fade(0f, 1f));

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

        yield return StartCoroutine(Fade(1f, 0f));

        if (dialogManager != null)
        {
            dialogManager.OnDialogCompleted += OnDialogEnd;
            dialogManager.StartDialog();
        }
        else
        {
            OnDialogEnd();
        }
    }

    private void OnDialogEnd()
    {
        if (dialogManager != null)
        {
            dialogManager.OnDialogCompleted -= OnDialogEnd;
        }

        SceneManager.LoadScene(SceneData.stlhbanjir);
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