using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kemusuh : MonoBehaviour
{ 
    public GameObject ui;
    private bool posisi = false;

    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (posisi && Input.GetKeyDown(KeyCode.F)){
            if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        SceneManager.LoadScene(SceneData.boss);
        }
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

