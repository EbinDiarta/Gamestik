using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jmbtntopsr1 : MonoBehaviour
{
    public CameraFollow cam;
    public Image fadePanel;

    bool isTeleporting;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
        SceneManager.LoadScene(SceneData.misi);
        }
    }
}