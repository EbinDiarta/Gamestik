using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fight11 : MonoBehaviour
{
    public static fight11 instance;

    public Slider HealdBar;
    public float stamina = 100f;


    private bool leo = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stamina = 100f;
        leo = false;
        HealdBar.maxValue = 100f;
        HealdBar.value = stamina;

    }

    public void over()
    {
        stamina = 100f;
        HealdBar.value = stamina;
    }

    public void benar()
    {
        stamina -= 5f;

        HealdBar.value = stamina;

        Debug.Log("Keycode benar! HP lawan: " + stamina);

        if (stamina <= 0f)
        {
            stamina = 0f;

            int win = PlayerPrefs.GetInt("win", 1);

            PlayerPrefs.SetInt("winning" + win, 1);

            SceneManager.LoadScene(SceneData.ending);
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