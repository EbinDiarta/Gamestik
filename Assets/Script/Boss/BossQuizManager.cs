using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossQuizManager : MonoBehaviour
{
    public static BossQuizManager instance;

    [Header("UI References")]
    public GameObject quizPanel;
    public GameObject dragTrashPrefab; // Prefab UI DragTrash
    public Transform trashContainer;  // Container UI penampung sampah (misi)
    public TextMeshProUGUI timerText;

    [Header("Boss Quiz Settings")]
    public GameObject[] quizTrashPrefabs; // Array prefab data sampah
    public Transform[] spawnPointsUI;     // Array 19 Point Transform UI (spawn 0 s/d 18)
    public int jumlahSampahQuiz = 5;      // Jumlah sampah UI yang dimunculkan
    public float maxTime = 30f;           // Durasi timer minigame

    [HideInInspector] public bool isQuizActive = false;

    private float currentTime;
    private bool isTiming;
    private PlayerController playerController;
    private GameObject touchedWorldTrash;

    private List<DragTrash> currentTrashList = new List<DragTrash>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (quizPanel != null) quizPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isTiming) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0)
        {
            currentTime = 0;
            TimeUp();
        }
    }

    public void OpenBossQuiz(PlayerController player, GameObject worldTrash)
    {
        if (isQuizActive) return;

        if (spawnPointsUI == null || spawnPointsUI.Length == 0)
        {
            Debug.LogError("SpawnPointsUI belum diisi di Inspector!");
            return;
        }

        playerController = player;
        touchedWorldTrash = worldTrash;

        // 1. Matikan gerak Player
        if (playerController != null)
        {
            playerController.enabled = false;

            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            Animator anim = playerController.GetComponent<Animator>();
            if (anim != null) anim.SetBool("IsRun", false);
        }

        ClearQuizTrash();

        // 2. Aktifkan Panel UI Minigame
        if (quizPanel != null) quizPanel.SetActive(true);

        // Force Canvas Update agar skala UI siap
        Canvas.ForceUpdateCanvases();

        // 3. Acak Spawn Points UI tanpa duplikat
        List<Transform> availableSpawns = new List<Transform>(spawnPointsUI);
        int spawnAmount = Mathf.Min(jumlahSampahQuiz, availableSpawns.Count);

        for (int i = 0; i < spawnAmount; i++)
        {
            int randomSpawnIndex = Random.Range(0, availableSpawns.Count);
            GameObject randomTrashPrefab = quizTrashPrefabs[Random.Range(0, quizTrashPrefabs.Length)];

            CreateQuizTrashAtSpawn(randomTrashPrefab, availableSpawns[randomSpawnIndex]);
            availableSpawns.RemoveAt(randomSpawnIndex);
        }

        isQuizActive = true;
        StartTimer();
    }

    private void CreateQuizTrashAtSpawn(GameObject prefab, Transform spawnTarget)
    {
        if (prefab == null || spawnTarget == null || dragTrashPrefab == null) return;

        // 1. Instantiate prefab ke trashContainer
        GameObject obj = Instantiate(dragTrashPrefab, trashContainer);
        RectTransform rect = obj.GetComponent<RectTransform>();
        DragTrash drag = obj.GetComponent<DragTrash>();
        Trash trash = prefab.GetComponent<Trash>();

        if (rect == null || drag == null || trash == null)
        {
            Destroy(obj);
            return;
        }

        // 2. TENTUKAN BATAS LAYAR CANVAS (Screen / Canvas Resolution)
        // Set Anchor & Pivot ke Center (0.5, 0.5)
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.localScale = Vector3.one;

        // 3. AMBIL POSISI DARI SPAWN TARGET & BATASI (CLAMP) DIDALAM LAYAR
        Vector3 targetWorldPos = spawnTarget.position;
        rect.position = targetWorldPos;

        // Kunci koordinat lokal agar tidak terlempar melampaui resolusi 1920x1080 (Batas X: -800s/d 800, Batas Y: -400 s/d 400)
        Vector2 clampedLocalPos = rect.localPosition;
        clampedLocalPos.x = Mathf.Clamp(clampedLocalPos.x, -750f, 750f);
        clampedLocalPos.y = Mathf.Clamp(clampedLocalPos.y, -350f, 350f);
        
        rect.localPosition = new Vector3(clampedLocalPos.x, clampedLocalPos.y, 0f);

        // 4. KUNCI POSISI UNTUK SISTEM DRAG
        drag.SetStartPosition(rect.position);

        // 5. ATUR DATA TIPE SAMPAH DAN SPRITE IMAGE
        drag.trashType = trash.jenisSampah;

        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        Image image = obj.GetComponent<Image>() ?? obj.GetComponentInChildren<Image>();

        if (spriteRenderer != null && image != null)
        {
            image.sprite = spriteRenderer.sprite;
            image.preserveAspect = true;
        }

        currentTrashList.Add(drag);
    }

    public void CheckDrop(DragTrash dragTrash, Trash.TrashType selectedBin)
    {
        if (dragTrash == null || !isQuizActive) return;

        if (dragTrash.trashType == selectedBin)
        {
            currentTrashList.Remove(dragTrash);
            Destroy(dragTrash.gameObject);

            if (currentTrashList.Count == 0)
            {
                FinishBossQuiz();
            }
        }
        else
        {
            dragTrash.ResetPosition();
        }
    }

    private void StartTimer()
    {
        currentTime = maxTime;
        isTiming = true;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Waktu: " + Mathf.CeilToInt(currentTime);
        }
    }

    private void TimeUp()
    {
        CloseQuizUI();
    }

    private void FinishBossQuiz()
    {
        if (touchedWorldTrash != null)
        {
            Destroy(touchedWorldTrash);
        }

        CloseQuizUI();
    }

    private void CloseQuizUI()
    {
        isTiming = false;
        isQuizActive = false;

        ClearQuizTrash();

        if (quizPanel != null) quizPanel.SetActive(false);

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    private void ClearQuizTrash()
    {
        foreach (DragTrash drag in currentTrashList)
        {
            if (drag != null) Destroy(drag.gameObject);
        }
        currentTrashList.Clear();

        if (trashContainer != null)
        {
            for (int i = trashContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(trashContainer.GetChild(i).gameObject);
            }
        }
    }
}