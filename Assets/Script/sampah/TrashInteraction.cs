using UnityEngine;

public class TrashInteraction : MonoBehaviour
{
    private bool playerInside = false;
    


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

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            OpenQuiz();
        }
    }

    private void OpenQuiz()
    {
        Trash trash = GetComponent<Trash>();

        if (trash != null)
        {
            QuizManager.instance.OpenQuiz(trash);
        }
        else
        {
            Debug.LogWarning("Script Trash tidak ditemukan pada object ini.");
        }
    }
}