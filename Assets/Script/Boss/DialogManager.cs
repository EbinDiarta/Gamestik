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

    [Header("Typewriter Settings")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Dialog Data")]
    [SerializeField] private DialogLine[] dialogLines;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isDialogActive = false;
    private Coroutine typingCoroutine;

    public System.Action OnDialogCompleted;

    private void Awake()
    {
        // Pastikan Panel & Teks Skip tersembunyi di awal game tanpa mematikan Canvas utamanya
        if (dialogPanel != null) dialogPanel.SetActive(false);
        if (skipInstructionUI != null) skipInstructionUI.SetActive(false);
    }

    private void Update()
    {
        if (!isDialogActive) return;

        // Press 'E' to Skip Entire Dialog
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndDialog();
            return;
        }

        // Click Screen / Left Mouse Click
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
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
        isDialogActive = true;
        currentLineIndex = 0;

        // Aktifkan elemen UI di dalam Canvas
        if (dialogPanel != null) dialogPanel.SetActive(true);
        if (skipInstructionUI != null) skipInstructionUI.SetActive(true);

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        DialogLine currentLine = dialogLines[currentLineIndex];
        nameText.text = currentLine.speakerName;

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        sentenceText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void EndDialog()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        isDialogActive = false;
        
        if (dialogPanel != null) dialogPanel.SetActive(false);
        if (skipInstructionUI != null) skipInstructionUI.SetActive(false);

        OnDialogCompleted?.Invoke();
    }
}