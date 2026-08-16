using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossCutsceneManager : MonoBehaviour
{
    [Header("UI Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float holdBlackDuration = 0.5f;

    [Header("UI Dialog Setup")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Player & Boss Setup")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Transform teleportTargetPoint;
    [SerializeField] private GameObject bossObject;

    [Header("Next Scene Settings")]
    [SerializeField] private string nextSceneName = "stlhbanjir";

    private bool isTriggered = false;
    private bool isDialogueActive = false;
    private int currentDialogIndex = 0;

    private readonly string[] bossDialogs = new string[]
    {
        "Trash Monster : Akhirnya kamu datang, Raka.",
        "Raka : Kamu siapa?",
        "Trash Monster : Panggil saja aku... Trash Monster.",
        "Raka : Jadi kamu yang menyebabkan semua sampah ini terus muncul?",
        "Trash Monster : Salah. Aku hanya memberikan apa yang kalian ciptakan.",
        "Raka : Apa maksudmu?",
        "Trash Monster : Sampah yang dibuang sembarangan. Sungai yang dicemari. Lingkungan yang kalian abaikan.",
        "Trash Monster : Semua itu adalah sumber kekuatanku.",
        "Raka : Kalau begitu, aku akan menghentikanmu.",
        "Trash Monster : Kamu pikir membersihkan beberapa sampah bisa mengalahkanku?",
        "Raka : Aku tidak akan menyerah.",
        "Trash Monster : Kalau begitu, buktikan!",
        "Raka : Aku akan mengalahkanmu!",
        "Trash Monster : COBA SAJA!"
    };

    private void Start()
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Menekan tombol Space untuk melanjut dialog
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialog();
        }
    }

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
        // 1. Hentikan pergerakan & animasi Player jika ada
        if (playerObject != null)
        {
            MonoBehaviour playerScript = playerObject.GetComponent<MonoBehaviour>();
            if (playerScript != null) playerScript.enabled = false;

            Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            Animator anim = playerObject.GetComponent<Animator>();
            if (anim != null) anim.SetBool("IsRun", false);
        }

        yield return StartCoroutine(Fade(0f, 1f));

        if (playerObject != null && teleportTargetPoint != null)
        {
            playerObject.transform.position = teleportTargetPoint.position;
        }

        if (bossObject != null)
        {
            bossObject.SetActive(true);
        }

        yield return new WaitForSeconds(holdBlackDuration);

        yield return StartCoroutine(Fade(1f, 0f));

        StartDialog();
    }

    private void StartDialog()
    {
        currentDialogIndex = 0;
        isDialogueActive = true;

        if (dialogPanel != null) dialogPanel.SetActive(true);
        if (dialogText != null) dialogText.text = bossDialogs[currentDialogIndex];
    }

    private void AdvanceDialog()
    {
        currentDialogIndex++;

        if (currentDialogIndex < bossDialogs.Length)
        {
            dialogText.text = bossDialogs[currentDialogIndex];
        }
        else
        {
            StartCoroutine(EndDialogAndChangeScene());
        }
    }

    private IEnumerator EndDialogAndChangeScene()
    {
        isDialogueActive = false;

        if (dialogPanel != null) dialogPanel.SetActive(false);

        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(holdBlackDuration);

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        if (fadeCanvasGroup == null) yield break;

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