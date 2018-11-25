using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.

    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
    //ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    BoxCollider2D boxCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount)
    {
        if (isDead)
            return;
        
        enemyAudio.Play();
        
        currentHealth -= amount;
        Debug.Log("enemy takes : " + amount);

        if (currentHealth <= 0)
        {
            isDead = true;
            GetComponent<EnemyAttack>().Explosion();
        }
    }


    public void Death()
    {
        isDead = true;
        boxCollider.isTrigger = true;
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        ScoreManager.score += scoreValue;
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<EnemyMovement>().enabled = false;
        Destroy(gameObject, 1.5f);
    }
}