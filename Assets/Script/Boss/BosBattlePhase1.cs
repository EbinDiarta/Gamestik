using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattlePhase1 : MonoBehaviour
{
    [Header("UI Canvas Setup")]
    [SerializeField] private GameObject bossBattleCanvas;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private GameObject[] playerHearts;

    [Header("Boss & Player Settings")]
    [SerializeField] private int maxBossHealth = 4;
    [SerializeField] private int trashRequiredPerHit = 4;
    [SerializeField] private GameObject bossObject;

    [Header("Trash Spawner Settings")]
    [SerializeField] private GameObject[] trashPrefabs;
    [SerializeField] private Transform trashSpawnPoint;

    [Header("Dialog & Feedback")]
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private string[] bossHitDialogs;

    private int currentBossHealth;
    private int currentPlayerHealth;
    private int currentTrashCount;
    private GameObject currentSpawnedTrash;

    public bool isBattleActive { get; private set; } = false;

    private void Start()
    {
        if (bossBattleCanvas != null)
        {
            bossBattleCanvas.SetActive(false);
        }
    }

    public void StartPhase1Battle()
    {
        if (bossBattleCanvas != null)
        {
            bossBattleCanvas.SetActive(true);
        }

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

    public void SpawnNextTrash()
    {
        if (!isBattleActive) return;

        if (currentSpawnedTrash != null)
        {
            Destroy(currentSpawnedTrash);
        }

        if (trashPrefabs.Length == 0 || trashSpawnPoint == null)
        {
            Debug.LogWarning("Trash Prefabs atau Trash Spawn Point belum diatur di Inspector!");
            return;
        }

        int randomIndex = Random.Range(0, trashPrefabs.Length);
        currentSpawnedTrash = Instantiate(trashPrefabs[randomIndex], trashSpawnPoint.position, Quaternion.identity, bossBattleCanvas.transform);
    }

    public void OnTrashSortedCorrectly()
    {
        if (!isBattleActive) return;

        currentTrashCount++;

        if (currentTrashCount >= trashRequiredPerHit)
        {
            currentTrashCount = 0;
            DamageBoss(1);
        }
        else
        {
            SpawnNextTrash();
        }
    }

    public void OnTrashSortedWrongly()
    {
        if (!isBattleActive) return;

        TakePlayerDamage(1);
        SpawnNextTrash();
    }

    public void DamageBoss(int damageAmount)
    {
        currentBossHealth -= damageAmount;
        if (currentBossHealth < 0) currentBossHealth = 0;

        if (bossHealthSlider != null)
        {
            bossHealthSlider.value = currentBossHealth;
        }

        if (currentBossHealth <= 0)
        {
            WinBattle();
        }
        else
        {
            SpawnNextTrash();
        }
    }

    public void TakePlayerDamage(int damageAmount)
    {
        currentPlayerHealth -= damageAmount;
        if (currentPlayerHealth < 0) currentPlayerHealth = 0;

        UpdateHeartUI();

        if (currentPlayerHealth <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < playerHearts.Length; i++)
        {
            if (playerHearts[i] != null)
            {
                playerHearts[i].SetActive(i < currentPlayerHealth);
            }
        }
    }

    private void WinBattle()
    {
        isBattleActive = false;
        if (currentSpawnedTrash != null) Destroy(currentSpawnedTrash);

        Debug.Log("Selamat! Fase 1 berhasil dikalahkan!");
    }

    private void GameOver()
    {
        isBattleActive = false;
        if (currentSpawnedTrash != null) Destroy(currentSpawnedTrash);

        Debug.Log("Game Over! Darah player habis.");
    }
}