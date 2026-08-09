using UnityEngine;
using UnityEngine.SceneManagement;

public class masukkelas: MonoBehaviour
{   
    public GameObject ui;
    private bool tempat = false;

    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (tempat && Input.GetKeyDown(KeyCode.E)){
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        SceneManager.LoadScene(SceneData.sekolah);
    }
    }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tempat = true;
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tempat = false;
            ui.SetActive(false);
        }
    }
}
