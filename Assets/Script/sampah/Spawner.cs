using UnityEngine;
using System.Collections;


public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public GameObject[] trashPrefabs;
    public Transform[] spawn;
    int minT = 5;
    int Maxt = 6;

    public bool smph;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnTrash();
    }

    IEnumerator LoopSampah()
    {
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        int statusKuis = PlayerPrefs.GetInt("KuisSelesai_Babak_" + babakAktif, 0);

        
            if (statusKuis != 1)
        {
            Debug.Log("Sampah tidak akan muncul karena kuis belum selesai.");
            yield break;
        }

        while (smph)
        {
            SpawnTrash();
            yield return new WaitForSeconds(30f);
        }
    }


    void SpawnTrash()
    {
                int jumlah = Random.Range(minT,Maxt + 1);
                for (int i = 0; i < jumlah; i++)
                {

                    GameObject trash = trashPrefabs[Random.Range(0, trashPrefabs.Length)];

                    Transform posisi = spawn[Random.Range(0, spawn.Length)];

                    Instantiate(trash,posisi.position,Quaternion.identity);
                }
        }
}
