using UnityEngine;

public class KontrolPenghalang : MonoBehaviour
{
    private BoxCollider2D colliderPenghalang;

    void Awake()
    {
        colliderPenghalang = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        PeriksaStatusPenghalang();
    }

    public void PeriksaStatusPenghalang()
    {
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        int statusKuis = PlayerPrefs.GetInt("KuisSelesai_Babak_" + babakAktif, 0);

        if (statusKuis == 1)
        {
            if (colliderPenghalang != null) colliderPenghalang.enabled = false;
            Debug.Log("Akses ke jembatan TERBUKA untuk Babak " + babakAktif);
        }
        else
        {
            
            if (colliderPenghalang != null) colliderPenghalang.enabled = true;
            Debug.Log("Akses ke jembatan TERTUTUP. Selesaikan kuis dulu!");
        }
    }
}