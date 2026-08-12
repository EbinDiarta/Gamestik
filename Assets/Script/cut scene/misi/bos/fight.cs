using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            StartCoroutine(UseStamina());
        }  

    }

    IEnumerator UseStamina()
    {
        yield return new WaitForSeconds(0f);
        stamina -= 5f;
        HealdBar.value = stamina;

        if (stamina <= 0)
        {
        int win = PlayerPrefs.GetInt("win", 1);
        
        PlayerPrefs.SetInt("winning" + win, 1);  
            SceneManager.LoadScene(SceneData.misi);
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
