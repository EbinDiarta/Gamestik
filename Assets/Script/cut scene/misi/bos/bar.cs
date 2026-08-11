using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    public Slider darah;


    public void SetDarah(float value)
    {
        darah.value = value;
    }
}
