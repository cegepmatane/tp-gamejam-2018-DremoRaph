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
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.
            Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        Ray mseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane playerPlane = new Plane(Vector3.back, transform.position);
        float d;
        if (playerPlane.Raycast(mseRay, out d))
        {
            Vector3 hitPt = mseRay.GetPoint(d);

            Debug.DrawRay(transform.position, hitPt - transform.position, Color.red, 1f);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, hitPt - transform.position);
            if (hit)
            {
                gunLine.enabled = true;
                gunLine.SetPosition(0, transform.position);

                if (hit.collider.CompareTag("Enemy"))
                {
                    // Try and find an EnemyHealth script on the gameobject hit.
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                    // If the EnemyHealth component exist...
                    if (enemyHealth != null)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(damagePerShot, hit.point);
                    }

                    // Set the second position of the line renderer to the point the raycast hit.
                    gunLine.SetPosition(1, (hitPt - transform.position) * range);
                    targetedEnemy = hit.transform;
                    Debug.Log("Enemy");
                }

                else
                {
                    gunLine.SetPosition(1, (hitPt - transform.position) * range);
                }

                if (hit.collider.CompareTag("Wall"))
                {
                    Debug.Log("Wall");
                }
            }
        }
    }
}