using UnityEngine;

public class TrashItem : MonoBehaviour
{
    
    public int id;

    public void Init(int newId)
    {
        id = newId;
    }

    void OnMouseDown()
    {
        TrashSpawner.cleanedTrash.Add(id);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           uismph.instance.Sentuh();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            uismph.instance.Tutup();
        }
    }
}