using UnityEngine;

public class oknum : MonoBehaviour
{
    public GameObject Oknum;
    
    public GameObject ui;
    private bool posisi = false;

    public static bool sudahNgomong = false;
    
    void Start()
    {
        ui.SetActive(false);
    }

    void Update(){
        if (posisi && Input.GetKeyDown(KeyCode.E)){
            if (!sudahNgomong)
            {
               Intro.instance.Babak1_Jalan();
                sudahNgomong = true;
                ui.SetActive(false);
            }
        }
    }


     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            posisi = true;
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            posisi = false;
            ui.SetActive(false);
        }
    }
}