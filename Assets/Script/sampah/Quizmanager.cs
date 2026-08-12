using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    [Header("UI")]
    public GameObject quizPanel;
    public GameObject dragTrashPrefab;
    public Transform trashContainer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Quiz Trash")]
    public GameObject[] quizTrashPrefabs;
    public int jumlahSampahQuiz = 10;

    [Header("Timer")]
    public float maxTime = 5f;

    public int score = 0;

    private float currentTime;
    private bool isTiming;
    private bool quizAktif;

    private Trash touchedTrash;

    private List<DragTrash> currentTrashList =
        new List<DragTrash>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        quizPanel.SetActive(false);

        score = 0;

        UpdateScore();
        UpdateTimer();
    }

    private void Update()
    {
        if (!isTiming)
            return;

        currentTime -= Time.unscaledDeltaTime;

        UpdateTimer();

        if (currentTime <= 0)
        {
            currentTime = 0;
            TimeUp();
        }
    }

    public void OpenQuiz(Trash touchedTrash)
    {
        if (quizAktif)
            return;
        Time.timeScale = 0;

        if (quizTrashPrefabs == null ||
            quizTrashPrefabs.Length < jumlahSampahQuiz)
        {
            Debug.LogError(
                "Quiz Trash Prefabs harus memiliki minimal "
                + jumlahSampahQuiz
                + " prefab."
            );

            return;
        }

        this.touchedTrash = touchedTrash;

        ClearQuizTrash();

        currentTrashList.Clear();

        List<GameObject> randomPrefabs =
            new List<GameObject>(quizTrashPrefabs);

        for (int i = 0; i < randomPrefabs.Count; i++)
        {
            int randomIndex =
                Random.Range(i, randomPrefabs.Count);

            GameObject temp =
                randomPrefabs[i];

            randomPrefabs[i] =
                randomPrefabs[randomIndex];

            randomPrefabs[randomIndex] =
                temp;
        }

        for (int i = 0; i < jumlahSampahQuiz; i++)
        {
            CreateQuizTrash(
                randomPrefabs[i]
            );
        }

        if (touchedTrash != null)
        {
            touchedTrash.gameObject.SetActive(false);
        }

        quizPanel.SetActive(true);

        quizAktif = true;

        StartTimer();

        UpdateScore();
    }

    private void CreateQuizTrash(GameObject prefab)
    {
        GameObject obj = Instantiate(
            dragTrashPrefab,
            trashContainer
        );

        DragTrash drag =
            obj.GetComponent<DragTrash>();

        if (drag == null)
        {
            Debug.LogError(
                "DragTrash tidak ditemukan pada Drag Trash Prefab."
            );

            Destroy(obj);
            return;
        }

        Trash trash =
            prefab.GetComponent<Trash>();

        if (trash == null)
        {
            Debug.LogError(
                "Trash.cs tidak ditemukan pada prefab "
                + prefab.name
            );

            Destroy(obj);
            return;
        }

        drag.currentTrash = null;
        drag.trashType = trash.jenisSampah;

        SpriteRenderer spriteRenderer =
            prefab.GetComponent<SpriteRenderer>();

        Image image =
            obj.GetComponent<Image>();

        if (image == null)
        {
            image =
                obj.GetComponentInChildren<Image>();
        }

        if (spriteRenderer != null &&
            image != null)
        {
            image.sprite =
                spriteRenderer.sprite;

            image.preserveAspect = true;
        }

        currentTrashList.Add(drag);
    }

    private void StartTimer()
    {
        currentTime = maxTime;
        isTiming = true;

        UpdateTimer();
    }

    private void StopTimer()
    {
        isTiming = false;
    }

    private void UpdateTimer()
    {
        if (timerText != null)
        {
            timerText.text =
                "Waktu: " +
                Mathf.CeilToInt(currentTime);
        }
    }

    private void TimeUp()
{
    StopTimer();

    Time.timeScale = 1;

    quizAktif = false;

    ClearQuizTrash();

    quizPanel.SetActive(false);

    if (touchedTrash != null)
    {
        touchedTrash.gameObject.SetActive(true);
    }

    touchedTrash = null;

    UpdateTimer();

    Debug.Log("Waktu habis! Keluar dari quiz.");
}

    public void CheckDrop(
        DragTrash dragTrash,
        Trash.TrashType selectedBin
    )
    {
        if (dragTrash == null)
            return;

        if (!quizAktif)
            return;

        if (dragTrash.trashType == selectedBin)
        {
            score += 2;

            UpdateScore();

            currentTrashList.Remove(dragTrash);

            Destroy(dragTrash.gameObject);

            if (currentTrashList.Count == 0)
            {
                FinishQuiz();
            }
        }
        else
        {
            dragTrash.ResetPosition();
        }
    }

    private void ClearQuizTrash()
    {
        foreach (DragTrash drag in currentTrashList)
        {
            if (drag != null)
            {
                Destroy(drag.gameObject);
            }
        }

        currentTrashList.Clear();

        if (trashContainer != null)
        {
            for (
                int i = trashContainer.childCount - 1;
                i >= 0;
                i--
            )
            {
                Destroy(
                    trashContainer.GetChild(i).gameObject
                );
            }
        }
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Poin : " + score;
        }
    }

    private void FinishQuiz()
    {
        StopTimer();
        
        Time.timeScale = 1;

        quizAktif = false;

        if (touchedTrash != null)
        {
            Destroy(touchedTrash.gameObject);
        }

        touchedTrash = null;

        quizPanel.SetActive(false);

        CheckRemainingTrash();
    }

    private void CheckRemainingTrash()
    {
        Trash[] remainingTrash =
            FindObjectsOfType<Trash>();
        if (score == 100)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        StopTimer();

        quizAktif = false;

        quizPanel.SetActive(false);

        Debug.Log("GAME SELESAI");
        Debug.Log("SKOR AKHIR : " + score);

        SceneManager.LoadScene(
            SceneData.stlhbanjir
        );
    }
}