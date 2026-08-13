using UnityEngine;
using UnityEngine.SceneManagement;

public class pintu : MonoBehaviour
{   
    public GameObject ui;
    private bool posisi = false;

    void Start()
    {
        posisi = false;
        ui.SetActive(false);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            posisi = true;
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            posisi = false;
            ui.SetActive(false);
        }
    }
}
