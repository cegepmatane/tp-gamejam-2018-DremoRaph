using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public int explosiveMultiplier = 1;               // The amount of health taken away per attack.
    public GameObject explosion;

    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            Explosion();
        }
    }


    void Update()
    {
        float distance = (this.transform.position - player.transform.position).magnitude;
        // Add the time since Update was last called to the timer.
        if(distance <= 1.5f) Explosion();
    }


    public void Explosion()
    {
        // playerHealth.TakeDamage(explosiveMultiplier);
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 3f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            float distance = (this.transform.position - hitColliders[i].transform.position).magnitude;
            hitColliders[i].SendMessage("TakeDamage", distance * explosiveMultiplier);
            i++;
        }

        Instantiate(explosion, this.transform.position, Quaternion.identity);
        enemyHealth.Death();
    }
}