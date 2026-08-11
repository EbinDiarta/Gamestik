using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lawan : MonoBehaviour
{
    public Slider Heald;
    public float drh = 100f;
    
    private bool leo = false;

    void Update()
    {
        if (leo)
        {
            drh -= 5f;
            Heald.value = drh;
            leo = false;
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leo = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leo = false;
        }
    }
}
