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
        if (QuizManager.instance == null)
            return;

        if (QuizManager.instance.quizPanel.activeSelf)
            return;

        QuizManager.instance.OpenQuiz(this);
    
        }
    }

    private void Awake()
    {
        trashID = gameObject.name.Replace("(Clone)", "");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}