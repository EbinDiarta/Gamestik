using UnityEngine;
using UnityEngine.SceneManagement;

public class gerbangsekolah : MonoBehaviour
{   
    public GameObject ui;
    private bool lokasi = false;

    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (lokasi && Input.GetKeyDown(KeyCode.E))
        {
            masuksekolah.instance.gosekolah();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lokasi = true;
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lokasi = false;
            ui.SetActive(false);
        }
    }
}
