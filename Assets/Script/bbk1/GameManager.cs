using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Pause;
    public bool puse = false;
    


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale = 0f;
        Pause.SetActive(true);
        puse = true;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale = 1f;
        Pause.SetActive(false);
        puse = false;
        }


    }

    public void pause()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale = 0f;
        Pause.SetActive(true);
    }
    public void restart()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale= 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Pause.SetActive(false);
    }
    public void resume()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale= 1f;
        Pause.SetActive(false);
    }

    public void masuk_kamar()
{
    if (Sound.instance != null)
    {
        Sound.instance.PlaySFX(Sound.instance.tab);
    }
    
    PlayerPrefs.DeleteKey("useSpawn");
    PlayerPrefs.DeleteKey("KeluarKelas");

    PlayerPrefs.SetInt("MasukKamar", 1);

    SceneManager.LoadScene(SceneData.game1);
}
    public void halaman()
{
    if (Sound.instance != null)
    {
        Sound.instance.PlaySFX(Sound.instance.tab);
    }
    Sound.instance.StopMusic();
    Sound.instance.PlayMusic(Sound.instance.Halaman);
    SceneManager.LoadScene(SceneData.halaman);
}

    public void masukkelas()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        SceneManager.LoadScene(SceneData.sekolah);
    }

}
