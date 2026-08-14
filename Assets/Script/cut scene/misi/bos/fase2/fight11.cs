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

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stamina = 100f;
        if (HealdBar != null)
        {
            HealdBar.maxValue = 100f;
            HealdBar.value = stamina;
        }
    }

    public void over()
    {
        stamina = 100f;
        if (HealdBar != null)
        {
            HealdBar.value = stamina;
        }
    }

    public void benar()
    {
        stamina -= 5f;

        if (HealdBar != null)
        {
            HealdBar.value = stamina;
        }

        Debug.Log("Keycode benar! HP lawan: " + stamina);

        if (stamina <= 0f)
        {
            stamina = 0f;

            int win = PlayerPrefs.GetInt("win", 1);
            PlayerPrefs.SetInt("winning" + win, 1);

            SceneManager.LoadScene(SceneData.ending);
        }
    }
}