using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tembak : MonoBehaviour
{
   public GameObject peluru;
   public Transform serang;
   public float speed = 5f;

    
    void Start()
    {
        

    }

    void Update()
    {
    }

    void setTembak()
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
