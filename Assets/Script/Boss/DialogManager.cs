using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct DialogLine
{
    public string speakerName;

    [TextArea(2, 5)]
    public string sentence;
}

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text sentenceText;
    [SerializeField] private GameObject skipInstructionUI;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float blackScreenDuration = 0.2f;

    [Header("Typewriter Settings")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Dialog Data")]
    [SerializeField] private DialogLine[] dialogLines;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isDialogActive = false;
    private bool isEndingDialog = false;
    private Coroutine typingCoroutine;

    public System.Action OnDialogCompleted;

    private void Awake()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        if (skipInstructionUI != null)
            skipInstructionUI.SetActive(false);

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.interactable = false;
            fadeCanvasGroup.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        if (!isDialogActive || isEndingDialog)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndDialog();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);

                sentenceText.text = dialogLines[currentLineIndex].sentence;
                isTyping = false;
            }
            else
            {
                currentLineIndex++;

                if (currentLineIndex < dialogLines.Length)
                {
                    DisplayNextSentence();
                }
                else
                {
                    EndDialog();
                }
            }
        }
    }

    public void StartDialog()
    {
        if (isDialogActive || isEndingDialog)
            return;

        isDialogActive = true;
        currentLineIndex = 0;

        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        if (skipInstructionUI != null)
            skipInstructionUI.SetActive(true);

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (currentLineIndex < 0 || currentLineIndex >= dialogLines.Length)
            return;

        DialogLine currentLine = dialogLines[currentLineIndex];

        if (nameText != null)
            nameText.text = currentLine.speakerName;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        if (sentenceText != null)
            sentenceText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (sentenceText != null)
                sentenceText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void EndDialog()
    {
        if (isEndingDialog)
            return;

        isEndingDialog = true;
        isDialogActive = false;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;

        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        if (skipInstructionUI != null)
            skipInstructionUI.SetActive(false);

        StartCoroutine(FadeAfterDialog());
    }

    private IEnumerator FadeAfterDialog()
    {
        yield return StartCoroutine(Fade(1f));

        yield return new WaitForSeconds(blackScreenDuration);

        OnDialogCompleted?.Invoke();

        yield return StartCoroutine(Fade(0f));

        isEndingDialog = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup == null)
            yield break;

        float startAlpha = fadeCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / fadeDuration;

            fadeCanvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                progress
            );

            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}