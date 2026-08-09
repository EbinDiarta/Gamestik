using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uismph : MonoBehaviour
{
    public static uismph instance;
    public GameObject ui;

    private void Awake()
    {
            instance = this;
        
    }

    void Start()
    {
        ui.SetActive(false);
    }
    
    public void Sentuh()
    {
      ui.SetActive(true);
    }

    public void Tutup()
    {
      ui.SetActive(false);
    }
    
}
