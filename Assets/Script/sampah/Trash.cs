using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType
    {
        Organik,
        NonOrganik
    }

    public TrashType jenisSampah;

    [HideInInspector]
    public string trashID;

    private void Awake()
    {
        trashID = gameObject.name.Replace("(Clone)", "");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (QuizManager.instance == null)
            return;

        if (QuizManager.instance.quizPanel.activeSelf)
            return;

        QuizManager.instance.OpenQuiz(this);
    }
}