using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    private Slider healthSlider;                                 // Reference to the UI's health bar.
    private Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        healthSlider = GameObject.FindGameObjectWithTag("Hp").GetComponent<Slider>();
        damageImage = GameObject.FindGameObjectWithTag("Yolo").GetComponent<Image>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;
        
        currentHealth -= amount;
        Debug.Log("Player takes: " + amount);
        healthSlider.value = currentHealth;
        
        playerAudio.Play();
        
        if (currentHealth <= 0 && !isDead)
            Death();
        
    }


    public void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        Debug.Log("Mort : " + isDead);
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        GetComponent<PlayerShooting>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<ClickToMove>().enabled = false;

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        FindObjectOfType<ResetManager>().ShowDeathPanel();

        gameObject.SetActive(false);
    }
}