using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warga1 : MonoBehaviour
{

   public GameObject ui;
    private bool posisi = false;

    public static bool sudahNgomong = false;
    
    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (posisi && Input.GetKeyDown(KeyCode.E)){
            if (!sudahNgomong)
            {
               Intro.instance.PakAris();
                sudahNgomong = true;
                ui.SetActive(false);
            }
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
