using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lawan12 : MonoBehaviour
{
    public static lawan12 instance;
    public Slider Heald;
    public float drh = 100f;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        Heald.maxValue = drh;
        Heald.value = drh;
    }

    public void Dead()
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

}
