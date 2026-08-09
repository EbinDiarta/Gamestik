using UnityEngine;
using System.Collections;


public class Spawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public Transform[] spawn;
    int minT = 3;
    int Maxt = 6;

    public MiniBoss miniBoss;


    void Start()
    {
        StartCoroutine(LoopSampah());
    }


    IEnumerator LoopSampah()
    {
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        int statusKuis = PlayerPrefs.GetInt("KuisSelesai_Babak_" + babakAktif, 0);

        
            if (statusKuis != 1)
        {
            yield break;
        }

        while (!miniBoss.miniBossKalah)
        {
            SpawnTrash();
            yield return new WaitForSeconds(3f);
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
