using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lawan : MonoBehaviour
{
     public Slider Heald;
    public float drh = 100f;

    void Start()
    {
        Heald.maxValue = drh;
        Heald.value = drh;
    }

    void Dead()
    {
        drh -= 10f;
        drh = Mathf.Clamp(drh, 0f, 100f);

        Heald.value = drh;

        drh -= 10f;
        Heald.value = drh;

        if (drh <= 0f)
        {
            drh = 100f;
            fight.instance.over();
        }
            
        }    

 void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("peluru"))
        {
            Dead();
        }
    }
}
