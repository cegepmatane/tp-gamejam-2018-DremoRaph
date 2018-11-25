using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 34;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    //int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    //ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource gunAudio;                           // Reference to the audio source.
    //Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 1f;                // The proportion of the timeBetweenBullets that the effects will display for.

    private Transform targetedEnemy;

    void Awake()
    {
        gunLine = GetComponentInChildren<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }
        
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;
        
        gunAudio.Play();
        
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPoint.z = -1;

        Debug.DrawRay(transform.position + transform.forward * 5, clickPoint - transform.position, Color.red, 1f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.forward * 5, clickPoint - transform.position);
        bool stopcast = false;
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player")) continue;
            if (stopcast) break;
            gunLine.enabled = true;
            Vector3 origin = transform.position;
            origin.z = -1;
            gunLine.SetPosition(0, origin);

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                    
                Debug.Log("Enemy hit");
                stopcast = true;
            }

                
            gunLine.SetPosition(1, (clickPoint));// * range);
                
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("Wall hit");
                stopcast = true;
            }
        }
    }
}