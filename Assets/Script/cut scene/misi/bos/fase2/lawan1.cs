using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lawan1 : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject panelKeycode;

    public bool kena = false;

    KeyCode cit;
    float timer = 0.002f;

    void Start()
    {
        panelKeycode.SetActive(false);

        NextKey();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("peluru"))
        {
            kena = true;

            panelKeycode.SetActive(true);

            NextKey();
        }
    }

    void Update()
    {
        if (!kena)
            return;

        timer -= Time.deltaTime;

        if (Input.GetKeyDown(cit))
        {
            fight11.instance.benar();

            panelKeycode.SetActive(false);

            kena = false;
            tembak12.instance.setTembak();
            NextKey();
        }

        if (timer <= 0f)
        {
            
            panelKeycode.SetActive(false);
            lawan12.instance.Dead();
            tembak12.instance.setTembak();

            NextKey();
        }
    }

    void NextKey()
    {
        KeyCode[] keys =
        {
            KeyCode.E,
            KeyCode.C,
            KeyCode.V,
            KeyCode.N,
            KeyCode.P,
            KeyCode.R,
            KeyCode.T
        };

        cit = keys[Random.Range(0, keys.Length)];

        text.text = cit.ToString();

        timer = 2f;
    }
}