using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public int explosiveMultiplier = 1;               // The amount of health taken away per attack.
    public float radius = 1.5f;               // The amount of health taken away per attack.
    public GameObject explosion;

    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    bool exploded = false;


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
        float distance = (transform.position - player.transform.position).magnitude;
        // Add the time since Update was last called to the timer.
        if(distance <= 1.5f) Explosion();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Explosion()
    {
        if (exploded) return;
        // playerHealth.TakeDamage(explosiveMultiplier);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        int i = 0;
        foreach (Collider2D col in hitColliders)
        {
            float distance = 12 - (this.transform.position - col.transform.position).magnitude;
            if (distance == 0) continue;
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            int dmgTaken = (int)distance * explosiveMultiplier;
            if (enemyHealth != null)
                enemyHealth.TakeDamage(dmgTaken);
            
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(dmgTaken);
            
            i++;
        }
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        exploded = true;
        enemyHealth.Death();
    }
}