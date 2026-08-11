using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guru : MonoBehaviour
{
    public GameObject ui;
    public GameObject gr;
    private bool tc = false;

    public static bool sudahNgomong = false;
    
    void Start()
    {
        
        gr.SetActive(false);
        ui.SetActive(false);
    }

    void Update(){

         {
            int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        int statusKuis = PlayerPrefs.GetInt("KuisSelesai_Babak_" + babakAktif, 0);

        if (statusKuis == 1)
        {
            gr.SetActive(true);
        }
        else
        {
            gr.SetActive(false);
        }

        if (tc && Input.GetKeyDown(KeyCode.E)){
            if (!sudahNgomong)
            {
                Intro.instance.Guru();
                sudahNgomong = true;
                ui.SetActive(false);
            }
        }
    }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
       
        {
            tc = true;
            ui.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tc = false;
            ui.SetActive(false);
        }
    }
}
