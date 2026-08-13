using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPasar : MonoBehaviour
{
    public static SoundPasar instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Fight();
    }
    public void Fight()
    {
        
        Sound.instance.StopMusic();
        Sound.instance.PlayMusic(Sound.instance.gamePasar);
    }
}
