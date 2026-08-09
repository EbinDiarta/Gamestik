using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warga : MonoBehaviour
{
    public GameObject Oknum;

    public static bool sudahNgomong = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&
            !sudahNgomong)
        {
            sudahNgomong = true;
            Intro.instance.Babak4_Warga();
        }
    }
}
