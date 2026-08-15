using UnityEngine;

public class Trash : MonoBehaviour
{
    private bool playerInside = false;

    public enum TrashType
    {
        Organik,
        NonOrganik
    }

    public TrashType jenisSampah;

    [HideInInspector]
    public string trashID;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController player = FindObjectOfType<PlayerController>();

            // 1. Cek jika di Scene Boss (BossQuizManager aktif)
            if (BossQuizManager.instance != null)
            {
                if (BossQuizManager.instance.quizPanel != null && BossQuizManager.instance.quizPanel.activeSelf) 
                    return;

                BossQuizManager.instance.OpenBossQuiz(player, this.gameObject);
            }
            // 2. Jika bukan Scene Boss, jalankan QuizManager bawaan kamu
            else if (QuizManager.instance != null)
            {
                if (QuizManager.instance.quizPanel != null && QuizManager.instance.quizPanel.activeSelf) 
                    return;

                QuizManager.instance.OpenQuiz(this);
            }
        }
    }

    private void Awake()
    {
        trashID = gameObject.name.Replace("(Clone)", "");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}