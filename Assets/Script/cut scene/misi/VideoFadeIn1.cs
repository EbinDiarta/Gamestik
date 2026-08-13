using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoFadeIn1 : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Fade")]
    public Image panelHitam;

    public GameObject Controller;
    public float durasiFade = 1f;

    void Start()
    {
        Controller.SetActive(false);
        Sound.instance.StopMusic();
        panelHitam.color = new Color(0, 0, 0, 1);

        videoPlayer.loopPointReached += OnVideoFinished;

        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
{
    Controller.SetActive(true);
    //StartCoroutine(MatikanVideo());
    SoundPasar.instance.Fight();
    NPCPatrol.instance.gasss();
    
    videoPlayer.gameObject.SetActive(false);
}

IEnumerator MatikanVideo()
{
    yield return new WaitForSecondsRealtime(
        FadeIn1.instance.durasi
    );    
    videoPlayer.gameObject.SetActive(false);
    
}


}