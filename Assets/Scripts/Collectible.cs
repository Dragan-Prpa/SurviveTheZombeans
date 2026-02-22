using UnityEngine;

public class Collectible : MonoBehaviour
{

    public float rotationSpeed = 1.0f;
    public float ammoAmmount = 100f;
    public AudioSource audioSource;
    public AudioClip pickup;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(pickup);
            //Debug.Log("pokupljen");
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            rend.enabled = false;
            Weapon weapon = other.GetComponentInChildren<Weapon>();
            weapon.currentAmmo += ammoAmmount;
            Destroy(gameObject, 1f);
       
        }
    }
}
