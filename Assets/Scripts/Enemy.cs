using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

  
    public Transform target;
    private NavMeshAgent agent;
    public float aggroRange = 50f;
    public float HP = 5;

    public Rigidbody rb;
    public ParticleSystem blood;
    private bool isDead = false;

    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip dead;

    public AudioClip bratePrestaniOvoViseNijeURedu;
    void Start()
    {

        if(target==null)
        {
            target = GameObject.FindWithTag("Player")?.transform;
        }
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

  
    void Update()
    {
        if (HP <= 0 && !isDead)
        {
            Die();
        }
        if (!isDead)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= aggroRange)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
    }

        void OnCollisionEnter(Collision collision)
    {
        
           if (collision.gameObject.CompareTag("Projectile"))
        {
            if (HP > 1)
            {
                audioSource.PlayOneShot(hit);
            }
            else if (HP == 1)
            {
                audioSource.PlayOneShot(dead);
            }
            else
            {
                  audioSource.PlayOneShot(bratePrestaniOvoViseNijeURedu);
            }
            blood.transform.position = collision.contacts[0].point;
            blood.transform.rotation = Quaternion.identity;
            blood.Play();
            Destroy(collision.gameObject);
            HP--;
        }
        }

    void Die()
    {
        GameManager.Instance.numberOfEnemies--;
        isDead = true;
        agent.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        Destroy(gameObject, 5f);
    }
}
