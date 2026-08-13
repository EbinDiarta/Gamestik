using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEnd : MonoBehaviour
{
    void Start()
    {
        
        Sound.instance.StopMusic();
        Sound.instance.PlayMusic(Sound.instance.gameEnd);
        
    }   

    void Update()
    {
     if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneData.home);
        }   
    }

}
