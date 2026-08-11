using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gank : MonoBehaviour
{ 
    public GameObject ui;
    private bool gang = false;

    public CameraFollow cam;
    public Image fadePanel;
    public Transform mc;
    public Transform tujuan;

    bool isTeleporting;

    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (gang && Input.GetKeyDown(KeyCode.F) && !isTeleporting){
            StartCoroutine(Teleport());
        
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gang = true;
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gang = false;
            ui.SetActive(false);
        }
    }

    
    IEnumerator Teleport()
    {
        isTeleporting = true;

        fadePanel.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn());

        mc.position = tujuan.position;

        yield return null;

        cam.SnapToTarget();

        yield return StartCoroutine(FadeOut());

        fadePanel.gameObject.SetActive(false);

        isTeleporting = false;
    }

    IEnumerator FadeIn()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(0, 1, t);
            fadePanel.color = c;

            yield return null;
        }

        Color akhir = fadePanel.color;
        akhir.a = 1f;
        fadePanel.color = akhir;
    }

    IEnumerator FadeOut()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(1, 0, t);
            fadePanel.color = c;

            yield return null;
        }

        Color akhir = fadePanel.color;
        akhir.a = 0f;
        fadePanel.color = akhir;
    }

}

