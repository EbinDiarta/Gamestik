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
    public TextMeshProUGUI timerText; //

    [Header("Boss Quiz Settings")]
    public GameObject[] quizTrashPrefabs; // Array prefab data sampah
    public Transform[] spawnPointsUI;     // Array 19 Point Transform UI (spawn 0 s/d 18)
    public int jumlahSampahQuiz = 5;      // Jumlah sampah UI yang dimunculkan
    public float maxTime = 30f;           // Durasi timer minigame

    [HideInInspector] public bool isQuizActive = false; //

    private float currentTime; //
    private bool isTiming; //
    private PlayerController playerController; //
    private GameObject touchedWorldTrash; // Menyimpan referensi sampah 2D di OverWorld

    private List<DragTrash> currentTrashList = new List<DragTrash>(); //

    private void Awake()
    {
        instance = this; //
    }

    private void Start()
    {
        if (quizPanel != null) quizPanel.SetActive(false); //
    }

    private void Update()
    {
        if (!isTiming) return; //

        currentTime -= Time.deltaTime; //
        UpdateTimerUI(); //

        if (currentTime <= 0)
        {
            currentTime = 0; //
            TimeUp(); //
        }
    }

    public void OpenBossQuiz(PlayerController player, GameObject worldTrash)
    {
        if (isQuizActive) return; //

        playerController = player; //
        touchedWorldTrash = worldTrash; // Simpan referensi sampah 2D OverWorld yang disentuh

        // 1. Matikan gerak Player saat Quiz dibuka
        if (playerController != null)
        {
            playerController.enabled = false; //

            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>(); //
            if (rb != null) rb.velocity = Vector2.zero; //

            Animator anim = playerController.GetComponent<Animator>(); //
            if (anim != null) anim.SetBool("IsRun", false); //
        }

        ClearQuizTrash(); //

        // 2. Aktifkan Panel UI Minigame
        if (quizPanel != null) quizPanel.SetActive(true); //

        // Force Canvas Update agar skala UI siap
        Canvas.ForceUpdateCanvases(); //

        // 3. Spawn sampah UI
        if (spawnPointsUI != null && spawnPointsUI.Length > 0)
        {
            List<Transform> availableSpawns = new List<Transform>(spawnPointsUI); //
            int spawnAmount = Mathf.Min(jumlahSampahQuiz, availableSpawns.Count); //

            for (int i = 0; i < spawnAmount; i++)
            {
                int randomSpawnIndex = Random.Range(0, availableSpawns.Count); //
                GameObject randomTrashPrefab = quizTrashPrefabs[Random.Range(0, quizTrashPrefabs.Length)]; //

                CreateQuizTrashAtSpawn(randomTrashPrefab, availableSpawns[randomSpawnIndex]); //
                availableSpawns.RemoveAt(randomSpawnIndex); //
            }
        }

        isQuizActive = true; //
        StartTimer(); //
    }

    private void CreateQuizTrashAtSpawn(GameObject prefab, Transform spawnTarget)
    {
        if (prefab == null || dragTrashPrefab == null) return; //

        GameObject obj = Instantiate(dragTrashPrefab, trashContainer); //
        RectTransform rect = obj.GetComponent<RectTransform>(); //
        DragTrash drag = obj.GetComponent<DragTrash>(); //
        Trash trash = prefab.GetComponent<Trash>(); //

        if (rect == null || drag == null || trash == null) //
        {
            Destroy(obj); //
            return; //
        }

        rect.anchorMin = new Vector2(0.5f, 0.5f); //
        rect.anchorMax = new Vector2(0.5f, 0.5f); //
        rect.pivot = new Vector2(0.5f, 0.5f); //
        rect.localScale = Vector3.one; //

        // 1. SET KOORDINAT ACAK UI
        float randomX = Random.Range(-850f, 850f); //
        float randomY = Random.Range(-100f, 500f); //
        rect.anchoredPosition = new Vector2(randomX, randomY); //

        // 2. KUNCI POSISI DENGAN PANGGILAN RESETPOSITION
        drag.ResetPosition(); //

        // 3. SET DATA TIPE SAMPAH & SPRITE
        drag.trashType = trash.jenisSampah; //
        drag.currentTrash = trash;          // Set referensi trash agar tidak null

        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>(); //
        Image image = obj.GetComponent<Image>() ?? obj.GetComponentInChildren<Image>(); //

        if (spriteRenderer != null && image != null) //
        {
            image.sprite = spriteRenderer.sprite; //
            image.preserveAspect = true; //
        }

        currentTrashList.Add(drag); //
    }

   public void CheckDrop(DragTrash dragTrash, Trash.TrashType selectedBin)
    {
        if (dragTrash == null || !isQuizActive) return; //

        // 1. Jika BENAR: Tipe sampah sesuai dengan Tong Sampah
        if (dragTrash.trashType == selectedBin) //
        {
            currentTrashList.Remove(dragTrash); //
            Destroy(dragTrash.gameObject); //

            if (currentTrashList.Count == 0) //
            {
                FinishBossQuiz(); //
            }
        }
        // 2. Jika SALAH: Kembalikan fisik sampah ke posisi semula
        else
        {
            dragTrash.ReturnToStartPosition();
        }
    }

    private void StartTimer()
    {
        currentTime = maxTime; //
        isTiming = true; //
        UpdateTimerUI(); //
    }

    private void UpdateTimerUI()
    {
        if (timerText != null) //
        {
            timerText.text = "Waktu: " + Mathf.CeilToInt(currentTime); //
        }
    }

    private void TimeUp()
    {
        // Panggil kegagalan saat waktu habis
        OnQuizFailed(); //
    }

    private void FinishBossQuiz()
    {
        if (touchedWorldTrash != null) //
        {
            Destroy(touchedWorldTrash); // Hapus sampah OverWorld jika berhasil
        }

        CloseQuizUI(); //
    }

    public void OnQuizFailed()
    {
        CloseQuizUI(); // Tutup UI Quiz

        // Aktifkan kembali Player & dorong mundur dari posisi sampah OverWorld
        if (playerController != null && touchedWorldTrash != null)
        {
            playerController.enabled = true; //
            playerController.MundurFromTrash(touchedWorldTrash.transform.position, 2f);
        }
    }

    private void CloseQuizUI()
    {
        isTiming = false; //
        isQuizActive = false; //

        ClearQuizTrash(); //

        if (quizPanel != null) quizPanel.SetActive(false); //

        if (playerController != null)
        {
            playerController.enabled = true; //
        }
    }

    private void ClearQuizTrash()
    {
        foreach (DragTrash drag in currentTrashList) //
        {
            if (drag != null) Destroy(drag.gameObject); //
        }
        currentTrashList.Clear(); //

        if (trashContainer != null) //
        {
            for (int i = trashContainer.childCount - 1; i >= 0; i--) //
            {
                Destroy(trashContainer.GetChild(i).gameObject); //
            }
        }
    }
}