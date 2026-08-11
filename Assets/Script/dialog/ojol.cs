using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ojol : MonoBehaviour
{
    public GameObject ui;
    private bool leo = false;

    public static bool sudahNgomong = false;
    
    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (leo && Input.GetKeyDown(KeyCode.E)){
            if (!sudahNgomong)
            {
                Intro.instance.Leon();
                sudahNgomong = true;
                ui.SetActive(false);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leo = true;
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leo = false;
            ui.SetActive(false);
        }
    }
}
