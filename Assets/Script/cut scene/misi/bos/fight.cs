using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fight : MonoBehaviour
{
    public Slider HealdBar;
    public float stamina = 100f;

    public GameObject ui;
    private bool leo = false;

    
    void Start()
    {
        ui.SetActive(false);
    }

    void Update()
    {
        if (leo && Input.GetKeyDown(KeyCode.X))
        {
            stamina -= 3f;
            HealdBar.value = stamina;
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
