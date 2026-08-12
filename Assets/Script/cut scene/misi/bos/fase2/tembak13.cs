using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tembak13 : MonoBehaviour
{
   public GameObject pl;
   public Transform srg;
   public float speed = 5f;

    public bool tbk;
    
    void Start()
    {
        

    }

    void Update()
    {
        if (tbk && Input.GetKeyDown(KeyCode.X))
        {
            setTembak();
        }
    }

    void setTembak()
    {
        GameObject pistol= Instantiate(pl, srg.position, srg.rotation);
        Rigidbody2D rb = pistol.GetComponent<Rigidbody2D>();
        rb.AddForce(srg.right * speed, ForceMode2D.Impulse);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tbk = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        tbk = false;
    }

}
