using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tembak12 : MonoBehaviour
{
    public static tembak12 instance;
   public GameObject peluru;
   public Transform serang;
   public float speed = 5f;

    void Awake()
    {
        instance = this;
    } 

    void Start()
    {
        setTembak();
    }
    

    public void setTembak()
    {
        GameObject pistol= Instantiate(peluru, serang.position, serang.rotation);
        Rigidbody2D rb = pistol.GetComponent<Rigidbody2D>();
        rb.AddForce(serang.right * speed, ForceMode2D.Impulse);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            setTembak();
        }
    }

}
