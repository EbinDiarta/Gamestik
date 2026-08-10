using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBattlePhase1 : MonoBehaviour
{
    [Header("Boss Health Settings")]
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private int maxBossHealth = 4;
    private int currentBossHealth;

    [Header("Trash Counter Settings")]
    [SerializeField] private int trashRequiredPerHit = 4;
    private int currentTrashCount = 0;

    [Header("Player Health Settings")]
    [SerializeField] private Image[] playerHearts; // Array 3 Icon Hati
    private int currentPlayerHealth = 3;

    [Header("Trash Spawner Settings")]
    [SerializeField] private GameObject[] trashPrefabs; // Daftar Prefab Sampah 3R
    [SerializeField] private Transform trashSpawnPoint; // Posisi munculnya sampah di UI Canvas
    private GameObject currentActiveTrash;

    [Header("Dialog Hit Settings")]
    [SerializeField] private DialogManager dialogManager;
    [TextArea(2, 4)]
    [SerializeField] private string[] bossHitDialogs; // Dialog Boss tiap terkena hit (opsional per hit)
    private int currentHitDialogIndex = 0;

    [Header("Phase Transition")]
    [SerializeField] private GameObject bossObject;

    public bool isBattleActive { get; private set; } = false;

    public void StartPhase1Battle()
    {
        currentBossHealth = maxBossHealth;
        if (bossHealthSlider != null)
        {
            bossHealthSlider.maxValue = maxBossHealth;
            bossHealthSlider.value = currentBossHealth;
        }

        currentPlayerHealth = playerHearts.Length;
        UpdateHeartUI();

        currentTrashCount = 0;
        isBattleActive = true;

        SpawnNextTrash();
    }

    public void OnTrashSortedCorrectly()
    {
        if (!isBattleActive) return;

        currentTrashCount++;

        // Jika sudah mencapai 4 sampah benar
        if (currentTrashCount >= trashRequiredPerHit)
        {
            currentTrashCount = 0;
            ApplyDamageToBoss();
        }
        else
        {
            SpawnNextTrash();
        }
    }

    public void OnTrashSortedWrongly()
    {
        if (!isBattleActive) return;

        currentPlayerHealth--;
        UpdateHeartUI();

        if (currentPlayerHealth <= 0)
        {
            GameOver();
        }
        else
        {
            SpawnNextTrash(); // Munculkan sampah berikutnya
        }
    }

    private void ApplyDamageToBoss()
    {
        currentBossHealth--;
        if (bossHealthSlider != null) bossHealthSlider.value = currentBossHealth;

        if (currentBossHealth <= 0)
        {
            OnBossDefeatedPhase1();
        }
        else
        {
            // Jeda sementara battle untuk Dialog Hit Boss
            StartCoroutine(TriggerHitDialogRoutine());
        }
    }

    private IEnumerator TriggerHitDialogRoutine()
    {
        isBattleActive = false; // Matikan input drag sementara

        if (dialogManager != null && bossHitDialogs.Length > currentHitDialogIndex)
        {
            // Tampilkan dialog hit dari boss
            dialogManager.StartDialog();
            
            // Tunggu hingga dialog diselesaikan oleh player
            bool isDialogDone = false;
            System.Action completedCallback = null;
            completedCallback = () => {
                isDialogDone = true;
                dialogManager.OnDialogCompleted -= completedCallback;
            };
            dialogManager.OnDialogCompleted += completedCallback;

            yield return new WaitUntil(() => isDialogDone);
            currentHitDialogIndex++;
        }

        isBattleActive = true; // Aktifkan battle kembali
        SpawnNextTrash();
    }

    public void SpawnNextTrash()
    {
        if (!isBattleActive) return;

        if (currentActiveTrash != null)
        {
            Destroy(currentActiveTrash);
        }

        if (trashPrefabs.Length > 0 && trashSpawnPoint != null)
        {
            int randomIndex = Random.Range(0, trashPrefabs.Length);
            currentActiveTrash = Instantiate(trashPrefabs[randomIndex], trashSpawnPoint);
            currentActiveTrash.transform.localPosition = Vector3.zero;

            // Sambungkan event Drag Drop sampah ke Manager
            TrashDragDrop trashScript = currentActiveTrash.GetComponent<TrashDragDrop>();
            if (trashScript != null)
            {
                trashScript.SetupBattleManager(this);
            }
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < playerHearts.Length; i++)
        {
            if (i < currentPlayerHealth)
                playerHearts[i].enabled = true;
            else
                playerHearts[i].enabled = false;
        }
    }

    private void OnBossDefeatedPhase1()
    {
        isBattleActive = false;
        if (currentActiveTrash != null) Destroy(currentActiveTrash);

        Debug.Log("Fase 1 Selesai! Boss lari ke lokasi berikutnya.");
        // Logika perpindahan ke latar/fase berikutnya dapat ditambahkan di sini
    }

    private void GameOver()
    {
        isBattleActive = false;
        if (currentActiveTrash != null) Destroy(currentActiveTrash);
        Debug.Log("Player Kalah!");
    }
}