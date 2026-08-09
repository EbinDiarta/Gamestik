using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn2 : MonoBehaviour
{
    public GameObject UI; 
    public Image fadePanel;
    public CameraFollow cam;

    void Start()
    {
        StartCoroutine(Intro());
        UI.SetActive(false);
    }



    IEnumerator Intro()
    {
        

        fadePanel.gameObject.SetActive(true);

        StartCoroutine(FadeIn());


        yield return null;

        cam.SnapToTarget();



        yield return StartCoroutine(FadeOut());

        fadePanel.gameObject.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 3f;

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
            t += Time.deltaTime * 3f;

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