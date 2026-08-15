using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f;

    [Header("Smooth Movement")]
    public float acceleration = 12f;
    public float deceleration = 18f;

    [Header("Components")]
    [SerializeField] private Animator animator;
    public Controller input;

    private Rigidbody2D rb;
    private float currentVelocityX;

    private Vector3 originalScale;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("useSpawn", 0) == 1)
        {
            GameObject spawn = GameObject.Find("SpawnBawah");

            if (spawn != null)
            {
            
                transform.position = spawn.transform.position + new Vector3(0, -0.2f, 0);
            }

            PlayerPrefs.SetInt("useSpawn", 0);
        } else if (PlayerPrefs.GetInt("KeluarKelas", 0) == 1)
        {
            GameObject spawn = GameObject.Find("SpawnLorong");

            if (spawn != null)
            {
            
                transform.position = spawn.transform.position + new Vector3(0, -0.2f, 0);
            }

            PlayerPrefs.SetInt("KeluarKelas", 0);
        }
        else if (PlayerPrefs.GetInt("MasukKamar", 0) == 1)
        {
            GameObject spawn = GameObject.Find("SpawnKamar");

            if (spawn != null)
            {
            
                transform.position = spawn.transform.position + new Vector3(0, -0.2f, 0);
            }

            PlayerPrefs.SetInt("MasukKamar", 0);
        }
        else if (PlayerPrefs.GetInt("Babak", 0) == 1)
        {
            GameObject spawn = GameObject.Find("GantiBabak");

            if (spawn != null)
            {
            
                transform.position = spawn.transform.position + new Vector3(0, -0.2f, 0);
            }

            PlayerPrefs.SetInt("Babak", 0);
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (input == null)
        {
            input = FindObjectOfType<Controller>();
        }

        originalScale = transform.localScale;
    }
    private void Update()
    {
        float move = 0f;

        // Input UI
        if (input != null)
            move += input.horizontal;


        move += Input.GetAxisRaw("Horizontal");

        move = Mathf.Clamp(move, -1f, 1f);

        if (move > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        float targetSpeed = move * speed;

        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            currentVelocityX = Mathf.Lerp(
                currentVelocityX,
                targetSpeed,
                acceleration * Time.deltaTime
            );
        }
        else
        {
            currentVelocityX = Mathf.Lerp(
                currentVelocityX,
                0,
                deceleration * Time.deltaTime
            );
        }

    
        if (animator != null)
        {
            animator.SetBool(
                "IsRun",
                Mathf.Abs(currentVelocityX) > 0.1f
            );
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(
            currentVelocityX,
            rb.velocity.y
        );
    }

    // Method untuk memundurkan Player dari posisi sampah Overworld
// Method untuk memundurkan Player dari posisi sampah Overworld
    public void MundurFromTrash(Vector3 trashPosition, float distance = 20f)
    {
        // Hitung posisi horizontal (X) antara Player dan Sampah OverWorld
        float directionX = transform.position.x - trashPosition.x;

        // Jika Player di sebelah kanan sampah, mundur ke kanan (+1). Jika di kiri, mundur ke kiri (-1).
        float pushDirection = directionX >= 0 ? 1f : -1f;

        // Reset kecepatan pergerakan tombol agar tidak menahan pergeseran
        currentVelocityX = 0f;

        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Hentikan gaya fisik saat ini
            
            // Pindahkan posisi Rigidbody2D secara instan di koordinat World Space
            rb.position = new Vector2(rb.position.x + (pushDirection * distance), rb.position.y);
        }
        else
        {
            transform.position += new Vector3(pushDirection * distance, 0f, 0f);
        }
    }
}