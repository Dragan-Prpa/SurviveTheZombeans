using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float lookSpeed=2.0f;
    private float xRotation = 0f;
    public float speed = 5.0f;
    public float sprintSpeed = 8.0f;   
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    private bool isGrounded;

    public int HP = 10;
    public Slider HPBar;
    private float nextDamageTime = 0;
    private AudioSource audioSource;
     public AudioClip hurt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = HP;
        if(HP<1)
        {
            PlayerDeath();
        }

        float currrentSpeed = Input.GetButton("Sprint") ? sprintSpeed : speed;


        RotateCamera();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = (transform.right*h+transform.forward*v)*currrentSpeed;
        Vector3 newVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

         newVelocity.y=rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        transform.Rotate(Vector3.up*mouseX);
    
        xRotation -= mouseY;
        xRotation= Mathf.Clamp(xRotation, -90f , 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation , 0f, 0f);


    }


        void OnCollisionStay(Collision collision)
        {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(Time.time >= nextDamageTime)
            {
                HP--;
                audioSource.PlayOneShot(hurt);
                nextDamageTime = Time.time + 1.0f;
            }
        }
        }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
        

   void PlayerDeath()
    {
        SceneManager.LoadScene("DeathScene");
    }
}